using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image Fill;

    public void SetMaxHealth (float MaxHealth) {
        slider.maxValue = MaxHealth;
        slider.value = MaxHealth;

        Fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth (float Health) {
        slider.value = Health;

        Fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
