using System.Collections;
using System.Collections.Generic;
using Player.Scripts;
using UnityEngine;

namespace Player.Scripts.Dummy
{
    public class DummyHixBox : PlayerBase
    {
        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            
        }

        private void Update()
        {
        
        }
    }
}

