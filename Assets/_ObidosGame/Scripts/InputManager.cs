using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[DefaultExecutionOrder(-1)]

public class InputManager : MonoBehaviour
{
    public Action OnPickupPhone = delegate{};
    public Action OnPutDownPhone = delegate{};
    public Action OnWaitingForInputCompleted = delegate { };
    public Action<int> OnDialNumber = delegate{};
    public Action<string> OnDialSpecialCharacter=delegate{};

    public static InputManager Instance;
    private SerialController serialController;
    public bool IsHungup { get; private set; }

    public bool IsWaitingForInput { get; set; }

    string playerDialInput;

    [SerializeField] private bool isToUseDummyInput;


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
        if (!isToUseDummyInput)
        {
            return;
        }

        string str = null;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            str = "up";
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            str = "down";
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            str = "0";
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            str = "1";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            str = "2";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            str = "3";
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            str = "4";
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            str = "5";
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            str = "6";
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            str = "7";
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            str = "8";
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            str = "9";
        }
        if (str != null)
        {
            OnMessageArrived(str);
            //print(str);
        }
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
            OnPickupPhone?.Invoke();
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
            OnPutDownPhone?.Invoke();
                print("down");
                IsHungup = true;
                if (GameManager.Instance.isPhoneRang
                && GameManager.Instance.isGameStarted
                && !GameManager.Instance.isGameEnded)
                {
                    GameManager.Instance.EndGame();
                }
                break;
            case "0":
                //print("0");
                OnDialNumber?.Invoke(0);
                break;
            case "1":
                //print("1");
                OnDialNumber?.Invoke(1);
                break;
            case "2":
                //print("2");
                OnDialNumber?.Invoke(2);
                break;
            case "3":
                // print("3");
                OnDialNumber?.Invoke(3);
                break;
            case "4":
                //  print("4");
                OnDialNumber?.Invoke(4);
                break;
            case "5":
                // print("5");
                OnDialNumber?.Invoke(5);
                break;
            case "6":
                //print("6");
                OnDialNumber?.Invoke(6);
                break;
            case "7":
                //print("7");
                OnDialNumber?.Invoke(7);
                break;
            case "8":
                // print("8");
                OnDialNumber?.Invoke(8);
                break;
            case "9":
                // print("9");
                OnDialNumber?.Invoke(9);
                break;
            case "*":
                // print("*");
                OnDialSpecialCharacter?.Invoke("*");
                break;
            case "#":
                // print("#");
                OnDialSpecialCharacter?.Invoke("#");
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
        if(isToUseDummyInput){
            SoundManager.Instance.PlayPhoneRingSound();
        }else{
        serialController.SendSerialMessage("Ring");
    }}

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
