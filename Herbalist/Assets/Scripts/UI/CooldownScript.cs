using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CooldownScript : MonoBehaviour
{
    public Slider Slider;
    public Image Fill;

    public float CooldownTime;
    private bool _startCooldown = false;

    private void Start()
    {
        Slider.value = 0;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire2") && !_startCooldown)
        {
            _startCooldown = true;
            Slider.value = 1;
        }

        if(_startCooldown)
        {
            ApplyCooldownTimer();
        }
    }

    private void ApplyCooldownTimer()
    {
        Slider.value -= 1 / CooldownTime * Time.deltaTime;

        if (Slider.value <= 0)
        {
            _startCooldown = false;
        }
    }
}
