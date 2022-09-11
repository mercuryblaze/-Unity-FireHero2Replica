using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 10;

    private Rigidbody2D _rb;
    private Vector2 _lastPlayerPosition;
    private float _lateralAcceleration;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastPlayerPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _lateralAcceleration = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 delta = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition) - _lastPlayerPosition;
            _lateralAcceleration += delta.x * _sensitivity;
            _lastPlayerPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_lateralAcceleration * _sensitivity, -4);
        _lateralAcceleration = 0;
    }
}
