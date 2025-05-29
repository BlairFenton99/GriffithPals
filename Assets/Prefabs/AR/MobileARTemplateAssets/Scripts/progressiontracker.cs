using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class ARObjectClickTracker : MonoBehaviour
{
    // public Text progressText; // UI text showing progress
    // public Text messageText;  // UI text showing item-specific messages
    public TMP_Text progressText; // Use if you're using TextMeshPro
    public TMP_Text messageText;  // Use if you're using TextMeshPro

    private int playerProgress = 0;
    //private HashSet<GameObject> clickedObjects = new HashSet<GameObject>();
    private HashSet<string> clickedObjectNames = new HashSet<string>();

    private ARRaycastManager raycastManager;

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        UpdateProgressText();
        messageText.text = ""; // Clear the message text at start
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject clickedObject = hit.collider.gameObject;

                    ClickableItem item = clickedObject.GetComponent<ClickableItem>();
                    if (item != null)
                    {
                        messageText.text = item.messageToDisplay;

                        string uniqueID = clickedObject.name;

                        if (!clickedObjectNames.Contains(uniqueID))
                        {
                            clickedObjectNames.Add(uniqueID);
                            playerProgress++;
                            UpdateProgressText();
                        }
                    }
                }

                /*if (Physics.Raycast(ray, out hit))
                {
                    GameObject clickedObject = hit.collider.gameObject;

                    // Show the unique message for this object
                    ClickableItem item = clickedObject.GetComponent<ClickableItem>();
                    if (item != null)
                    {
                        messageText.text = item.messageToDisplay;
                    }

                    // Only count progress the first time the object is clicked
                    if (!clickedObjects.Contains(clickedObject))
                    {
                        playerProgress++;
                        clickedObjects.Add(clickedObject);
                        UpdateProgressText();
                    }
                }*/
            }
        }
    }

    void UpdateProgressText()
    {
        progressText.text = "Progress: " + playerProgress;
    }
}
