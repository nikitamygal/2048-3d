using System;
using UnityEngine;


namespace SoloGames.Services
{
    public class InputService : MonoBehaviour
    {
        public static event Action<bool> OnTap;
        public static bool IsDragging { get; private set; } = false;
        public static Vector3 DragWorldPosition { get; private set; }

        private bool _isSwiping = false;
        private bool _isMobileDevice = false;

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isSwiping = true;
                IsDragging = true;
                DetectTap(false);
            }
            else if (Input.GetMouseButton(0) && _isSwiping)
            {
                UpdateDragPosition(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isSwiping = false;
                IsDragging = false;
                DetectTap(true);
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
                    IsDragging = true;
                    DetectTap(false);
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    UpdateDragPosition(touch.position);
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _isSwiping = false;
                    IsDragging = false;
                    DetectTap(true);
                    break;
            }
        }

        private void UpdateDragPosition(Vector2 screenPos)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            if (plane.Raycast(ray, out float distance))
            {
                DragWorldPosition = ray.GetPoint(distance);
            }
        }

        private void DetectTap(bool isUp)
        {
            OnTap?.Invoke(isUp);
        }

        private void Update()
        {
            if (_isMobileDevice)
                HandleTouchInput();
            else
                HandleMouseInput();
        }
    }
}
