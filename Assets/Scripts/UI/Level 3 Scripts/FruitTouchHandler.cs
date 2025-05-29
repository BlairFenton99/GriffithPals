using UnityEngine;

public class FruitTouchHandler : MonoBehaviour
{
    void Update()
    {
        // Mobile touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            HandleTouch(Input.GetTouch(0).position);
        }

        // Editor mouse click
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch(Input.mousePosition);
        }
    }

    void HandleTouch(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Fruit fruit = hit.collider.GetComponent<Fruit>();
            if (fruit != null)
            {
                fruit.OnCollected();
            }
        }
    }
}

