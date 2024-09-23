using Unity.VisualScripting;
using UnityEngine;

namespace Entity.Player.Scripts.Pyke
{
    public class PykeExecution : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("entity"))
            {
                var o = other.gameObject;
                Debug.Log(o);
                var e = o.GetComponent<Entity>();
                if (e.hp <= e.maxHp * 0.3)
                {
                    Destroy(o);
                }
                else
                {
                    e.hp -= 20f;
                }
            }
        }
    }
}
