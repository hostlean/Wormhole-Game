using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;
        if(otherObject.GetComponent<Player>())
        {
            Destroy(otherObject);
            FindObjectOfType<GameSession>().StartDeathSequance();
        }
    }

}
