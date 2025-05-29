using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FruitCollector : MonoBehaviour
{
    public static FruitCollector Instance;

    public int fruitsCollected = 0;
    public int fruitsRequired = 5;

    public GameObject upgradePrompt;
    public GameObject investigatePromptPanel;
    public Button upgradeContinueButton;
    public Button continueToStatueButton;

    void Awake()
    {
        Instance = this;

        upgradePrompt.SetActive(false);
        investigatePromptPanel.SetActive(false);

        upgradeContinueButton.onClick.AddListener(ShowInvestigatePrompt);
        continueToStatueButton.onClick.AddListener(LoadStatueScene);
    }

    public void CollectFruit()
    {
        fruitsCollected++;

        if (fruitsCollected >= fruitsRequired)
        {
            upgradePrompt.SetActive(true);
        }
    }

    void ShowInvestigatePrompt()
    {
        upgradePrompt.SetActive(false);
        investigatePromptPanel.SetActive(true);
    }

    void LoadStatueScene()
    {
        SceneManager.LoadScene("level_04_statue");
    }
}
