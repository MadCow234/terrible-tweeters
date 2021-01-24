using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bird : MonoBehaviour
{
    [SerializeField] private float launchForce = 500f;
    [SerializeField] private float maxDragDistance = 3.5f;
    
    private Vector2 _startPosition;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    public bool IsDragging { get; private set; }

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
        IsDragging = true;
    }

    private void OnMouseUp()
    {
        var currentPosition = _rigidbody2D.position;
        var direction = _startPosition - currentPosition;
        direction.Normalize();
        
        // Add the bird back to physics control so it can be launched
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(direction * launchForce);
        
        _spriteRenderer.color = Color.white;

        IsDragging = false;
    }

    private void OnMouseDrag()
    {
        // Sync the bird position to the mouse position
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * maxDragDistance);
        }
        
        if (desiredPosition.x > _startPosition.x)
            desiredPosition.x = _startPosition.x;
        
        _rigidbody2D.position = desiredPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        
        // Reset the bird
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
    }
}
