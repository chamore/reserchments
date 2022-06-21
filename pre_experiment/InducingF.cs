using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InducingF : MonoBehaviour
{
    [SerializeField] GameObject Rippoutai;
    [SerializeField] GameObject AtariHantei;
    [SerializeField] GameObject cross;
    [SerializeField] GameObject Demo;

    [SerializeField] Text Magnet;
    [SerializeField] Text Space;
    [SerializeField] Text HMD;
    [SerializeField] Text PressKey;
    [SerializeField] Text Atari;

    bool demo = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Demo)
        {
            demo = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Cross.Scene == -1)
        {
            Cross.Scene++;
            cross.gameObject.SetActive(true);
            Magnet.enabled = true;
            HMD.enabled = true;
            Invoke("ChangeUI", 30);

            Rippoutai.gameObject.SetActive(false);
        }

        if (Cross.Scene == 2)
        {
            AtariHantei.gameObject.SetActive(true);

            if (demo == true)
            {
                Atari.enabled = true;
            }
        }

        if (Cross.Scene == 3)
        {
            Cross.Scene++;
            cross.gameObject.SetActive(true);
            Space.enabled = true;
            Magnet.enabled = true;

            Rippoutai.gameObject.SetActive(false);
        }

        if (Cross.Scene == 6)
        {
            Cross.Scene++;
            if(demo == true)
            {
                Invoke("ChangeScene", 10);
            }
            else
            {
                Invoke("ChangeScene", 90);
            }
        }
    }

    void ChangeScene()
    {
        AtariHantei.gameObject.SetActive(true);

        if (demo == true)
        {
            Atari.enabled = true;
        }

        Cross.Scene++;
    }

    void ChangeUI()
    {
        Magnet.enabled = false;
        HMD.enabled = false;
        PressKey.enabled = true;
    }
}
