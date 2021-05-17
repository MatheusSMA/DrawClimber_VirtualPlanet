using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    //Script para reniciar o game
    //Script to restart game
    public void Restart()
    {
        SceneManager.LoadScene("Level1");
    }
}
