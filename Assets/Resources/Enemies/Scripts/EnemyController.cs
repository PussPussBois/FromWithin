using UnityEngine;

namespace FromWithin.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        public float MoveSpeed { get; set; }
        public bool IsJumping { get; private set; }
        public float JumpThreshold { get; private set; }

        [SerializeField] private float initialMoveSpeed = 1f;

        [SerializeField] private float initialJumpThreshold = 0.5f;

        private Rigidbody2D _rb;
        private Vector2 _target;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            MoveSpeed = initialMoveSpeed;
            JumpThreshold = initialJumpThreshold;
            _target = transform.position;
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            var direction = _target - _rb.position;
            _rb.velocity = new Vector2
            {
                x = Mathf.Clamp(direction.x, -1f, 1f) * MoveSpeed,
                y = _rb.velocity.y
            };
            IsJumping = direction.y > JumpThreshold;
        }

        public void Move(Vector2 target)
        {
            _target = target;
        }
    }
}