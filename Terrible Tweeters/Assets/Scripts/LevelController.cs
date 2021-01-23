using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    
    private Monster[] _monsters;

    private void OnEnable()
    {
        _monsters = FindObjectsOfType<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MonstersAreAllDead())
            GoToNextLevel();
    }

    private void GoToNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

    private bool MonstersAreAllDead()
    {
        foreach (Monster monster in _monsters)
        {
            if (monster.gameObject.activeSelf)
                return false;
        }

        return true;
    }
}
