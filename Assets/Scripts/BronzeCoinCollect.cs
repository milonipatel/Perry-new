using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RunAndJump
{
    public class BronzeCoinCollect : LevelPiece
    {

        //public GameObject gm;
        //Player player;

        //AudioSource coinCollectSound;
        public delegate void StartInteractionDelegate();
        public static event StartInteractionDelegate StartInteractionEvent;
        public GameObject coinCollectionParticles;

        // Use this for initialization
        void Start()
        {
            //coinCollectSound = GetComponent<AudioSource>();

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTriggerEnter2D(Collider2D coll1)
        {
            if (StartInteractionEvent != null)
            {
                StartInteractionEvent();
            }
          //  if (coll1.gameObject.tag == ("Player"))
            //{
                Debug.Log("COIN COLLECT");
            //coinCollectSound.Play();

                GetComponent<AudioSource>().Play();
                GameObject clone = Instantiate(coinCollectionParticles, transform.position, transform.rotation);
                Destroy(gameObject);
                Destroy(clone, 1f);
          //  }
        }
    }
}