using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StatueRebuildManager : MonoBehaviour
{
    public static StatueRebuildManager Instance;

    public int totalParts = 4; // Adjust this to match the number of pillars
    private int snappedParts = 0;

    public GameObject completionPanel; // UI Panel for the completion prompt
    public TextMeshProUGUI completionText;
    public Button continueButton;

    [Header("Fade In Sphere")]
    public GameObject fadeInSphere;  // Assign your 3D sphere GameObject here

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (completionPanel != null)
            completionPanel.SetActive(false);

        if (fadeInSphere != null)
            fadeInSphere.SetActive(false); // Start disabled

        if (continueButton != null)
            continueButton.onClick.AddListener(HideCompletionPanel);
    }

    public void PartSnapped()
    {
        snappedParts++;

        if (snappedParts >= totalParts)
        {
            ShowCompletionPrompt();
        }
    }

    void ShowCompletionPrompt()
    {
        if (completionPanel != null && completionText != null)
        {
            completionText.text = "Congrats the barrier is restored! Head on over to the Student Centre to find who caused the barrier to be damaged.";
            completionPanel.SetActive(true);

            // Activate the fadeInSphere after showing prompt
            if (fadeInSphere != null)
            {
                fadeInSphere.SetActive(true);
                // The fade-in script on the sphere should handle the fading itself
            }
        }
    }

    void HideCompletionPanel()
    {
        if (completionPanel != null)
            completionPanel.SetActive(false);

        // Load the next scene (Scene 5)
        SceneManager.LoadScene("level_05_riddles");
    }
}

