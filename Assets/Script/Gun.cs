using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject firePos;
    [SerializeField] float fireRate = 1f;
    [SerializeField] float projectileForce = 20f;
    float nextFire;
    Rigidbody2D rb;
    Vector2 playerPos;
    Player player;



    private void Start()
    {
        player = FindObjectOfType<Player>();
        nextFire = Time.time;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 lookDir = playerPos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    /*public void Fire()
    {
        if (Time.time > nextFire)
        {
            GameObject newProjectile =
            Instantiate(projectile, firePos.transform.position, firePos.transform.rotation);
            nextFire = Time.time + fireRate;
            //Rigidbody2D bullet = projectile.GetComponent<Rigidbody2D>();
             //bullet.AddForce(firePos.transform.right * projectileForce, ForceMode2D.Force);
        }

    }*/


}
