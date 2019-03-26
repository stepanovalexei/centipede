using GameLogic;
using UnityEngine;

namespace MapLogic
{
    public class Mushroom : MonoBehaviour
    {
        private int hp = 2;
        private void OnCollisionEnter(Collision other)
        {
            var bullet = other.collider.GetComponent<Bullet>();
            if (bullet)
            {
                hp--;

                if (hp == 0)
                {
                    Destroy(transform.gameObject);
                }
            }
        }

    }
}