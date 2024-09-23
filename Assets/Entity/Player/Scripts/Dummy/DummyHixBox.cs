using System;
using System.Collections;
using System.Collections.Generic;
using Player.Scripts;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player.Scripts.Dummy
{
    public class DummyHixBox : PlayerBase
    {
        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            Status = Effects.Normal;
        }

        private void Update()
        {
            StatusCheck();
        }
    }
}

