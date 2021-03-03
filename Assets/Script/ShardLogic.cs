using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShardLogic : MonoBehaviour
{
    int currentSceneIndex;
    Vector2 zeroVelocity;
    Player player;
    void Start()
    {
        zeroVelocity = new Vector2(0, 0);
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;
        if(otherObject.GetComponent<Player>())
        {
            StartCoroutine(WaitNextLevelLoad());
        }
    }

    IEnumerator WaitNextLevelLoad()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = zeroVelocity;
        player.GetComponent<Animator>().SetTrigger("GoesAway");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void Update()
    {
        
    }
}
