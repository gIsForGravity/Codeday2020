using UnityEngine;
using System.Collections;

public class ClickManager : MonoBehaviour
{
    private Camera _camera;

    void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
            Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null) {
                Debug.Log(hit.collider.gameObject.name);
                hit.transform.position = new Vector2(10, 15);
            }
        }
    }

}