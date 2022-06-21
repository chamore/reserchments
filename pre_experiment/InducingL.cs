using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InducingL : MonoBehaviour
{
    [SerializeField] GameObject Rippoutai;
    [SerializeField] GameObject AtariHantei;
    [SerializeField] GameObject cross;
    [SerializeField] GameObject hammer;

    [SerializeField] Image Light;
    [SerializeField] Text PressKey;
    [SerializeField] Text Atari;

    [SerializeField] GameObject Demo;

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
            PressKey.enabled = true;

            Rippoutai.gameObject.SetActive(false);
        }

        if (Cross.Scene == 2)
        {
            Cross.Scene++;
            if(demo == true)
            {
                Invoke("TriggerOn", 5);
                Invoke("ChangeScene", 10);
            }
            else
            {
                Invoke("TriggerOn", 80);
                Invoke("ChangeScene", 90);
            }
        }

        if(Cross.Scene == 5)
        {
            hammer.gameObject.SetActive(true);
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

    void TriggerOn()
    {
        Light.GetComponent<Image>().color = Color.white;
    }
}
