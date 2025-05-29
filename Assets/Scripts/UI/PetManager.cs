using UnityEngine;

public class PetManager : MonoBehaviour
{
    public GameObject petPanel;         // Assign your PetPanel (UI container)
    public Animator petAnimator;        // Assign the Animator attached to your pet object inside the panel

    void Start()
    {
        petPanel.SetActive(false); // Start hidden
    }

    public void TogglePetPanel()
    {
        bool isActive = petPanel.activeSelf;
        petPanel.SetActive(!isActive);

        if (!isActive && petAnimator != null)
        {
            petAnimator.SetTrigger("Jump");  // Trigger jump only when showing the panel
        }
    }
}


