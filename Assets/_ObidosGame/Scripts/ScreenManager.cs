using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;

    [SerializeField] StartScreen startScreen;
    [SerializeField] GameScreen gameScreen;
    [SerializeField] GameOverScreen gameOverScreen;
    [SerializeField] WaitingForInputScreen waitingForInputScreen;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }

        DisableAll();
    }

    public void EnableStartScreen()
    {
        startScreen.gameObject.SetActive(true);
    }

    public void DisableStartScreen()
    {
        startScreen.gameObject.SetActive(false);
    }

    public void EnableGameScreen()
    {
        ScreenManager.Instance.DisableStartScreen();
        ScreenManager.Instance.DisableWaitingForInputScreen();
        gameScreen.gameObject.SetActive(true);
    }

    public void DisableGameScreen()
    {
        gameScreen.gameObject.SetActive(false);
    }


    public void EnableGameOverScreen()
    {
        DisableAll();
        gameOverScreen.gameObject.SetActive(true);
        gameOverScreen.GameOverSet(false);
    }

    public void DisableGameOverScreen()
    {
        gameOverScreen.gameObject.SetActive(false);
    }

    public void DisableAll()
    {
        startScreen.gameObject.SetActive(false);
        gameScreen.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);
        waitingForInputScreen.gameObject.SetActive(false);
    }

    public void EnableWaitingForInputScreen()
    {
        gameScreen.gameObject.SetActive(false);
        waitingForInputScreen.gameObject.SetActive(true);
    }

    public void DisableWaitingForInputScreen()
    {
        waitingForInputScreen.gameObject.SetActive(false);
    }

    internal GameScreen GetGameScreen() => gameScreen;

    internal void ShowGameSuccessScreen()
    {
        DisableAll();
        gameOverScreen.gameObject.SetActive(true);
        gameOverScreen.GameOverSet(true);
    }
}
