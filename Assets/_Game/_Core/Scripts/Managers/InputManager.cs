using SoloGames.Patterns;
using UnityEngine;

namespace SoloGames.Managers
{
    public class InputManager : MonoSingleton<InputManager>
    {
        [SerializeField] private float _minSwipeDistance = 25f;

        public Vector3 LateralDirection => _lateralDirection;
        public bool IsSwiping => _isSwiping;

        protected Vector3 _lateralDirection = Vector3.zero;
        protected Vector2 _swipeStart;
        protected bool _isSwiping = false;
        protected bool _isMobileDevice = false;
        
        private void HandleTouchSwipe()
        {
            if (Input.touchCount == 0) return;

            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _swipeStart = touch.position;
                    _isSwiping = true;
                    break;

                case TouchPhase.Ended:
                    if (!_isSwiping) return;

                    Vector2 swipeEnd = touch.position;
                    Vector2 delta = swipeEnd - _swipeStart;

                    if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y) && Mathf.Abs(delta.x) > _minSwipeDistance)
                    {
                        if (delta.x > 0)
                            _lateralDirection = Vector3.right;
                        else
                            _lateralDirection = Vector3.left;
                    }
                    _isSwiping = false;
                    break;
            }
        }

        private void HandleMouseSwipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _swipeStart = Input.mousePosition;
                _isSwiping = true;
            }

            if (Input.GetMouseButton(0) && _isSwiping)
            {
                Vector2 swipeEnd = (Vector2)Input.mousePosition;
                Vector2 delta = swipeEnd - _swipeStart;

                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y) && Mathf.Abs(delta.x) > _minSwipeDistance)
                {
                    if (delta.x > 0)
                        _lateralDirection = Vector3.right;
                    else
                        _lateralDirection = Vector3.left;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    _isSwiping = false;
                }
            }
        }

        private void Update()
        {
            if (_isMobileDevice)
                HandleTouchSwipe();
            else
                HandleMouseSwipe();
        }
    }
}
