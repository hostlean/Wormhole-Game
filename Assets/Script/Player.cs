using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float waitToActivatemove = 0.5f;

    private void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;
        if (otherObject.GetComponent<PulserLogic>())
        {
            StartCoroutine(WaitToEnable());
            GetComponent<PlayerMovement>().enabled = false;
            if (!FindObjectOfType<LeftRecieverLogic>())
            {
                Rigidbody2D rightRecieverRb = 
                        FindObjectOfType<RightRecieverLogic>().GetComponent<Rigidbody2D>();
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                float exitHoleRotation = rightRecieverRb.rotation - 270f;
                Vector2 velocity = rb.velocity;
                Vector3 exitHolePosition = FindObjectOfType<RightRecieverLogic>().transform.position;
                velocity = Quaternion.Euler(0, 0, exitHoleRotation) * velocity;
                transform.position = exitHolePosition;
                rb.velocity = new Vector2(Mathf.Abs(velocity.x), Mathf.Abs(velocity.y));
                StartCoroutine(WaitToEnable());
            }
            if (!FindObjectOfType<RightRecieverLogic>())
            {
                Rigidbody2D leftRecieverRb =
                        FindObjectOfType<LeftRecieverLogic>().GetComponent<Rigidbody2D>();
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                float exitHoleRotation = leftRecieverRb.rotation - 270f;
                Vector2 velocity = rb.velocity;
                Vector3 exitHolePosition = FindObjectOfType<LeftRecieverLogic>().transform.position;
                velocity = Quaternion.Euler(0, 0, exitHoleRotation) * velocity;
                transform.position = exitHolePosition;
                rb.velocity = new Vector2 (-(Mathf.Abs(velocity.x)), Mathf.Abs(velocity.y));
                StartCoroutine(WaitToEnable());
            }
        }    
    }

        IEnumerator WaitToEnable()

        {
            yield return new WaitForSeconds(waitToActivatemove);
            GetComponent<PlayerMovement>().enabled = true;

        }
        
    }
