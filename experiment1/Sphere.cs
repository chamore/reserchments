using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    [SerializeField] GameObject SphereEmpty;
    [SerializeField] GameObject HandModel;

    LeapMotion script;

    // Start is called before the first frame update
    void Start()
    {
        script = HandModel.GetComponent<LeapMotion>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = script.OffsetSphere + SphereEmpty.transform.position;
    }
}
