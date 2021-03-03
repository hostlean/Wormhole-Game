using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject firePos;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float projectileForce = 200f;
    Vector2 playerPos;
    Rigidbody2D rb;
    public Transform player;
    float nextFire;
    [SerializeField] float fireRate = 1f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveSpeed, 0f);
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = playerPos - rb.position;
        /*rb.velocity = new Vector2(moveSpeed, 0f);*/


        /*private void OnTriggerExit2D(Collider2D collision)
        {
            moveSpeed = -moveSpeed;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }*/
    }
    public void Fire()
    {
        if (Time.time > nextFire)
        {
            GameObject newProjectile =
            Instantiate(projectile, firePos.transform.position, firePos.transform.rotation);
            //Rigidbody2D bullet = projectile.GetComponent<Rigidbody2D>();
            //bullet.AddForce(firePos.transform.right * projectileForce * Time.deltaTime, ForceMode2D.Impulse);
            nextFire = Time.time + fireRate;
        }
    }


}
