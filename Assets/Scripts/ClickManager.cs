using UnityEngine;

namespace Scripts
{
    public class ClickManager : MonoBehaviour
    {
        private Camera _camera;

        void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Raycast");
                Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider == null) return;
                Debug.Log("Raycast hit");
                var mirror = hit.collider.GetComponent<MirrorComponent>();

                if (mirror)
                    mirror.Rotate();
            }
        }

    }
}
