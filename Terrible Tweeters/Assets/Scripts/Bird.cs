using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Vector2 _startPosition;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Cache the bird's initial position
        _startPosition = _rigidbody2D.position;
        
        // Take the bird out of physics control
        // This is so we can aim it and it does not automatically fall to the ground
        _rigidbody2D.isKinematic = true;
    }

    private void OnMouseDown()
    {
        _spriteRenderer.color = Color.red;
    }

    private void OnMouseUp()
    {
        var currentPosition = _rigidbody2D.position;
        var direction = _startPosition - currentPosition;
        direction.Normalize();
        
        // Add the bird back to physics control so it can be launched
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(direction * 500);
        
        _spriteRenderer.color = Color.white;
    }

    private void OnMouseDrag()
    {
        // Sync the bird position to the mouse position
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var transform1 = transform;
        transform1.position = new Vector3(mousePosition.x, mousePosition.y, transform1.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
