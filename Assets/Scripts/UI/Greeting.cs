using UnityEngine;

/// <summary>
/// Manages the greeting-to-QR scanner flow.
/// </summary>
public class Greeting : MonoBehaviour
{
    [Tooltip("The greeting prompt GameObject shown first.")]
    [SerializeField]
    private GameObject greetingPrompt;

    [Tooltip("The QR scanner GameObject to enable after greeting.")]
    [SerializeField]
    private GameObject qrScannerUI;

    /// <summary>
    /// Called when the user presses the Continue button on the greeting prompt.
    /// </summary>
    public void OnContinuePressed()
    {
        if (greetingPrompt != null) greetingPrompt.SetActive(false);
        if (qrScannerUI != null) qrScannerUI.SetActive(true);
    }

    private void Start()
    {
        // Show greeting at start, hide QR scanner
        if (greetingPrompt != null) greetingPrompt.SetActive(true);
        if (qrScannerUI != null) qrScannerUI.SetActive(false);
    }
}
