using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class QRScanHandler : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject scanPopup;     // The UI telling user to scan QR
    public GameObject eggSelectionUI; // The UI or prefab container for the eggs

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            OnQRScanned(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                OnQRScanned(trackedImage);
            }
        }
    }

    void OnQRScanned(ARTrackedImage trackedImage)
    {
        // Only trigger once
        if (scanPopup.activeSelf)
        {
            scanPopup.SetActive(false);
            eggSelectionUI.SetActive(true);
        }
    }
}
