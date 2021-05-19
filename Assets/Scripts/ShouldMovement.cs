using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShouldMovement : MonoBehaviour
{
    void Update() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.x -= objectPos.x;
        mousePosition.y -= objectPos.y;

        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        if (transform.parent.transform.localScale.x < 0) {
            angle += 180;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
