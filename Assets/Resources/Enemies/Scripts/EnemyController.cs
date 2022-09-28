using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;

namespace FromWithin.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private float initialMoveSpeed = 1f;

        [SerializeField]
        private float jumpThreshold = 0.5f;

        private Rigidbody2D _rb;

        public float MoveSpeed { get; private set; }
        public bool IsJumping { get; private set; }

        private Vector2 _target;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            MoveSpeed = initialMoveSpeed;
            _target = transform.position;
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            var currentPosition = _rb.position;
            var newPosition = Vector2.MoveTowards(
                currentPosition, _target, MoveSpeed * Time.deltaTime);

            IsJumping = _target.y - currentPosition.y >= jumpThreshold;

            _rb.position = new Vector2
            {
                x = newPosition.x,
                y = 0
            };
        }

        public void Move(Vector2 target)
        {
            _target = target;
        }
    }
}