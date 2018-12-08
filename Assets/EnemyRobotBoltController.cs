using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunAndJump;

public class EnemyRobotBoltController : MonoBehaviour {

    public float speed;
    public PlayerController player;
    public GameObject hitEffect;
    private Rigidbody2D myRigidBody;
    //private AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        myRigidBody = GetComponent<Rigidbody2D>();

        if (player.transform.position.x < transform.position.x)
        {
            speed = -speed;
            transform.localScale *= -1;
        }

        //if (audioManager == null)
        //{
        //    Debug.LogError("FREAK OUT! No AudioManager found in the scene.");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.velocity = new Vector2(speed, myRigidBody.velocity.y);
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // kill player
            //audioManager.PlaySound("Shot");
            player.StartPlayerDeath();
            Destroy(gameObject);
            GameObject clone = Instantiate(hitEffect, collision.transform.position, collision.transform.rotation);
            Destroy(clone);
        }
        

    }
}
