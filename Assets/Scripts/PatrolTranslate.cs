using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RunAndJump
{
    public class PatrolTranslate : LevelPiece
    {

        [SerializeField]
        float moveSpeed = 5f;

        [SerializeField]
        float distanceCovered = 7f;

        [SerializeField]
        float frequency = 10f;

        [SerializeField]
        float magnitude = 0.5f;

        bool facingRight = true;
        bool move = true;

        float startPosition;
        Vector3 pos, localScale, currentPosition, stopPosition;

        // Use this for initialization
        void Start()
        {
            //currentPosition = gameObject.transform.position;

            pos = transform.position;
            localScale = transform.localScale;
            startPosition = pos.x;
            //Debug.Log("in start of patrol sinu and startposition is " + startPosition);

        }

        // Update is called once per frame
        void Update()
        {
            if (move)
            {
                Movement();
            }
            else
            {
                stopPosition = new Vector3(currentPosition.x, currentPosition.y, 0);
                gameObject.transform.position = stopPosition;

            }


        }

        void CheckWhereToFace()
        {
            if (pos.x < startPosition - distanceCovered)
                facingRight = true;

            else if (pos.x > startPosition + distanceCovered)
                facingRight = false;

            if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
                localScale.x *= -1;

            transform.localScale = localScale;

        }

        void MoveRight()
        {
            pos += transform.right * Time.deltaTime * moveSpeed;
            transform.position = pos + transform.right ;
        }

        void MoveLeft()
        {
            pos -= transform.right * Time.deltaTime * moveSpeed;
            transform.position = pos + transform.right;
        }

        void Movement()
        {
            CheckWhereToFace();

            if (facingRight)
                MoveRight();
            else
                MoveLeft();
        }

        public void stopPatrol()
        {
            Debug.Log("stopPatrol successfully called");
            currentPosition = gameObject.transform.position;
            //Debug.Log("The current position in stopPatrol is " + currentPosition);
            move = false;
        }

        public void startPatrol()
        {
            Debug.Log("startPatrol successfully called");
            move = true;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == ("Player"))
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                player.StartPlayerDeath();
            }
            //Destroy(gameObject);

        }
    }
}