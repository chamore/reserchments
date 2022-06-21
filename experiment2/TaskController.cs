using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Linq;
using Leap;

public class TaskController : MonoBehaviour
{
    Controller controller = new Controller();

    private Frame frame;

    [SerializeField] GameObject Hand;
    HandMover script;
    private float randomFPS;
    private float KeikaJikan;

    private float time; //タスク成功中の時間

    private float GrabStrength;

    private int TaskNumber = 0;
    List<int> TaskType = new List<int>();

    private bool TaskWorking;

    private bool ExperimentStart;

    [SerializeField] Text Three;
    [SerializeField] Text Second;
    [SerializeField] Text One;
    [SerializeField] Text Zero;

    [SerializeField] Text Opening;
    [SerializeField] Text Grabbing;
    [SerializeField] Text LeftSide;
    [SerializeField] Text RightSide;
    [SerializeField] Text LeftRotation;
    [SerializeField] Text RightRotation;

    [SerializeField] GameObject Palm;

    // Start is called before the first frame update
    void Start()
    {
        script = Hand.GetComponent<HandMover>();

        Three.enabled = false;
        Second.enabled = false;
        One.enabled = false;
        Zero.enabled = false;
        Opening.enabled = false;
        Grabbing.enabled = false;
        LeftSide.enabled = false;
        RightSide.enabled = false;
        LeftRotation.enabled = false;
        RightRotation.enabled = false;

        TaskType.Add(0); //手を握る
        TaskType.Add(1); //手を開く
        TaskType.Add(2); //手の向き左
        TaskType.Add(3); //手の向き右
        TaskType.Add(4); //回内
        TaskType.Add(5); //回外


        TaskType = TaskType.OrderBy(a => Guid.NewGuid()).ToList();

        TaskWorking = false;
        ExperimentStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        randomFPS = script.randomFPS;

        KeikaJikan += Time.deltaTime;

        if (KeikaJikan >= randomFPS)
        {
            KeikaJikan = 0;

            if (controller.Frame() != null)
            {
                frame = controller.Frame();
                foreach (Hand mhand in frame.Hands)
                {
                    GrabStrength = mhand.GrabStrength;
                }
            }
        }

        if (SceneManagementer.Scene == 5)
        {
            if (ExperimentStart == true)
            {
                ExperimentStart = false;
                Invoke("ChangeTask", 2f);
            }
            CountDown();
        }

        if (SceneManagementer.Scene == 6)
        {
            TaskWorking = false;
            One.enabled = false;
            Second.enabled = false;
            Three.enabled = false;

            Opening.enabled = false;
            Grabbing.enabled = false;
            LeftSide.enabled = false;
            RightSide.enabled = false;
            LeftRotation.enabled = false;
            RightRotation.enabled = false;

            ExperimentStart = true;
        }

        if (TaskWorking == true)
        {
            switch (TaskType[TaskNumber])
            {
                case 0:
                    GrabingTask();
                    break;

                case 1:
                    OpeningTask();
                    break;

                case 2:
                    LeftSideTask();
                    break;

                case 3:
                    RightSideTask();
                    break;

                case 4:
                    LeftRotationTask();
                    break;

                case 5:
                    RightRotationTask();
                    break;
            }
        }
    }

    void TaskOriginating()
    {
        TaskWorking = true;
    }

    void GrabingTask()
    {
        Grabbing.enabled = true;

        if (GrabStrength > 0.8f)
        {
            time += Time.deltaTime; //タスク成功中時間加算
        }
        else
        {
            time = 0;
        }

        if (time > 3f)
        {
            Grabbing.enabled = false;
        }
    }

    void OpeningTask()
    {
        Opening.enabled = true;

        if (GrabStrength < 0.2f)
        {
            time += Time.deltaTime; //タスク成功中時間加算
        }
        else
        {
            time = 0;
        }

        if (time > 3f)
        {
            Opening.enabled = false;
        }
    }

    void LeftSideTask()
    {
        LeftSide.enabled = true;

        float HandAngle = Palm.transform.localEulerAngles.x;

        if (HandAngle > 20f && HandAngle < 180f)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
        }

        if (time > 3f)
        {
            LeftSide.enabled = false;
        }
    }

    void RightSideTask()
    {
        RightSide.enabled = true;

        float HandAngle = Palm.transform.localEulerAngles.x;

        if (HandAngle > 180f && HandAngle < 350f)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
        }

        if (time > 3f)
        {
            RightSide.enabled = false;
        }
    }

    void LeftRotationTask()
    {
        LeftRotation.enabled = true;

        float HandAngle = Palm.transform.localEulerAngles.y;

        Debug.Log(HandAngle);

        if (HandAngle > 210f && HandAngle < 360f)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
        }

        if (time > 3f)
        {
            LeftRotation.enabled = false;
        }
    }

    void RightRotationTask()
    {
        RightRotation.enabled = true;

        float HandAngle = Palm.transform.localEulerAngles.y;

        if (HandAngle > 0 && HandAngle < 130f)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
        }

        if (time > 3f)
        {
            RightRotation.enabled = false;
        }
    }

    void ChangeTask()
    {
        Zero.enabled = false;

        TaskNumber++;

        if (TaskNumber == 6)
        {
            TaskNumber = 0;
        }

        TaskWorking = true;
    }

    void CountDown()
    {
        if (time > 3f)
        {
            time = 0;

            One.enabled = false;
            Zero.enabled = true;

            TaskWorking = false;

            Invoke("ChangeTask", 2f);

        }
        else if (time > 2f)
        {
            Second.enabled = false;
            One.enabled = true;
        }
        else if (time > 1f)
        {
            Three.enabled = false;
            Second.enabled = true;
        }
        else if (time > 0)
        {
            Three.enabled = true;
        }
        else
        {
            One.enabled = false;
            Second.enabled = false;
            Three.enabled = false;
        }
    }
}
