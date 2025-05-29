using UnityEngine;

public class riddleInteraction : MonoBehaviour
{
    public GameObject myItem;
    public GameObject selectedItem;

    void OnMouseDown()
    {
        if (myItem.activeSelf)
        {
            myItem.SetActive(false);
        }
        else
        {
            myItem.SetActive(true);
        }
            
    }
}
