using UnityEngine;
using TMPro;

public class InstructionTextController : MonoBehaviour
{
    public TextMeshProUGUI instructionText; 
    public float displayTime = 10f;
    private float timer;

    void Start()
    {
        instructionText.gameObject.SetActive(true);
        Debug.Log("the text is loading, You just can't see it due to a different screen?");
        timer = displayTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && instructionText.gameObject.activeSelf)
        {
            instructionText.gameObject.SetActive(false);
        }
    }
}