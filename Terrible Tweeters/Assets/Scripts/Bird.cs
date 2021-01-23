using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Vector2 _startPosition;

    // Start is called before the first frame update
    private void Start()
    {
        // Cache the bird's initial position
        _startPosition = GetComponent<Rigidbody2D>().position;
        
        // Take the bird out of physics control
        // This is so we can aim it and it does not automatically fall to the ground
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void OnMouseUp()
    {
        Vector2 currentPosition = GetComponent<Rigidbody2D>().position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();
        
        // Add the bird back to physics control so it can be launched
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddForce(direction * 500);
        
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnMouseDrag()
    {
        // Sync the bird position to the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
