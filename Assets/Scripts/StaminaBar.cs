using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    public RectTransform staminaBar;
    public RectTransform stamina;
    public float staminaMax;
    public float haveStamina;
    public bool staminaBool=true;

    void Start()
    {
        staminaMax = 10f;
        haveStamina = staminaMax;
    }

    void Update()
    {
        InputStamina();
        StBool();
    }

    

    void InputStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && staminaBool == true)
        {
            //staminaBar = new Color(1, 1, 1, 1);
            haveStamina -= 2f * Time.deltaTime;
            staminaBar.gameObject.SetActive(true);
            stamina.gameObject.SetActive(true);
            stamina.localScale = new Vector3(0.39f * haveStamina, 0.4f, 0);
        }
        else
        {
            if (haveStamina < 10)
                haveStamina += 1f * Time.deltaTime;
            staminaBar.gameObject.SetActive(false);
            stamina.gameObject.SetActive(false);

        }

        //if(staminaBar.color)
    }

    void StBool()
    {
        if(haveStamina <= 0)
        {
            haveStamina = 0;
            staminaBool=false;
        }
        else if (haveStamina >= 5f)
        {
            staminaBool = true;
        }
    }
}
