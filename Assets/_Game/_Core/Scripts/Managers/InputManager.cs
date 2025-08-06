using System;
using MoreMountains.Tools;
using UnityEngine;

namespace SoloGames.Managers
{
    public class InputManager : MMSingleton<InputManager>
    {
        [SerializeField] private float _swipeThreshold = 25f;

        public bool IsSwiping => _isSwiping;
        public static event Action<Vector2> OnSwipe;      // swipe direction (normalized)
        public static event Action OnTap;                // simple tap/click

        private Vector2 _startTouchPosition;
        private Vector2 currentTouchPosition;
        protected bool _isSwiping = false;
        protected bool _isMobileDevice = false;

        private void HandleMouseSwipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isSwiping = true;
                _startTouchPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0) && _isSwiping)
            {
                currentTouchPosition = Input.mousePosition;
                DetectSwipe();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isSwiping = false;
            }
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount == 0) return;

            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _isSwiping = true;
                    _startTouchPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    currentTouchPosition = touch.position;
                    DetectSwipe();
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _isSwiping = false;
                    break;
            }
        }
        
        private void DetectSwipe()
        {
            Vector2 delta = currentTouchPosition - _startTouchPosition;
            if (delta.magnitude >= _swipeThreshold)
            {
                Vector2 direction = delta.normalized;
                OnSwipe?.Invoke(direction);
                _isSwiping = false; // reset swipe
            }
        }

        private void Update()
        {
            // if (_isMobileDevice)
            //     HandleTouchSwipe();
            // else
            HandleMouseSwipe();
        }
    }
}
