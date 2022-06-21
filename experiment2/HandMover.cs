using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using System.Threading;
using System.Linq;
using System;

public class HandMover: MonoBehaviour
{
    Controller controller = new Controller();

    float FPS64 = 0, FPS8 = 0, FPS1 = 0;

    List<float> conditions = new List<float>();

    public float randomFPS = 0;

    [SerializeField] GameObject Demo;
    bool demo = false;

    // Start is called before the first frame update
    void Start()
    {
        FPS64 = 1f / 64f;
        FPS8 = 1f / 8f;
        FPS1 = 1f;

        conditions.Add(FPS64);
        conditions.Add(FPS8);
        conditions.Add(FPS1);

        conditions = conditions.OrderBy(a => Guid.NewGuid()).ToList();

        foreach (float item in conditions)
        {
            print(item);
        }

        if (Demo)
        {
            demo = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManagementer.Scene == 1)
        {
            SceneManagementer.Scene++;
        }

        if(demo == true)
        {
            randomFPS = 0;
        }
        else
        {
            randomFPS = conditions[SceneManagementer.Jouken];
        }
    }
}
