using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;

namespace Player.Scripts
{
    public class PykeSkill : PlayerBase
    {
        private GameObject _knife;
        private GameObject _eye;
        private bool _canStash;
        private bool _canDash;
        private bool _isPressed;
        private float _pressTime;
        private float _previousPressed;
        private Rigidbody2D _knife2D;
        private Transform _firePoint;
        public GameObject knifePrefab;
        public GameObject gostPrefab;
        private int _moveMode;
        private LineRenderer _lr;
        private GameObject _gost;
        private BoxCollider2D _bcol2D;
        private void Start()
        {
            _bcol2D = GetComponent<BoxCollider2D>();
            _moveMode = 0;
            isDashing = false;
            canMove = true;
            _canStash = true;
            _canDash = true;
            _knife = transform.GetChild(0).gameObject;
            _eye = transform.GetChild(2).gameObject;
            _knife2D = GameObject.FindWithTag("pyke").GetComponent<Rigidbody2D>();
            rb2D = GetComponent<Rigidbody2D>();
            _eye.SetActive(false);
        }

        private void Update()
        {
            
            BoneSkewer();
            MoveInput();
            PhantomUndertow();
        }

        private void FixedUpdate()
        {
            Move();
            
        }

        private void BoneSkewer()
        {
            _firePoint = gameObject.transform;
            if (Input.GetMouseButtonDown(0)&&_canStash)
            {
                _isPressed = true;
            }

            if (_isPressed)
            {
                _pressTime += Time.deltaTime;
                
            }

            if (_pressTime is > 0.5f and < 2.4f)
            {
                StartCoroutine(EyeGlowFlow());
            }
            
            if (Input.GetMouseButtonUp(0)&&_canStash)
            {
                _isPressed = false;
                canMove = false;
                _previousPressed = _pressTime;
                if (_previousPressed <= 0.5f&&_canStash)
                {
                    StartCoroutine(StashKnife());
                }
                else if (_previousPressed <= 3.1f&&_canStash)
                {
                    StartCoroutine(ThrowKnife(_previousPressed));
                }
                else
                {
                    canMove = true;
                }
                _pressTime = 0f;
            }
        }

        private void PhantomUndertow()
        {
            if (!_canDash)
            {
                _lr.SetPosition(0,_gost.transform.position);
                _lr.SetPosition(1,gameObject.transform.position);
            }
            if (Input.GetKeyDown(KeyCode.Space)&&_canDash)
            {
                StartCoroutine(Dash());
            }
        }

        private void GhostWaterDive()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                
            }
        }
        private IEnumerator StashKnife()
        {
            _canStash = false;
            _knife.SetActive(true);
            yield return new WaitForSeconds(2f);
            _knife.SetActive(false);
            _canStash = true;
            _previousPressed = 0f;
            canMove = true;
        }

        private IEnumerator ThrowKnife(float throwtime)
        {
            _canStash = false;
            var knife = Instantiate(knifePrefab, _firePoint.position, _firePoint.rotation);

            var rb = knife.GetComponent<Rigidbody2D>();
            var lr = knife.GetComponent<LineRenderer>();
            rb.AddForce(_firePoint.up*17f,ForceMode2D.Impulse);

            for (var i = 0f; i <= throwtime / 4f; i += Time.deltaTime)
            {

                lr.SetPosition(0,knife.transform.position);
                lr.SetPosition(1,gameObject.transform.position);
                yield return null;
            }
            rb.velocity = new Vector2(0, 0);
            Destroy(knife);
            _canStash = true;
            canMove = true;
            _eye.SetActive(false);
        }

        private IEnumerator Dash()
        {
            var gost = Instantiate(gostPrefab, _firePoint.position, _firePoint.rotation);
            var rb = gost.GetComponent<Rigidbody2D>();
            var lr = gost.GetComponent<LineRenderer>();
            _gost = gost;
            _lr = lr;
            isDashing = true;
            _canDash = false;
            _bcol2D.excludeLayers = LayerMask.GetMask("wall");
                for (var i = 0f; i <= 0.12f; i += Time.deltaTime)
                {
                    rb2D.MovePosition(Vector2.MoveTowards(gameObject.transform.position, _mousePos, 0.5f));//250 * Time.deltaTime));
                    lr.SetPosition(0,gost.transform.position);
                    lr.SetPosition(1,gameObject.transform.position);
                    yield return null;
                }
                isDashing = false;
                _bcol2D.excludeLayers = LayerMask.NameToLayer("Default"); 
                yield return new WaitForSeconds(0.82f);
                for (var i = 0f; i <= 0.12f; i += Time.deltaTime)
                {
                    rb.MovePosition(Vector2.MoveTowards(rb.position, gameObject.transform.position,0.5f));//250 * Time.deltaTime));
                    lr.SetPosition(0,gost.transform.position);
                    lr.SetPosition(1,gameObject.transform.position);
                    yield return null;
                }

                _canDash = true;
            Destroy(gost);
            rb2D.velocity = new Vector2(0, 0);

        }

        private IEnumerator EyeGlowFlow()
        {
            _eye.SetActive(true);
            for (var i = 0f; i <= 2.3f; i+=Time.deltaTime)
            {
                _eye.transform.localScale = new Vector3(i*0.22f,i*0.22f,1f);
                if (Input.GetMouseButtonUp(0))
                {
                    yield break;
                }
                yield return null;
            }

            _eye.transform.localScale = new Vector3(0.1f,0.1f,1);
            _eye.SetActive(false);
        }
        
        
    }
}


