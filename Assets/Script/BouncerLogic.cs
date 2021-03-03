using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerLogic : MonoBehaviour
{
    [SerializeField] float waitForThisSeconds = 0.6f;

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;
        if(otherObject.GetComponent<Player>())
        {
            FindObjectOfType<Player>().GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(WaitToActivate());
        }
    }

    IEnumerator WaitToActivate()
    {
        yield return new WaitForSeconds(waitForThisSeconds);
        FindObjectOfType<Player>().GetComponent<PlayerMovement>().enabled = true;

    }
}
