using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClearSky
{
    public class SimplePlayerController : MonoBehaviour
    {
        public float movePower = 10f;
        public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5
        
        [SerializeField]
        private int health = 100;

        public DeathUI UiDeath;

        public int Health {
            get => health;
            set
            {
                HealthBar.AdjustCurrentValue(value - health);
                health = value;
                if (health <= 0)
                {
                    Die();
                }
            }
        }
        public bool isDamaged = false;
        private Rigidbody2D rb;
        private Animator anim;
        Vector3 movement;
        private int direction = 1;
        bool isJumping = false;
        private bool alive = true;
        public bool isAlive => alive;

        public int Direction => direction;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            RestartLevel();
            if (alive)
            {
                Run();
            }
        }

        private void Update()
        {
            if (!alive) return;
            Jump();
            HurtMe();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {

            anim.SetBool("isJump", false);
        }

        private void RestartLevel()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        private void HurtMe()
        {
            if (!isDamaged) return;
            anim.SetTrigger("hurt");
            rb.AddForce(direction == 1 ? new Vector2(-5f, 1f) : new Vector2(5f, 1f), ForceMode2D.Impulse);
            Health -= 1;
            isDamaged = false;
        }

        void Run()
        {
            var moveVelocity = Vector3.zero;
            anim.SetBool("isRun", false);

            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                direction = -1;
                moveVelocity = Vector3.left;

                transform.localScale = new Vector3(direction, 1, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                direction = 1;
                moveVelocity = Vector3.right;

                transform.localScale = new Vector3(direction, 1, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

            }
            transform.position += moveVelocity * movePower * Time.deltaTime;
            
        }
        void Jump()
        {
            if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
                && !anim.GetBool("isJump") && Time.timeScale != 0)
            {
                JumpMe();
            }
        }

        public void JumpMe()
        {
            if (!anim.GetBool("isJump"))
            {
                isJumping = true;
                anim.SetBool("isJump", true);
            }
            if (!isJumping)
            {
                return;
            }

            rb.velocity = Vector2.zero;

            var jumpVelocity = new Vector2(0, jumpPower);
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
            isJumping = false;
        }
        public void Attack()
        {
            anim.SetTrigger("attack");
        }
        void Hurt()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                anim.SetTrigger("hurt");
                rb.AddForce(direction == 1 ? new Vector2(-5f, 1f) : new Vector2(5f, 1f), ForceMode2D.Impulse);
            }
        }
        void Die()
        {
            anim.SetTrigger("die");
            alive = false;
            UiDeath.Show();
        }
        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                anim.SetTrigger("idle");
                alive = true;
            }
        }
    }
}