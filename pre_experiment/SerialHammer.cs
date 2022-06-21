using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialHammer : MonoBehaviour
{
    public static bool trigger;

    public SerialHandler serialHandler;

    char Button = '0';

    // Start is called before the first frame update
    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
    }

    void OnDataReceived(string message)
    {
        try
        {
            Button = message[0];

            switch (Button)
            {
                case '1':
                    trigger = true;
                    break;
            }
            //Debug.Log(message);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}
