
using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace RunAndJump
{
    public class PlayerController : LevelPiece
    {
        public Animator animator;
        public float speed = 300f;
        public float jumpSpeed = 30f;
        public float jumpSpeedX = 50f;
        private float movement = 0f;
        public float fallboundry = -20f;
        private Rigidbody2D rigidBody;
        public AudioClip toJump;


        private bool _facingRight = true;
        // private float _xVel;
        // private float _yVel;
        private Collider2D _collider;
        // private Rigidbody2D _groundRigidBody;
        //  private Animator _anim;
        private bool _playerDied = false;


        public delegate void PlayerDeathDelegate();
        public static event PlayerDeathDelegate PlayerDeathEvent;
        // Use this for initialization
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            movement = CrossPlatformInputManager.GetAxis("Horizontal");
            animator.SetFloat("Speed", Mathf.Abs(movement));

            if (movement <= -0.5)
            {
                rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            }
            else if (movement >= 0.5f)
            {
                rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                rigidBody.velocity = new Vector2(jumpSpeedX, jumpSpeed);
                animator.SetBool("IsJumping", true);
            }

            if (CrossPlatformInputManager.GetButtonUp("Jump"))
            {
                rigidBody.velocity = new Vector2(jumpSpeedX, jumpSpeed);
                animator.SetBool("IsJumping", false);
                AudioPlayer.Instance.PlaySfx(toJump);
            }

            if (gameObject.transform.position.y < fallboundry)
            {
                StartPlayerDeath();
            }
            if (movement > 0 && !_facingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (movement < 0 && _facingRight)
            {
                // ... flip the player.
                Flip();
            }

            //Flip();

        }

        public void StartPlayerDeath()
        {
            Debug.Log("StartPlayerDeath called...");
            if (!_playerDied)
            {
                _playerDied = true;
                if (PlayerDeathEvent != null)
                {
                    PlayerDeathEvent();
                }
            }
        }

        public void OnLanding()
        {
            animator.SetBool("IsJumping", false);
        }

        private void Flip()
        {
            _facingRight = !_facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}