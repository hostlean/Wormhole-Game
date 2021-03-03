using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 5f;
    FirePos firePos;
    [SerializeField] float projectileForce = 100f;
    Rigidbody2D rb;
    Player target;
    Vector2 moveToPlayer;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>();
        moveToPlayer = (target.transform.position - transform.position).normalized * projectileSpeed;
        rb.velocity = new Vector2(moveToPlayer.x, moveToPlayer.y);
    }
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;
        if(otherObject.GetComponent<Player>())
        {        
            FindObjectOfType<GameSession>().StartDeathSequance();
            Destroy(gameObject);
        } 
        if (otherObject.GetComponent<CompositeCollider2D>())
        {
            Destroy(gameObject);
        }
        
        if(otherObject.GetComponent<BreakableObject>())
        {
            Destroy(otherObject);
            Destroy(gameObject);
        }
        
        if ( otherObject.GetComponent<RightRecieverLogic>())
        {
            gameObject.tag = "All Killer";
            Rigidbody2D pulserRb = FindObjectOfType<PulserLogic>().GetComponent<Rigidbody2D>();
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            float exitHoleRotation = pulserRb.rotation - 180f;
            Vector2 velocity = rb.velocity;
            Vector3 exitHolePosition = FindObjectOfType<PulserLogic>().transform.position;
            velocity = Quaternion.Euler(0, 0, exitHoleRotation) * velocity;
            transform.position = exitHolePosition;
            rb.velocity = velocity;
        }
        if (otherObject.GetComponent<LeftRecieverLogic>())
        {
            gameObject.tag = "All Killer";
            Rigidbody2D pulserRb = FindObjectOfType<PulserLogic>().GetComponent<Rigidbody2D>();
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            float exitHoleRotation = pulserRb.rotation;
            Vector2 velocity = rb.velocity;
            Vector3 exitHolePosition = FindObjectOfType<PulserLogic>().transform.position;
            velocity = Quaternion.Euler(0, 0, exitHoleRotation) * velocity;
            transform.position = exitHolePosition;
            rb.velocity = velocity;
        }
        if (otherObject.GetComponent<Enemy>() && gameObject.tag == "All Killer")
        {
            Destroy(otherObject);
            Destroy(gameObject);
        }
        else { return; }
    }

}
