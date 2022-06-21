using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtariHanteiF : MonoBehaviour
{
    private float time;

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
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Renderer>().material.color = Color.black;

            time += Time.deltaTime;

            if (time >= 1f)
            {
                if(Cross.Scene == 2)
                {
                    Cross.Scene++;
                }

                if(Cross.Scene == 8)
                {
                    Cross.Scene = -1;
                }

                other.GetComponent<Renderer>().material.color = Color.white;

                time = 0;

                if (demo == true)
                {
                    Atari.enabled = false;
                }

                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Renderer>().material.color = Color.white;
            time = 0;
        }
    }
}
