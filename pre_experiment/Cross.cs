using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cross : MonoBehaviour
{
    static public int Scene = 0;

    static public int Jouken = 0;

    [SerializeField] GameObject Rippoutai;
    [SerializeField] GameObject Demo;

    [SerializeField] Text PressKey;
    [SerializeField] Text Magnet;
    [SerializeField] Text Space;
    [SerializeField] Text HMD;
    [SerializeField] Text Atari;

    // Start is called before the first frame update
    void Start()
    {
        if (Magnet == true)
        {
            Magnet.enabled = false;
            Space.enabled = false;
            HMD.enabled = false;
        }

        if (Demo == true)
        {
            Atari.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            if(Jouken == 5)
            {

            }
            else
            {
                Scene++;

                Rippoutai.gameObject.SetActive(true);

                PressKey.enabled = false;

                if (Magnet == true)
                {
                    Magnet.enabled = false;
                    Space.enabled = false;
                }

                this.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            OVRManager.display.RecenterPose();
        }
    }
}
