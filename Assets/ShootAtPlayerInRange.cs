using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunAndJump;

public class ShootAtPlayerInRange : MonoBehaviour {

    public float playerRange;
    public GameObject bullet;
    public PlayerController player;
    public Transform launchPoint;
    public float waitBetweenShots;
    private float shotCounter;
    public AudioClip shotSound;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        shotCounter = waitBetweenShots;
    }
    private void Update()
    {
        shotCounter -= Time.deltaTime;

        Debug.DrawLine(new Vector3(transform.position.x - playerRange, transform.position.y, transform.position.z), new Vector3(transform.position.x + playerRange, transform.position.y, transform.position.z));

        if (transform.localScale.x < 0 && player.transform.position.x > transform.position.x && player.transform.position.x < transform.position.x + playerRange && shotCounter < 0)
        {
            GameObject clone = Instantiate(bullet, launchPoint.position, launchPoint.rotation);
            Destroy(clone, 5f);
            shotCounter = waitBetweenShots;
            AudioPlayer.Instance.PlaySfx(shotSound);
        }

        if (transform.localScale.x > 0 && player.transform.position.x < transform.position.x && player.transform.position.x > transform.position.x - playerRange && shotCounter < 0)
        {
            GameObject clone = Instantiate(bullet, launchPoint.position, launchPoint.rotation);
            Destroy(clone, 5f);
            shotCounter = waitBetweenShots;
            AudioPlayer.Instance.PlaySfx(shotSound);
        }
    }
}
