using UnityEngine;


namespace SoloGames.Tools
{
    public class ScrollMaterial : MonoBehaviour
    {
        [SerializeField] private float _scrollX = 0.5f;
        [SerializeField] private float _scrollY = 0.5f;

        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void Scroll()
        {
            if (_renderer == null) return;
            float offsetX = Time.time * _scrollX;
            float offsetY = Time.time * _scrollY;
            _renderer.material.mainTextureOffset = new Vector2(offsetX, offsetY);
        }

        private void Update()
        {
            Scroll();
        }

    }
}
