/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OpenMGHandModel;

public enum ESpecificBone
{
    eBone0,
    eBone1,
    eBone2
}

public enum EHand
{
    eRightHand,
    eLeftHand
}

public enum EDirection
{
    eUpwards,
    eDownwards,
    eLeft,
    eRight,
    eOutwards,
    eInWards
}

public enum EHandAxis
{
    ePalmDirection,
    eThumbDirection,
    eFingerDirection
}


public enum EFinger
{
    eUnknown = -1,
    eThumb = 0,
    eIndex = 1,
    eMiddle = 2,
    eRing = 3,
    ePinky = 4
}

public struct FingerExtendedDetails
{
    public bool bThumbExtended;
    public bool bIndexExtended;
    public bool bMiddleExtended;
    public bool bRingExtended;
    public bool bPinkeyExtended;
}

[System.Serializable]
public struct ObjectActivationFingers
{
    public bool bThumb;
    public bool bIndex;
    public bool bMiddle;
    public bool bRing;
    public bool bPinky;

    public ObjectActivationFingers(bool Thumb, bool Index, bool Middle, bool Ring, bool Pinky)
    {
        bThumb = Thumb;
        bIndex = Index;
        bMiddle = Middle;
        bRing = Ring;
        bPinky = Pinky;
    }

    public bool[] GetAsArray()
    {
        bool[] returnArray = new bool[5];

        returnArray[0] = bThumb;
        returnArray[1] = bIndex;
        returnArray[2] = bMiddle;
        returnArray[3] = bRing;
        returnArray[4] = bPinky;

        return returnArray;
    }
}

partial class OmgGestureDetectionManager : MonoBehaviour
{
    public HandTrackingDeviceController device;

    public static OmgGestureDetectionManager sInstance = null;

    OmgDetectionHand m_LeftDetection;
    OmgDetectionHand m_RightDetection;

    int m_iNumberOfHands = 0;

    bool m_bLeftIsVisible = false;
    bool m_bRightIsVisible = false;

    public UnityEvent OnLeftHandEnter;
    public UnityEvent OnLeftHandExit;

    public UnityEvent OnRightHandEnter;
    public UnityEvent OnRightHandExit;

    public ObjectActivationFingers LeftActivationFingers = new ObjectActivationFingers(false, true, false, false, false);
    public ObjectActivationFingers RightActivationFingers = new ObjectActivationFingers(false, true, false, false, false);

    Dictionary<EFinger, GameObject> m_RightFingerCollisions = new Dictionary<EFinger, GameObject>();
    Dictionary<EFinger, GameObject> m_LeftFingerCollisions = new Dictionary<EFinger, GameObject>();

    public static OmgGestureDetectionManager Get()
    {
        return sInstance;
    }

    public OmgDetectionHand GetHand(EHand a_hand)
    {
        return a_hand == EHand.eRightHand ? m_RightDetection : m_LeftDetection;
    }

    public bool IsHandSet(EHand a_hand)
    {
        bool bIsVisible = a_hand == EHand.eLeftHand ? m_bLeftIsVisible : m_bRightIsVisible;
        return GetHand(a_hand).IsSet() && bIsVisible;
    }

    public bool IsBothHandsSet()
    {
        return IsHandSet(EHand.eLeftHand) && IsHandSet(EHand.eRightHand);
    }

    public bool AreBothHandsVisible()
    {
        return m_iNumberOfHands >= 2 && IsBothHandsSet();
    }

    public float GetDistanceBetweenHands()
    {
        Vector3 leftPos = m_LeftDetection.GetHandPosition();
        Vector3 rightPos = m_RightDetection.GetHandPosition();

        return Vector3.Distance(leftPos, rightPos);
    }

    GameObject CreateCollisionSphere()
    {
        GameObject DetectionSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        DetectionSphere.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        DetectionSphere.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

        DetectionSphere.AddComponent<FingerCollision>();

        DetectionSphere.GetComponent<SphereCollider>().isTrigger = true;
        DetectionSphere.GetComponent<MeshRenderer>().enabled = false;
        Rigidbody rb = DetectionSphere.AddComponent<Rigidbody>();

        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        return DetectionSphere;
    }

    private void Awake()
    {
        if (sInstance == null)
        {
            sInstance = this;
        }
        else
        {
            Destroy(this);
        }

        m_LeftDetection = new OmgDetectionHand();
        m_RightDetection = new OmgDetectionHand();


        bool[] leftBools = LeftActivationFingers.GetAsArray();
        bool[] rightBools = RightActivationFingers.GetAsArray();

        for (int i = 0; i < leftBools.Length; i++)
        {
            EFinger SelectedFinger = EFinger.eThumb + i;
            if (leftBools[i])
            {
                m_LeftFingerCollisions.Add(SelectedFinger, CreateCollisionSphere());
            }

            if (rightBools[i])
            {
                m_RightFingerCollisions.Add(SelectedFinger, CreateCollisionSphere());
            }
        }

    }

