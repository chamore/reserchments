using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using System.Threading;
using System.Linq;
using System;

public class RippoutaiSizeL : MonoBehaviour
{
    Controller controller = new Controller();

    private Frame frame;

    [SerializeField] GameObject EmptyRippoutai;

    float FPS64 = 0, FPS16 = 0, FPS4 = 0, FPS1 = 0, FPS025 = 0;

    float KeikaJikan = 0;

    List<float> FPS = new List<float>();
    float randomFPS = 0;


    [SerializeField] GameObject Atari;

    [SerializeField] GameObject Demo;
    bool demo = false;

    Vector3 RippoutaiOffset = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        FPS64 = 1f / 64f;
        FPS16 = 1f / 16f;
        FPS4 = 1f / 4f;
        FPS1 = 1f;
        FPS025 = 4f;

        FPS.Add(FPS64);
        FPS.Add(FPS16);
        FPS.Add(FPS4);
        FPS.Add(FPS1);
        FPS.Add(FPS025);

        FPS = FPS.OrderBy(a => Guid.NewGuid()).ToList();

        foreach (float item in FPS)
        {
            float ValueFPS = 1f / item;
            print(ValueFPS);
        }

        RippoutaiOffset = Atari.transform.position;

        if (Demo)
        {
            demo = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Cross.Scene == 1)
        {
            Cross.Scene++;
            if(demo == true)
            {
                randomFPS = FPS64;
            }
            else
            {
                randomFPS = FPS[Cross.Jouken];
            }

            Cross.Jouken++;
        }

        KeikaJikan += Time.deltaTime;
        

        if (KeikaJikan >= randomFPS)
        {
            KeikaJikan = 0;

            Vector3 Offset = EmptyRippoutai.transform.position;
            Offset.x += RippoutaiOffset.x;
            Offset.z += RippoutaiOffset.z;
            Offset.y -= 0.3f;

            this.transform.position = Offset;
            this.transform.rotation = EmptyRippoutai.transform.rotation;

            if (controller.Frame() != null)
            {
                frame = controller.Frame();
                foreach (Hand mhand in frame.Hands)
                {
                    //Debug.Log(mhand.GrabAngle + ";" + mhand.GrabStrength);

                    this.transform.localScale = Vector3.one * ((1 - mhand.GrabStrength) * 0.08f + 0.02f);
                }
            }
        }
    }
}
