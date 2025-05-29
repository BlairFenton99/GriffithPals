using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue Object")]
public class DialogueObject : ScriptableObject
{
    [TextArea] public string[] dialogueLines;
}
