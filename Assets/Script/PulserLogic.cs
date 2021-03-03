using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulserLogic : MonoBehaviour
{
    Rigidbody2D rb;
    Camera cam;
    Vector2 mousePos;
    Slider slider;
    [SerializeField] float waitToActivatemove = 0.5f;

    float emptyValue = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        slider = FindObjectOfType<PulserSlider>().GetComponent<Slider>();
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        DestroyItSelf();

    }

    private void DestroyItSelf()
    {
        if (Input.GetKeyUp(KeyCode.W) || slider.value == emptyValue)
        {
            Destroy(gameObject);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;
        
        
        if(otherObject.GetComponent<Player>())
        {
            FindObjectOfType<Player>().GetComponent<PlayerMovement>().enabled = false;
            float teleportOffSet = 0.1f;
            GameObject recieverRight = FindObjectOfType<RightRecieverLogic>().gameObject;
            Rigidbody2D playerRb = FindObjectOfType<Player>().GetComponent<Rigidbody2D>();
            Vector2 relPoint = transform.InverseTransformPoint(otherObject.transform.position);
            Vector2 relVelocity = -transform.InverseTransformDirection(playerRb.velocity);
            playerRb.velocity = recieverRight.transform.TransformDirection(relVelocity);
            Vector2 recieverGoPoint = recieverRight.transform.TransformPoint(relPoint);
            playerRb.transform.position = recieverGoPoint + 
                (otherCollider.attachedRigidbody.velocity.normalized) * teleportOffSet;
        }
    }*/
}
