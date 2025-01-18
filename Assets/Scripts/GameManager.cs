using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;

    void Update()
    {   if (GameIsOver)
        {
            return;
        }
        
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        GameIsOver = true;
        Debug.Log("Game Over");
    }
}
