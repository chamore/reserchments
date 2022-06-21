using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hammer : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject cross;
    [SerializeField] GameObject Rippoutai;
    [SerializeField] Image Light;


    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SerialHammer.trigger == true)
        {
            animator.SetTrigger("HammerTrigger");

            SerialHammer.trigger = false;

            Invoke("ResetMotion", 10);

            Invoke("ChangeScene", 11);

            //Debug.Log("cccccc");
        }
    }

    void ChangeScene()
    {
        Cross.Scene = -1;
        cross.gameObject.SetActive(true);

        Rippoutai.gameObject.SetActive(false);
        
        this.gameObject.SetActive(false);
    }

    void ResetMotion()
    {
        animator.Play("Idle");
        Light.GetComponent<Image>().color = Color.black;
    }
}
