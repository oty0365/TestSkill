using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Entity
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] public float hp;
        [SerializeField] public float maxHp;
        [Header("상태이상")] [SerializeField] public Effects Status;
        protected Rigidbody2D rb2D;
        public void StatusCheck()
        {
            
            if (Status==Effects.Stun)
            {
                StartCoroutine(StunFlow());
                Status = Effects.Normal;
            }

            if (Status==Effects.Stealth)
            {
                
            }
            
        }
        protected IEnumerator StunFlow()
        {
            for (var i = 0f; i <= 1.5f; i += Time.deltaTime)
            {
                rb2D.velocity = Vector2.zero;
                yield return null;
            }
        }
    }
}
