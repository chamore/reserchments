using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapMotion : MonoBehaviour
{
    #region Offset Definition
    /// <summary>
    /// Quaternionの差分の保持
    /// </summary>
    private class Offset
    {
        /// <summary>
        /// 対象モデルのQuaternion
        /// </summary>
        private Quaternion Model;
        /// <summary>
        /// 参照モデルのQuaternion
        /// </summary>
        private Quaternion Leap;

        /// <summary>
        /// Quaternionの差分を保持し、参照モデルのQuaternionの変化を対象モデルへと伝搬する
        /// </summary>
        /// <param name="model">対象モデル</param>
        /// <param name="leap">参照モデル</param>
        public Offset(Quaternion model, Quaternion leap)
        {
            Model = model;
            Leap = Quaternion.Inverse(leap);
        }

        /// <summary>
        /// オフセットの適用
        /// </summary>
        /// <param name="currentLeap">現在の参照モデルのQuaternion</param>
        /// <returns>オフセットを適用した対象モデルのQuaternion</returns>
        public Quaternion ApplyOffset(Quaternion currentLeap)
        {
            return currentLeap * Leap * Model;
        }
    }

    /// <summary>
    /// 指のオフセット
    /// </summary>
    private class FingerOffset
    {
        private Offset Bone1;
        private Offset Bone2;
        private Offset Bone3;
        private Offset Bone4;

        public Quaternion Bone1_q;
        public Quaternion Bone2_q;
        public Quaternion Bone3_q;
        public Quaternion Bone4_q;


        public FingerOffset(Finger model, Finger leap)
        {
            if (model.Bone1 != null && leap.Bone1 != null)
            {
                Bone1 = new Offset(model.Bone1.transform.rotation, leap.Bone1.transform.rotation);
            }
            Bone2 = new Offset(model.Bone2.transform.rotation, leap.Bone2.transform.rotation);
            Bone3 = new Offset(model.Bone3.transform.rotation, leap.Bone3.transform.rotation);
            Bone4 = new Offset(model.Bone4.transform.rotation, leap.Bone4.transform.rotation);
        }

        public void ApplyOffset(Finger model, Finger leap)
        {
            if (Bone1 != null)
            {
                Bone1_q = Bone1.ApplyOffset(leap.Bone1.rotation);
            }
            Bone2_q = Bone2.ApplyOffset(leap.Bone2.rotation);
            Bone3_q = Bone3.ApplyOffset(leap.Bone3.rotation);
            Bone4_q = Bone4.ApplyOffset(leap.Bone4.rotation);
        }
    }

    private class HandOffset
    {
        private FingerOffset Thumb;
        private FingerOffset Index;
        private FingerOffset Middle;
        private FingerOffset Ring;
        private FingerOffset Pinky;
        private Offset Wrist;

        public Vector3 OffsetWrist;

        public Quaternion ThumbBone1;
        public Quaternion ThumbBone2;
        public Quaternion ThumbBone3;

        public Quaternion IndexBone1;
        public Quaternion IndexBone2;
        public Quaternion IndexBone3;
        public Quaternion IndexBone4;

        public Quaternion MiddleBone1;
        public Quaternion MiddleBone2;
        public Quaternion MiddleBone3;
        public Quaternion MiddleBone4;

        public Quaternion RingBone1;
        public Quaternion RingBone2;
        public Quaternion RingBone3;
        public Quaternion RingBone4;

        public Quaternion PinkyBone1;
        public Quaternion PinkyBone2;
        public Quaternion PinkyBone3;
        public Quaternion PinkyBone4;

        public Quaternion WristRotation;
        public Vector3 WristPosition;


        public HandOffset(HandHand model, HandHand leap)
        {
            Thumb = new FingerOffset(model.Thumb, leap.Thumb);
            Index = new FingerOffset(model.Index, leap.Index);
            Middle = new FingerOffset(model.Middle, leap.Middle);
            Ring = new FingerOffset(model.Ring, leap.Ring);
            Pinky = new FingerOffset(model.Pinky, leap.Pinky);

            Wrist = new Offset(model.Wrist.rotation, leap.Wrist.rotation);
            OffsetWrist = model.Wrist.position - leap.Wrist.position;
        }

        public void ApplyOffset(HandHand model, HandHand leap)
        {
            Thumb.ApplyOffset(model.Thumb, leap.Thumb);
            ThumbBone1 = Thumb.Bone2_q;
            ThumbBone2 = Thumb.Bone3_q;
            ThumbBone3 = Thumb.Bone4_q;

            Index.ApplyOffset(model.Index, leap.Index);
            IndexBone1 = Index.Bone1_q;
            IndexBone2 = Index.Bone2_q;
            IndexBone3 = Index.Bone3_q;
            IndexBone4 = Index.Bone4_q;

            Middle.ApplyOffset(model.Middle, leap.Middle);
            MiddleBone1 = Middle.Bone1_q;
            MiddleBone2 = Middle.Bone2_q;
            MiddleBone3 = Middle.Bone3_q;
            MiddleBone4 = Middle.Bone4_q;

            Ring.ApplyOffset(model.Ring, leap.Ring);
            RingBone1 = Ring.Bone1_q;
            RingBone2 = Ring.Bone2_q;
            RingBone3 = Ring.Bone3_q;
            RingBone4 = Ring.Bone4_q;

            Pinky.ApplyOffset(model.Pinky, leap.Pinky);
            PinkyBone1 = Pinky.Bone1_q;
            PinkyBone2 = Pinky.Bone2_q;
            PinkyBone3 = Pinky.Bone3_q;
            PinkyBone4 = Pinky.Bone4_q;


            WristRotation = Wrist.ApplyOffset(leap.Wrist.rotation);

            WristPosition = leap.Wrist.position + OffsetWrist;

            Debug.Log(WristPosition);
        }
    }
    #endregion


    [SerializeField]
    private HandHand HandRight;
    [SerializeField]
    private HandHand HandLeapRight;

    private HandOffset HandOffsetRight;

    private Vector3 StartHand;

    private float StartX = 0.2f;
    private float StartY = -0.266f;
    private float StartZ = 0.3f;

    HandMover script;

    public Vector3 OffsetSphere;


    private void Awake()
    {
        HandOffsetRight = new HandOffset(HandRight, HandLeapRight);

        GameObject obj;
        obj = new GameObject("RightHandOffset");
        obj.transform.parent = gameObject.transform;

        StartHand = new Vector3(StartX, StartY, StartZ);

        script= this.GetComponent<HandMover>();
    }


    private void Update()
    {
        HandOffsetRight.ApplyOffset(HandRight, HandLeapRight);

        int randomJouken = script.randomJouken;
        //int randomJouken = 0;

        if (randomJouken == 0)
        {
            Synchronous(HandRight);
        }
        else if(randomJouken == 1)
        {
            StartCoroutine("Asynchronous", HandRight);
        }
    }

    private void Synchronous(HandHand model)
    {
        model.Thumb.Bone2.rotation = HandOffsetRight.ThumbBone1;
        model.Thumb.Bone3.rotation = HandOffsetRight.ThumbBone2;
        model.Thumb.Bone4.rotation = HandOffsetRight.ThumbBone3;

        model.Index.Bone1.rotation = HandOffsetRight.IndexBone1;
        model.Index.Bone2.rotation = HandOffsetRight.IndexBone2;
        model.Index.Bone3.rotation = HandOffsetRight.IndexBone3;
        model.Index.Bone4.rotation = HandOffsetRight.IndexBone4;

        model.Middle.Bone1.rotation = HandOffsetRight.MiddleBone1;
        model.Middle.Bone2.rotation = HandOffsetRight.MiddleBone2;
        model.Middle.Bone3.rotation = HandOffsetRight.MiddleBone3;
        model.Middle.Bone4.rotation = HandOffsetRight.MiddleBone4;

        model.Ring.Bone1.rotation = HandOffsetRight.RingBone1;
        model.Ring.Bone2.rotation = HandOffsetRight.RingBone2;
        model.Ring.Bone3.rotation = HandOffsetRight.RingBone3;
        model.Ring.Bone4.rotation = HandOffsetRight.RingBone4;

        model.Pinky.Bone1.rotation = HandOffsetRight.PinkyBone1;
        model.Pinky.Bone2.rotation = HandOffsetRight.PinkyBone2;
        model.Pinky.Bone3.rotation = HandOffsetRight.PinkyBone3;
        model.Pinky.Bone4.rotation = HandOffsetRight.PinkyBone4;

        model.Wrist.rotation = HandOffsetRight.WristRotation;

        model.Wrist.position = HandOffsetRight.WristPosition + StartHand;
        OffsetSphere = StartHand + HandOffsetRight.OffsetWrist;
    }


    IEnumerator Asynchronous(HandHand model)
    {
        Quaternion ThumbBone1, ThumbBone2, ThumbBone3;
        Quaternion IndexBone1, IndexBone2, IndexBone3, IndexBone4;
        Quaternion MiddleBone1, MiddleBone2, MiddleBone3, MiddleBone4;
        Quaternion RingBone1, RingBone2, RingBone3, RingBone4;
        Quaternion PinkyBone1, PinkyBone2, PinkyBone3, PinkyBone4;
        Quaternion WristRotation;
        Vector3 WristPosition;

        ThumbBone1 = HandOffsetRight.ThumbBone1;
        ThumbBone2 = HandOffsetRight.ThumbBone2;
        ThumbBone3 = HandOffsetRight.ThumbBone3;

        IndexBone1 = HandOffsetRight.IndexBone1;
        IndexBone2 = HandOffsetRight.IndexBone2;
        IndexBone3 = HandOffsetRight.IndexBone3;
        IndexBone4 = HandOffsetRight.IndexBone4;

        MiddleBone1 = HandOffsetRight.MiddleBone1;
        MiddleBone2 = HandOffsetRight.MiddleBone2;
        MiddleBone3 = HandOffsetRight.MiddleBone3;
        MiddleBone4 = HandOffsetRight.MiddleBone4;

        RingBone1 = HandOffsetRight.RingBone1;
        RingBone2 = HandOffsetRight.RingBone2;
        RingBone3 = HandOffsetRight.RingBone3;
        RingBone4 = HandOffsetRight.RingBone4;

        PinkyBone1 = HandOffsetRight.PinkyBone1;
        PinkyBone2 = HandOffsetRight.PinkyBone2;
        PinkyBone3 = HandOffsetRight.PinkyBone3;
        PinkyBone4 = HandOffsetRight.PinkyBone4;

        WristRotation = HandOffsetRight.WristRotation;
        WristPosition = HandOffsetRight.WristPosition;

        yield return new WaitForSeconds(2);

        model.Thumb.Bone2.rotation = ThumbBone1;
        model.Thumb.Bone3.rotation = ThumbBone2;
        model.Thumb.Bone4.rotation = ThumbBone3;

        model.Index.Bone1.rotation = IndexBone1;
        model.Index.Bone2.rotation = IndexBone2;
        model.Index.Bone3.rotation = IndexBone3;
        model.Index.Bone4.rotation = IndexBone4;

        model.Middle.Bone1.rotation = MiddleBone1;
        model.Middle.Bone2.rotation = MiddleBone2;
        model.Middle.Bone3.rotation = MiddleBone3;
        model.Middle.Bone4.rotation = MiddleBone4;

        model.Ring.Bone1.rotation = RingBone1;
        model.Ring.Bone2.rotation = RingBone2;
        model.Ring.Bone3.rotation = RingBone3;
        model.Ring.Bone4.rotation = RingBone4;

        model.Pinky.Bone1.rotation = PinkyBone1;
        model.Pinky.Bone2.rotation = PinkyBone2;
        model.Pinky.Bone3.rotation = PinkyBone3;
        model.Pinky.Bone4.rotation = PinkyBone4;

        model.Wrist.rotation = WristRotation;

        model.Wrist.position = WristPosition + StartHand;
        OffsetSphere = StartHand + HandOffsetRight.OffsetWrist;
    }
}
