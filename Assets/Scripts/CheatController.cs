using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.UI;
using UnityEngine.TextCore.Text;

public class CheatController : MonoBehaviour
{
    private bool _isEveryObstacleEnabled;
    
    private PlayerCollision _playerCollision;
    private ObstacleDamager[] _obstacles;

    private int nonPlayableSceneCount;

    private void Start()
    {
        _playerCollision = GetComponentInChildren<PlayerCollision>();
        _obstacles = GetComponentsInChildren<ObstacleDamager>();
        
        _isEveryObstacleEnabled = true;

        nonPlayableSceneCount = 2;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TimeAndStatusManager.areCheatsUsed = true;
            SwitchInvulnerabilityMode();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            TimeAndStatusManager.areCheatsUsed = true;
            DisableObstacles();
        }
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            TimeAndStatusManager.areCheatsUsed = true;
            if (SceneManager.GetActiveScene().buildIndex - 1 != 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            TimeAndStatusManager.areCheatsUsed = true;
            if (SceneManager.GetActiveScene().buildIndex + nonPlayableSceneCount != SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
    
    private void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex > SceneManager.sceneCount)
        {
            levelIndex = 0;
        }
        SceneManager.LoadScene(levelIndex);
    }

    private void SwitchInvulnerabilityMode()
    {
        _playerCollision.IsInvulnerable = !_playerCollision.IsInvulnerable;
    }
    
    private void DisableObstacles()
    {
        _isEveryObstacleEnabled = !_isEveryObstacleEnabled;
        foreach (ObstacleDamager obstacle in _obstacles)
        {
            obstacle.ChangeState(_isEveryObstacleEnabled);
        }
    }
}
