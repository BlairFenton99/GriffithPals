using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public GameObject mapPanel;
    public Image mapImage;
    public Sprite[] levelMaps; // Assign different maps for each level
    public int currentLevelIndex = 0;

    void Start()
    {
        mapPanel.SetActive(false); // Start hidden
        UpdateMap();
    }

    public void ToggleMapPanel()
    {
        mapPanel.SetActive(!mapPanel.activeSelf);
    }

    public void UpdateMap()
    {
        if (mapImage != null && levelMaps.Length > currentLevelIndex)
        {
            mapImage.sprite = levelMaps[currentLevelIndex];
        }
    }
}
