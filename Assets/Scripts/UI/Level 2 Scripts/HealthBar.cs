using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        if (slider == null || fill == null)
        {
            Debug.LogWarning("Slider or Fill Image not assigned in HealthBar.");
            return;
        }
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        if (slider == null || fill == null)
        {
            Debug.LogWarning("Slider or Fill Image not assigned in HealthBar.");
            return;
        }
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
