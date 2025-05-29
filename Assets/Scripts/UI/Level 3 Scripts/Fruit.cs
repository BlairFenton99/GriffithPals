using UnityEngine;

public class Fruit : MonoBehaviour
{
    public void OnCollected()
    {
        Destroy(gameObject);  // You can also play a VFX or scale animation here
        FruitCollector.Instance.CollectFruit();
    }
}
