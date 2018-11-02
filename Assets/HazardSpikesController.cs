using UnityEngine;
using System.Collections;

namespace RunAndJump
{

    public class HazardSpikesController : LevelPiece
    {

        public AudioClip PlayerLoseFx;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == ("Player"))
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                player.StartPlayerDeath();
            }
        }
    }
}