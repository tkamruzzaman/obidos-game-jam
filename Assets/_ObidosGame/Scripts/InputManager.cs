using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[DefaultExecutionOrder(-1)]

public class InputManager : MonoBehaviour
{
    public Action OnWaitingForInputCompleted = delegate { };

    public static InputManager Instance;
    private SerialController serialController;
    public bool IsHungup { get; private set; }

    public bool IsWaitingForInput { get; set; }

    string playerDialInput;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        serialController = FindObjectOfType<SerialController>();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     Debug.Log("Sending some action");
        // }
    }


    void OnConnectionEvent(bool success)
    {
        print("is connected: " + success);
    }

    void OnMessageArrived(string msg)
    {
        msg = msg.Trim();

        switch (msg)
        {
            case "up":
                print("up");
                if (GameManager.Instance.isPhoneRang
                && !GameManager.Instance.isGameStarted
                && !GameManager.Instance.isGameEnded
                && !IsWaitingForInput)
                {
                    //GameManager.Instance.StartGame();
                    //wait to dial the number
                    StartCoroutine(StartOparator());
                }
                break;
            case "down":
                print("down");
                IsHungup = true;
                GameManager.Instance.EndGame();
                break;
            case "0":
                //print("0");
                break;
            case "1":
                //print("1");
                break;
            case "2":
                //print("2");
                break;
            case "3":
                // print("3");
                break;
            case "4":
                //  print("4");
                break;
            case "5":
                // print("5");
                break;
            case "6":
                //print("6");
                break;
            case "7":
                //print("7");
                break;
            case "8":
                // print("8");
                break;
            case "9":
                // print("9");
                break;
            case "*":
                // print("*");
                break;
            case "#":
                // print("#");
                break;

        }

        if (IsWaitingForInput)
        {
            playerDialInput += msg;
            print("player typed: " + playerDialInput);
        }
    }

    public bool IsPlayerInputCorrect(string correctInput)
    {
        if (playerDialInput.CompareTo(correctInput) == 0)
        {
            print("CoRRECTTTTTTTT!");
            return true;
        }
        return false;
    }

    public string GetPlayerDialInput()
    {
        return playerDialInput;
    }

    public void ResetPlayerInput() { playerDialInput = ""; }

    public void Ring()
    {
        print("ring");
        serialController.SendSerialMessage("Ring");
    }

    public IEnumerator StartOparator()
    {
        //ScreenManager.Instance.starts
        yield return new WaitForSeconds(0.2f);
        //start oprator
        ConversationManager.Instance.PlayOparator(0);

        yield return new WaitForSeconds(ConversationManager.Instance.GetOparatorClipLength(0));

        //wait for input
        yield return new WaitForSeconds(0.2f);
        ScreenManager.Instance.EnableWaitingForInputScreen();

    }

    internal void InputCompleted()
    {
        ResetPlayerInput();
        OnWaitingForInputCompleted?.Invoke();
    }
}
