using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Action<bool> OnGameOver = delegate { };
    public static GameManager Instance;

    public bool isPhoneRang;
    public bool isIntroEnded;
    public bool isGameStarted;
    public bool isGameEnded;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartIntro();
        InputManager.Instance.OnWaitingForInputCompleted += OnWaitingForInputCompleted;

    }

    public void StartIntro()
    {
        //show the start screen
        ScreenManager.Instance.EnableStartScreen();
    }

    public void StartGame()
    {
        isGameStarted = true;

        ScreenManager.Instance.EnableGameScreen();
    }

    private void OnWaitingForInputCompleted()
    {
        print("oooooooooooooooooooooooooooooooooo");
        Play();
    }

    public int doneClipNo;
    public void Play()
    {
        ScreenManager.Instance.EnableGameScreen();
        if (doneClipNo < 3)
        {
            //play wrong
            ScreenManager.Instance.GetGameScreen().Play(doneClipNo);
        }

        //play right
        else
        {
            ConversationManager.Instance.PlayRightNumber(0);
        }
    }

    public int GetCurrentWrongNumberIndex() { return doneClipNo; }

    public void EndGame()
    {
        isGameEnded = true;
        //show the game over screen
        print("GAME OVER!");

        ScreenManager.Instance.EnableGameOverScreen();
        OnGameOver?.Invoke(false);
    }

    public void GameSuccess()
    {
        isGameEnded = true;
        print("SUCCESS!!!!!!!!");
        ScreenManager.Instance.ShowGameSuccessScreen();
        OnGameOver?.Invoke(true);
    }
}
