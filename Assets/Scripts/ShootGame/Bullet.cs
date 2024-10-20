using System;
using Tools;
using UnityEngine;

namespace ShootGame
{
    public class Bullet: MonoBehaviour
    {
        [ReadOnly] [SerializeField] private float speed;
        [ReadOnly] [SerializeField] private Vector3 forward;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public Bullet SetSpeed(float newSpeed)
        {
            speed = newSpeed;
            return this;
        }

        public Bullet SetForward(Vector3 newForward)
        {
            forward = newForward;
            return this;
        }
        private void Update()
        {
            transform.position += speed * Time.deltaTime * forward;
            if (!_spriteRenderer.isVisible) Die();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Words>(out var word))
            {
                word.Die();
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}