using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARPlaneStartHandler : MonoBehaviour
{
    public GameObject firstGreetingPanel;       // Step 1: Initial greeting
    public Button firstGreetingContinueButton;

    public GameObject scanQRPanel;              // Step 2: "Please scan the QR code"

    public GameObject secondGreetingPanel;      // Step 3: "Welcome to the Business building..."
    public Button secondGreetingContinueButton;

    public GameObject fruitGoalPanel;           // New prompt after portal spawns
    public Button fruitGoalContinueButton;

    public GameObject scanInstructionPanel;     // Step 4: "Look at a wall..."

    public ARPlaneManager planeManager;
    public ARTrackedImageManager imageManager;

    private bool qrScanned = false;
    private bool planeDetected = false;
    private bool readyToScanQR = false;

    void Start()
    {
        // Initial UI states
        firstGreetingPanel.SetActive(true);
        scanQRPanel.SetActive(false);
        secondGreetingPanel.SetActive(false);
        scanInstructionPanel.SetActive(false);
        fruitGoalPanel.SetActive(false);
        planeManager.enabled = false;

        // Disable AR tracked image detection until QR scanning is allowed
        if (imageManager != null)
            imageManager.enabled = false;

        // Button handlers
        firstGreetingContinueButton.onClick.AddListener(OnFirstGreetingContinue);
        secondGreetingContinueButton.onClick.AddListener(OnSecondGreetingContinue);
        fruitGoalContinueButton.onClick.AddListener(OnFruitGoalContinue);
    }

    void OnEnable()
    {
        if (imageManager != null)
            imageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        if (imageManager != null)
            imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnFirstGreetingContinue()
    {
        firstGreetingPanel.SetActive(false);
        scanQRPanel.SetActive(true);

        // Now we allow tracked image detection
        if (imageManager != null)
        {
            imageManager.enabled = true;
            readyToScanQR = true;
        }
    }

    void OnSecondGreetingContinue()
    {
        secondGreetingPanel.SetActive(false);
        scanInstructionPanel.SetActive(true);
        planeManager.enabled = true;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        if (!readyToScanQR || qrScanned) return;

        foreach (ARTrackedImage trackedImage in args.added)
            HandleQRDetected(trackedImage);

        foreach (ARTrackedImage trackedImage in args.updated)
            HandleQRDetected(trackedImage);
    }

    void HandleQRDetected(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            qrScanned = true;
            scanQRPanel.SetActive(false);
            secondGreetingPanel.SetActive(true);
        }
    }

    void Update()
    {
        if (!planeDetected && planeManager.enabled)
        {
            foreach (ARPlane plane in planeManager.trackables)
            {
                if (plane.trackingState == TrackingState.Tracking)
                {
                    planeDetected = true;
                    scanInstructionPanel.SetActive(false);

                    // Show the fruit collection prompt
                    fruitGoalPanel.SetActive(true);
                    break;
                }
            }
        }
    }

    void OnFruitGoalContinue()
    {
        fruitGoalPanel.SetActive(false);
        // Optional: Trigger fruit spawning or pet logic here
    }
}

