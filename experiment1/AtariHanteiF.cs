using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtariHanteiF : MonoBehaviour
{
    private float time;

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
            this.GetComponent<Renderer>().material.color = Color.blue;

            time += Time.deltaTime;

            if (time >= 2f)
            {
                if(SceneManagementer.Scene == 3)
                {
                    SceneManagementer.Scene++;
                }

                if(SceneManagementer.Scene == 6)
                {
                    SceneManagementer.Scene = -1;
                }

                this.GetComponent<Renderer>().material.color = Color.gray;

                time = 0;

                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<Renderer>().material.color = Color.gray;
            time = 0;
        }
    }
}
