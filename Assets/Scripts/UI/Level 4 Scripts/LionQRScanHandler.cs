using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.UI;

public class StatueQRScanHandler : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject scanPopup;
    public GameObject brokenStatue;

    public GameObject guardianMessagePanel; // Panel with the message and continue button
    public TextMeshProUGUI guardianMessageText; // The message text
    public Button continueButton; // The continue button

    private bool promptShown = false;

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;

        if (continueButton != null)
            continueButton.onClick.AddListener(HideGuardianMessage);
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;

        if (continueButton != null)
            continueButton.onClick.RemoveListener(HideGuardianMessage);
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            OnQRScanned(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                OnQRScanned(trackedImage);
            }
        }
    }

    void OnQRScanned(ARTrackedImage trackedImage)
    {
        if (scanPopup.activeSelf)
        {
            scanPopup.SetActive(false);
            brokenStatue.SetActive(true);

            if (!promptShown)
            {
                ShowGuardianMessage("OH NO the guardian's pillars have fallen over. We must restore the barrier.");
                promptShown = true;
            }
        }
    }

    void ShowGuardianMessage(string message)
    {
        if (guardianMessagePanel != null && guardianMessageText != null)
        {
            guardianMessageText.text = message;
            guardianMessagePanel.SetActive(true);
        }
    }

    void HideGuardianMessage()
    {
        if (guardianMessagePanel != null)
        {
            guardianMessagePanel.SetActive(false);
        }
    }
}



