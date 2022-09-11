using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    private float _speedMovement = 1.5f;
    private float _minPositionY;
    private Vector2 _startingPosition;

    private void Awake()
    {
        _startingPosition = transform.position;
        _minPositionY = _sprite.bounds.size.y - _startingPosition.y;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * _speedMovement * Time.deltaTime);

        if (transform.position.y <= -_minPositionY)
        {
            transform.position = _startingPosition;
        }
    }
}
