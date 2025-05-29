using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject questPanel;
    public TextMeshProUGUI questText;

    [Header("Quest Settings")]
    [TextArea]
    public string startingQuestText = "Find the herbs in the portal.";

    private void Start()
    {
        if (questPanel != null)
            questPanel.SetActive(false); // Hide panel at start

        if (questText != null)
            questText.text = startingQuestText; // Set initial quest text
    }

    public void ToggleQuestPanel()
    {
        if (questPanel != null)
            questPanel.SetActive(!questPanel.activeSelf);
    }

    // Optional: Method to update quest from another script
    public void SetQuestText(string newText)
    {
        if (questText != null)
            questText.text = newText;
    }
}

