using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public Slider Slider;
    public Gradient Gradient;
    public Image Fill;

    public void SetHealth(float health)
    {
        Slider.value = health;

        Fill.color = Gradient.Evaluate(Slider.normalizedValue);
    }

    public void SetMaxHealth(float maxHealth)
    {
        Slider.maxValue = maxHealth;
        Slider.value = maxHealth;

        Fill.color = Gradient.Evaluate(1f);
    }
}
