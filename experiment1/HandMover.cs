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

    int Douki = 0; //同期条件=0, 非同期条件=1
    int Hidouki = 1;

    List<int> conditions = new List<int>();

    public int randomJouken = 0;

    [SerializeField] GameObject Demo;
    bool demo = false;

    // Start is called before the first frame update
    void Start()
    {
        conditions.Add(Douki);
        conditions.Add(Hidouki);

        conditions = conditions.OrderBy(a => Guid.NewGuid()).ToList();

        foreach (int item in conditions)
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
        if(SceneManagementer.Scene == 2)
        {
            SceneManagementer.Scene++;
            randomJouken = 0;
        }

        if(SceneManagementer.Scene == 4)
        {
            if(demo == true)
            {
                randomJouken = 0;
            }
            else
            {
                randomJouken = conditions[SceneManagementer.Jouken];
            }
        }
    }
}
