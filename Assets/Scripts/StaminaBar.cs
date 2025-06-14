using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public static StaminaBar instance;

    public float stamina;
    public float mValue;
    public Slider StaminaBa;
    float maxStamina;

    public float decreaseMultiplier = 2f;
    public float increaseMultiplier = 0.5f;

    public bool isDrained = false; 

    public bool CanRun => !isDrained || stamina >= maxStamina;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        maxStamina = stamina;
        StaminaBa.value = maxStamina;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && CanRun)
            EnerjiAzalt();

        else if (stamina != maxStamina)
            EnerjiCogalt();

        StaminaBa.value = stamina;

        stamina = Mathf.Clamp(stamina, 0f, maxStamina);

        if (stamina <= 0f)
            isDrained = true;

        if (stamina >= maxStamina)
            isDrained = false;
    }

    private void EnerjiAzalt()
    {
        stamina -= mValue * decreaseMultiplier * Time.deltaTime;
    }

    private void EnerjiCogalt()
    {
        stamina += mValue * increaseMultiplier * Time.deltaTime;
    }
}
