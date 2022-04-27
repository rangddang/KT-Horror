using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    public RectTransform staminaBar;
    public RectTransform stamina;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            staminaBar.gameObject.SetActive(true);
            stamina.gameObject.SetActive(true);
            
        }
        else
        {
            staminaBar.gameObject.SetActive(false);
            stamina.gameObject.SetActive(false);

        }
    }
}
