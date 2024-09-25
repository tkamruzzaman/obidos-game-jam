using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static InputManager Instance;
    private SerialController serialController;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //serialController = GameObject.Find("SerialController").GetComponent<SerialControllerCustomDelimiter>();
        serialController = FindObjectOfType<SerialController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Sending some action");
            // Sends a 65 (ascii for 'A') followed by an space (ascii 32, which 
            // is configured in the controller of our scene as the separator).
            //serialController.SendSerialMessage("Ring");
        }
    }

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        msg = msg.Trim();

        switch (msg)
        {
            case "up":
                print("up");
                break;
            case "down":
                print("down");
                GameManager.Instance.EndGame();
                break;
            case "0":
                print("0");
                break;
            case "1":
                print("1");
                break;
            case "2":
                print("2");
                break;
            case "3":
                print("3");
                break;
            case "4":
                print("4");
                break;
            case "5":
                print("5");
                break;
            case "6":
                print("6");
                break;
            case "7":
                print("7");
                break;
            case "8":
                print("8");
                break;
            case "9":
                print("9");
                break;
            case "*":
                print("*");
                break;
            case "#":
                print("#");
                break;

        }
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        print("is connected: " + success);

    }

    public void Ring()
    {
        print("ring");

        // Sends a 65 (ascii for 'A') followed by an space (ascii 32, which 
        // is configured in the controller of our scene as the separator).
        serialController.SendSerialMessage("Ring");
    }
}
