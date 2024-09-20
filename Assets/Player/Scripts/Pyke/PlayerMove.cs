using UnityEngine;

namespace Player.Scripts
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Camera mainCam;
        private Vector3 _mousePos;
        private float _horizontal;
        private float _vertical;
        private Rigidbody2D _rb2D;
        [SerializeField] private float moveSpeed;
        private void Start()
        {
            _rb2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");
            _mousePos = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition);
            var dir = Mathf.Atan2(_mousePos.y-transform.position.y, _mousePos.x-transform.position.x);
            var dirAngle = (180 / Mathf.PI) * dir - 90;
            gameObject.transform.rotation = Quaternion.Euler(0,0,dirAngle);
        }

        private void FixedUpdate()
        {
            _rb2D.velocity = new Vector2(_horizontal*moveSpeed, _vertical*moveSpeed);
        }
    }
}
