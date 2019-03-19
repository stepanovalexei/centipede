using UnityEngine;

namespace Game_Logic
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Rigidbody rigidbody;
        private CharacterController characterController;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            characterController = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var moveDirection = new Vector3(horizontal, vertical, 0f);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            characterController.Move(moveDirection * Time.deltaTime);
        }
    }
}

