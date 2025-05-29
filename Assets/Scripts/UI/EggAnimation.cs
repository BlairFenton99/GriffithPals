using System.Collections;
using UnityEngine;

public class EggAnimationSequence : MonoBehaviour
{
    public Animator animator;         // Egg's Animator
    public GameObject dialogueUI;     // Dialogue UI
    public GameObject dino;           // Dino sprite (starts hidden)
    public GameObject egg;            // Entire egg GameObject (to hide later)

    void Start()
    {
        dino.SetActive(false);             // Hide dino at start
        dialogueUI.SetActive(false);       // Hide dialogue
        StartCoroutine(PlayEggSequence()); // Start the animation sequence
    }

    IEnumerator PlayEggSequence()
    {
        animator.Play("Egg_Move");
        yield return new WaitForSeconds(1f);

        animator.Play("Egg_Crack");
        yield return new WaitForSeconds(1f);

        animator.Play("Egg_Hatch");
        yield return new WaitForSeconds(1f);

        egg.SetActive(false);              // 🔥 Hide the egg
        dino.SetActive(true);              // 🦖 Show the dino
        dialogueUI.SetActive(true);        // 💬 Show dialogue
    }
}