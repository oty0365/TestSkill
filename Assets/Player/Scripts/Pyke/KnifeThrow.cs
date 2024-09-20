using System;
using System.Collections;
using UnityEngine;

namespace Player.Scripts.Pyke
{
    public class KnifeThrow : MonoBehaviour
    {
        private Rigidbody2D _rb2D;
        private SpriteRenderer _sr;
        private LineRenderer _lr;
        private BoxCollider2D _bcol2D;
        private Rigidbody2D _grapedbody2D;
        private GameObject _pyke;
        private void Start()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _lr = GetComponent<LineRenderer>();
            _bcol2D = GetComponent<BoxCollider2D>();
            _pyke = GameObject.FindWithTag("pyke");
        }

        private void Update()
        {
            
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("enemy"))
            {
                _sr.color = new Color(255, 255, 255, 0);
                _lr.enabled = false;
                _bcol2D.enabled = false;
                var graped = other.gameObject;
                var grapedbody2D = graped.GetComponent<Rigidbody2D>();
                _grapedbody2D = grapedbody2D;
                StartCoroutine(PullOpponentFlow());
            }
        }

        private IEnumerator PullOpponentFlow()
        {
            for (var i = 0f; i <= 0.6; i += Time.deltaTime)
            {
                _grapedbody2D.MovePosition(Vector2.MoveTowards(_grapedbody2D.position, _pyke.transform.position, 0.3f));//100f*Time.deltaTime));
                yield return null;
            }
            //Destroy(gameObject);
        }
    }
}
