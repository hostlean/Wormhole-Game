using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightRecieverLogic : MonoBehaviour
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
        if (Input.GetButtonUp("Fire2") || slider.value == emptyValue)
        {
            Destroy(gameObject);
        }
    }
    /*private void MoveWithPlayer()
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);

        rb.MovePosition(playerPos + playerToHoleVector);
    }*/

}

