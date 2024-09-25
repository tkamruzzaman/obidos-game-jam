using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartGame(){
        //show the start screen
    }

    public void EndGame(){
       //show the game over screen
       
    }
}
