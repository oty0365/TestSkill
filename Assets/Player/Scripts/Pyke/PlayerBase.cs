using UnityEngine;

namespace Player.Scripts
{
    public class PlayerBase : MonoBehaviour
    {
        [SerializeField] protected Camera mainCam;
        [SerializeField] protected float moveSpeed;
        [Header("대쉬가능 여부")] [SerializeField] protected bool ableToDash;
        protected bool isDashing;
        public Vector3 _mousePos;
        private float _horizontal;
        private float _vertical;
        protected Rigidbody2D rb2D;
        protected bool canMove;

        protected void MoveInput()
        {
            if (!canMove) return;
            _mousePos = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition);
            var dir = Mathf.Atan2(_mousePos.y - transform.position.y, _mousePos.x - transform.position.x);
            var dirAngle = (180 / Mathf.PI) * dir - 90;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, dirAngle);
            if (ableToDash && isDashing) return;
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");
        }

        protected void Move()
        {
            rb2D.velocity = !canMove ? new Vector2(0, 0) : new Vector2(_horizontal*moveSpeed, _vertical*moveSpeed);
            
        }
    }
}
