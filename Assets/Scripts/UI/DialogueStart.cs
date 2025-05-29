using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject dialogueObject;

    private TypeWriter typeWriter;
    private Coroutine typingCoroutine;

    private int currentLineIndex = 0;
    private bool isTyping = false;

    private void Awake()
    {
        typeWriter = GetComponent<TypeWriter>();
    }

    private void Start()
    {
        StartDialogue();
    }

    private void Update()
    {
        // Continue when user taps or clicks or presses space
        if (!isTyping && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ||
            (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
        {
            DisplayNextLine();
        }
    }

    public void StartDialogue()
    {
        currentLineIndex = 0;
        DisplayNextLine();
    }

    private void DisplayNextLine()
    {
        if (currentLineIndex >= dialogueObject.dialogueLines.Length)
        {
            textLabel.text = "";
            return;
        }

        string line = dialogueObject.dialogueLines[currentLineIndex];
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(RunTyping(line));
        currentLineIndex++;
    }

    private IEnumerator RunTyping(string line)
    {
        isTyping = true;
        yield return typeWriter.Run(line, textLabel);
        isTyping = false;
    }
}
