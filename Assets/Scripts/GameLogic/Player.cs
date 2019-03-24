using UnityEngine;

namespace GameLogic
{
    public class Player : MonoBehaviour
    {
        private const float bulletOffset = 0.5f;
        
        [SerializeField] private float speed;
        [SerializeField] private float bulletForce;
        [SerializeField] private GameObject bulletPrefab;

        private Rigidbody rigidbody;
        private CharacterController characterController;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            characterController = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            Move();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }

        private void Move()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var moveDirection = new Vector3(horizontal, vertical, 0f);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            characterController.Move(moveDirection * Time.deltaTime);
        }

        private void Shoot()
        {
            // Spawn bullet just above the player
            var spawnPosition = new Vector3(transform.position.x, transform.position.y + bulletOffset,
                transform.position.z);
            var bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            var bulletRigid = bullet.GetComponent<Rigidbody>();

            bulletRigid.velocity = Vector3.up * bulletForce;
        }
    }
}