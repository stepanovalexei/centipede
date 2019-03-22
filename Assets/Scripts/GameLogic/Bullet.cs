using UnityEngine;

namespace GameLogic
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            print("Collided!");
            Destroy(gameObject);
        }
    }
}