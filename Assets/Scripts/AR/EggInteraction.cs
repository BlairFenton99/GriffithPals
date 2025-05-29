using System.Collections;
using UnityEngine;

public class EggInteraction : MonoBehaviour
{
    public Animator eggAnimator;
    public GameObject promptUI;
    public GameObject petObject;         // 👈 Assign your pet GameObject here
    public float animationDuration = 2.5f;
    private bool hasCracked = false;

    void OnMouseDown()
    {
        if (hasCracked) return;

        hasCracked = true;

        if (eggAnimator != null)
        {
            eggAnimator.SetTrigger("Crack");
        }

        StartCoroutine(HandleEggCrack());
    }

    IEnumerator HandleEggCrack()
    {
        yield return new WaitForSeconds(animationDuration);

        // Hide egg
        gameObject.SetActive(false);

        // Show pet
        if (petObject != null)
        {
            petObject.SetActive(true);
        }

        // Show next prompt
        if (promptUI != null)
        {
            promptUI.SetActive(true);
        }
    }
}

