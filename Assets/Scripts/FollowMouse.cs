using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Vector3 _mousePos;
    
    public float moveSpeed;

    private void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePos.z = transform.position.z;
        transform.localScale = _mousePos.x < transform.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        transform.position = Vector2.Lerp(transform.position, _mousePos, moveSpeed * Time.deltaTime);
    }
}