    void UpdateHand(OpenMGHand hand)
    {
        if (hand.isLeft)
        {
            m_LeftDetection.SetHand(hand);

            for (int i = 0; i < 5; i++)
            {
                EFinger SelectedFinger = EFinger.eThumb + i;

                if (m_LeftFingerCollisions.ContainsKey(SelectedFinger))
                {
                    //m_LeftFingerCollisions[SelectedFinger].transform.position = m_LeftDetection.GetFinger(SelectedFinger).GetTipPosition();
                    if (i == 0)
                    {
                        m_LeftFingerCollisions[SelectedFinger].transform.position = hand.GetThumbTipPosition();
                    }
                    else if (i == 1)
                    {
                        m_LeftFingerCollisions[SelectedFinger].transform.position = hand.GetIndexTipPosition();
                    }
                    else if (i == 2)
                    {
                        m_LeftFingerCollisions[SelectedFinger].transform.position = hand.GetMiddleTipPosition();
                    }
                    else if (i == 3)
                    {
                        m_LeftFingerCollisions[SelectedFinger].transform.position = hand.GetRingTipPosition();
                    }
                    else if (i == 4)
                    {
                        m_LeftFingerCollisions[SelectedFinger].transform.position = hand.GetPinkyTipPosition();
                    }
                    m_LeftFingerCollisions[SelectedFinger].SetActive(true);
                }

                if (m_iNumberOfHands == 1)
                {
                    if (m_RightFingerCollisions.ContainsKey(SelectedFinger))
                    {
                        m_RightFingerCollisions[SelectedFinger].SetActive(false);
                    }
                }
            }

            m_bLeftIsVisible = true;

            if (m_iNumberOfHands == 1)
            {
                m_bRightIsVisible = false;
            }
        }

        if (hand.isRight)
        {
            m_RightDetection.SetHand(hand);

            for (int i = 0; i < 5; i++)
            {
                EFinger SelectedFinger = EFinger.eThumb + i;

                if (m_RightFingerCollisions.ContainsKey(SelectedFinger))
                {
                    //m_RightFingerCollisions[SelectedFinger].transform.position = m_RightDetection.GetFinger(SelectedFinger).GetTipPosition();
                    if (i == 0)
                    {
                        m_RightFingerCollisions[SelectedFinger].transform.position = hand.GetThumbTipPosition();
                    }
                    else if (i == 1)
                    {
                        m_RightFingerCollisions[SelectedFinger].transform.position = hand.GetIndexTipPosition();
                    }
                    else if (i == 2)
                    {
                        m_RightFingerCollisions[SelectedFinger].transform.position = hand.GetMiddleTipPosition();
                    }
                    else if (i == 3)
                    {
                        m_RightFingerCollisions[SelectedFinger].transform.position = hand.GetRingTipPosition();
                    }
                    else if (i == 4)
                    {
                        m_RightFingerCollisions[SelectedFinger].transform.position = hand.GetPinkyTipPosition();
                    }
                    m_RightFingerCollisions[SelectedFinger].SetActive(true);
                }

                if (m_iNumberOfHands == 1)
                {
                    if (m_LeftFingerCollisions.ContainsKey(SelectedFinger))
                    {
                        m_LeftFingerCollisions[SelectedFinger].SetActive(false);
                    }
                }
            }

            m_bRightIsVisible = true;

            if (m_iNumberOfHands == 1)
            {
                m_bLeftIsVisible = false;
            }
        }
    }

    void Update()
    {
        bool bHasHandsInScene = device.m_DataProvider.GetNumOfHands() > 0;
        bool bControllerIsConnected = true;

        m_iNumberOfHands = device.m_DataProvider.GetNumOfHands();

        bool bPrevLeftWasVisible = m_bLeftIsVisible;
        bool bPrevRightWasVisible = m_bRightIsVisible;

        if (bHasHandsInScene && bControllerIsConnected)
        {
            if (device.m_DataProvider.GetNumOfHands() == 2)
            {
                foreach (OpenMGHand hand in device.m_DataProvider.hands)
                {
                    UpdateHand(hand);
                }
            }
            else if(device.m_DataProvider.GetNumOfHands() == 1)
            {
                UpdateHand(device.m_DataProvider.hands[0]);
            }
        }
        else
        {
            m_bLeftIsVisible = false;
            m_bRightIsVisible = false;
        }

        if (bPrevLeftWasVisible != m_bLeftIsVisible)
        {
            if (m_bLeftIsVisible)
            {
                OnLeftHandEnter.Invoke();
            }
            else
            {
                OnLeftHandExit.Invoke();
            }
        }

        if (bPrevRightWasVisible != m_bRightIsVisible)
        {
            if (m_bRightIsVisible)
            {
                OnRightHandEnter.Invoke();
            }
            else
            {
                OnRightHandExit.Invoke();
            }
        }
    }
}
