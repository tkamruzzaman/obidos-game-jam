using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ObidosGameJam
{
    public class MessageListener : MonoBehaviour
    {
        // Invoked when a line of data is received from the serial device.
        void OnMessageArrived(string msg)
        {
            print("msg arrived: " + msg);
        }

        // Invoked when a connect/disconnect event occurs. The parameter 'success'
        // will be 'true' upon connection, and 'false' upon disconnection or
        // failure to connect.
        void OnConnectionEvent(bool success)
        {
            print("is connected: " + success);

        }
    }
}