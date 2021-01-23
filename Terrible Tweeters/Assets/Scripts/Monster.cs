using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
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

        return false;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
