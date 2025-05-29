using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class BossQRScanHandler : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject scanPopup;       // UI telling user to scan QR
    public GameObject battleCanvas;    // The canvas with the boss fight UI
    public AudioSource battleMusic;    // Audio source for battle music
    public PlayerHealth playerHealth;  // Reference to the player health script

    private bool battleStarted = false;

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
            CheckForBattleStart(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                CheckForBattleStart(trackedImage);
            }
        }
    }

    void CheckForBattleStart(ARTrackedImage trackedImage)
    {
        if (!battleStarted && scanPopup.activeSelf)
        {
            scanPopup.SetActive(false);
            battleCanvas.SetActive(true);
            battleStarted = true;

            if (battleMusic != null && !battleMusic.isPlaying)
            {
                battleMusic.Play();
            }

            // Notify PlayerHealth to start taking damage
            if (playerHealth != null)
            {
                playerHealth.StartTakingDamage();
            }
        }
    }
}
