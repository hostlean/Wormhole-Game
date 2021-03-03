using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftRecieverLogic : MonoBehaviour
{
    Slider slider;

    float emptyValue = 0f;

    void Start()
    {
        slider = FindObjectOfType<SuckerSlider>().GetComponent<Slider>();

    }
    void Update()
    {
        DestroyItSelf();
    }
    private void DestroyItSelf()
    {
        if (Input.GetButtonUp("Fire1") || slider.value == emptyValue)
        {
            Destroy(gameObject);
        }
    }

}
