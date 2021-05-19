using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    [SerializeField] private float defDistance = 100;
    [SerializeField] private LayerMask layerMask;
    public LineRenderer m_lineRenderer;
    Transform m_transform;

    private void Awake() {
        m_transform = GetComponent<Transform>();
    }

    private void Update() {
        m_lineRenderer.sortingLayerName = "Foreground";
        m_lineRenderer.sortingOrder = 0;
        if (Input.GetKey(KeyCode.Mouse0)) {
            DrawLaser();
        }
        else {
            m_lineRenderer.positionCount = 0;
        }
    }

    void DrawLaser() {
        Vector2 position = m_transform.position;
        Vector2 rotation = m_transform.transform.right;

        if (transform.parent.transform.parent.transform.parent.transform.localScale.x < 0) {
            rotation *= -1;
        }

        const int maxPositions = 10;
        Vector3[] positions = new Vector3[maxPositions];
        positions[0] = new Vector3(position.x, position.y, 2);
        int positionCount = 1;

        bool contact = true;

        RaycastHit2D result;
        for (positionCount = 2; positionCount <= maxPositions && contact; positionCount++) {
            result = Physics2D.Raycast(position, rotation, Mathf.Infinity, layerMask);
            if (result.collider != null) {                
                float xOffset = 0.02f;
                if (rotation.x > 0) {
                    xOffset *= -1;
                }

                float yOffset = 0.01f;
                if (rotation.y > 0) {
                    yOffset *= -1;
                }

                position = new Vector2(result.point.x + xOffset, result.point.y + yOffset);
                rotation = Vector2.Reflect(rotation, result.normal);
                positions[positionCount - 1] = new Vector3(position.x, position.y, 2);

                if (result.collider.tag == "player") {
                    contact = false;
                }
            }
            else {
                position = rotation * defDistance;
                positions[positionCount - 1] = new Vector3(position.x, position.y, 2);
                contact = false;
            }
        }

        m_lineRenderer.positionCount = positionCount - 1;
        m_lineRenderer.SetPositions(positions);
    }
}
