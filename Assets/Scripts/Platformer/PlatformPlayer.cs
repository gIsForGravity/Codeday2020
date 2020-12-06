using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Platformer
{
    public class PlatformPlayer : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Animator _animator;
        private BoxCollider2D _collider;
        private static readonly int Moving = Animator.StringToHash("Moving");

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            Vector2 velocity = new Vector2();
            bool moving = false;
            bool grounded = IsGrounded();

            if (!(_rb.velocity.x < 0 && Mathf.Abs(_rb.velocity.x) > 5f))
                if (Input.GetKey(KeyCode.A))
                {
                    velocity += Vector2.left * 10f;
                    moving = true;
                }

            if (!(_rb.velocity.x > 0 && Mathf.Abs(_rb.velocity.x) > 5f))
                if (Input.GetKey(KeyCode.D))
                {
                    velocity += Vector2.right * 10f;
                    moving = true;
                }

            if (!grounded) moving = false;
            
            _animator.SetBool(Moving, moving);
            _rb.AddForce(velocity);
            
            if (grounded && Input.GetKeyDown(KeyCode.W))
                _rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);

            if (IsTouchingRobot())
            {
                SceneManager.LoadScene("MainScene");
            }
        }

        private bool IsTouchingRobot()
        {
            bool rightCast = false;
            var raycast = Physics2D.Raycast(_collider.bounds.center, Vector2.right, _collider.bounds.extents.x + 0.3f, ~(1<<LayerMask.NameToLayer("Player")));
            if (raycast.collider != null && raycast.transform.gameObject.layer == LayerMask.NameToLayer("Robot"))
            {
                Debug.DrawRay(_collider.bounds.center, Vector2.right * (_collider.bounds.extents.x + 0.3f), Color.green);
                rightCast = true;
            }
            else
                Debug.DrawRay(_collider.bounds.center, Vector2.right * (_collider.bounds.extents.x + 0.3f), Color.red);
            
            bool leftCast = false;
            raycast = Physics2D.Raycast(_collider.bounds.center, Vector2.left, _collider.bounds.extents.x + 0.3f, ~(1<<LayerMask.NameToLayer("Player")));
            if (raycast.collider != null && raycast.transform.gameObject.layer == LayerMask.NameToLayer("Robot"))
            {
                Debug.DrawRay(_collider.bounds.center, Vector2.left * (_collider.bounds.extents.x + 0.3f), Color.green);
                leftCast = true;
            }
            else
                Debug.DrawRay(_collider.bounds.center, Vector2.left * (_collider.bounds.extents.x + 0.3f), Color.red);

            //return leftCast || rightCast;
            return rightCast;
        }

        private bool IsGrounded()
        {
            var raycast = Physics2D.Raycast(_collider.bounds.center, Vector2.down, _collider.bounds.extents.y + 0.1f, ~(1<<LayerMask.NameToLayer("Player")));
            Color rayColor;
            bool hit;
            if (raycast.collider != null)
            {
                rayColor = Color.green;
                hit = true;
            }
            else
            {
                rayColor = Color.red;
                hit = false;
            }

            Debug.DrawRay(_collider.bounds.center, Vector2.down * (_collider.bounds.extents.y + 0.1f), rayColor);
            return hit;
        }
    }
}