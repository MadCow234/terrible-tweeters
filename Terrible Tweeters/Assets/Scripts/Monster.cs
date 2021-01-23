using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private ParticleSystem particleSystem;
    
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (ShouldDieFromCollision(collision2D))
            Die();
            
    }

    private bool ShouldDieFromCollision(Collision2D collision2D)
    {
        
        Bird bird = collision2D.gameObject.GetComponent<Bird>();

        if (bird != null)
            return true;

        if (collision2D.contacts[0].normal.y < -0.5)
            return true;
        
        return false;
    }

    private void Die()
    {
        GetComponent<SpriteRenderer>().sprite = deadSprite;
        particleSystem.Play();
        // gameObject.SetActive(false);
    }
}
