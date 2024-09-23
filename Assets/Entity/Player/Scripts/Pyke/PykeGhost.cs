using System;
using System.Linq;
using UnityEngine;

namespace Player.Scripts.Pyke
{
    public class PykeGhost : PlayerBase
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("entity"))
            {
                var e = other.GetComponent<Entity.Entity>();
                Debug.Log(e.Status);
                e.Status=Effects.Stun;
                e.hp -= 10f;
            }
        }
    }
}
