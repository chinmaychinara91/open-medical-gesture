/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using UnityEngine;
using OpenMGHandModel;

public class OpenMGHandTrackingProvider : MonoBehaviour
{
    public GameObject m_DataSource;
    const int max_num_hands = 2;
    public int hand_num = 0;
    public OpenMGHand[] hands = new OpenMGHand[max_num_hands];

    public string leftHandDebug;
    public string rightHandDebug;

    public OpenMGHandTrackingProvider()
    {
        Init();
    }

    void Init()
    {
        for (int i = 0; i < hands.Length; i++)
        {
            hands[i] = new OpenMGHand();
            hands[i].Init();
        }
    }
    public void UpdateThumbPosition(Vector3 pos, int hand, int joint)
    {
        hands[hand].SetThumbJointPosition(pos, joint);
    }
    public void UpdateIndexPosition(Vector3 pos, int hand, int joint)
    {
        hands[hand].SetIndexJointPosition(pos, joint);
    }
    public void UpdateMiddlePosition(Vector3 pos, int hand, int joint)
    {
        hands[hand].SetMiddleJointPosition(pos, joint);
    }
    public void UpdateRingPosition(Vector3 pos, int hand, int joint)
    {
        hands[hand].SetRingJointPosition(pos, joint);
    }
    public void UpdatePinkyPosition(Vector3 pos, int hand, int joint)
    {
        hands[hand].SetPinkyJointPosition(pos, joint);
    }

    public void UpdateThumbRotation(Quaternion rot, int hand, int joint)
    {
        hands[hand].SetThumbJointRotation(rot, joint);
    }
    public void UpdateIndexRotation(Quaternion rot, int hand, int joint)
    {
        hands[hand].SetIndexJointRotation(rot, joint);
    }
    public void UpdateMiddleRotation(Quaternion rot, int hand, int joint)
    {
        hands[hand].SetMiddleJointRotation(rot, joint);
    }
    public void UpdateRingRotation(Quaternion rot, int hand, int joint)
    {
        hands[hand].SetRingJointRotation(rot, joint);
    }
    public void UpdatePinkyRotation(Quaternion rot, int hand, int joint)
    {
        hands[hand].SetPinkyJointRotation(rot, joint);
    }

    public void UpdatePalmPosition(Vector3 pos, int hand)
    {
        if (hand < hands.Length)
        {
            if(hands[hand]!=null)
                hands[hand].SetPalmPosition(pos);
        }
    }
    public Vector3 GetPalmPosition(int hand)
    {
        if (hand < hands.Length)
        {
            if (hands[hand] != null)
                return hands[hand].GetPalmPosition();
        }
        return Vector3.zero;
    }
    public void UpdatePalmVelocity(Vector3 vel, int hand)
    {
        if (hand < hands.Length)
        {
            if (hands[hand] != null)
                hands[hand].SetPalmVelocity(vel);
        }
    }
    public Vector3 GetPalmVelocity(int hand)
    {
        if (hand < hands.Length)
        {
            if (hands[hand] != null)
                return hands[hand].GetPalmVelocity();
        }
        return Vector3.zero;
    }

    public void UpdateHandRotation(Quaternion quat, int hand)
    {
        if (hand < hands.Length)
        {
            if (hands[hand] != null)
                hands[hand].SetHandRotation(quat);
        }
    }
    public Quaternion GetHandRotation(int hand)
    {
        if (hand < hands.Length)
        {
            if (hands[hand] != null)
                return hands[hand].GetHandRotation();
        }
        return Quaternion.identity;
    }

    Vector3 rlt = Vector3.zero;
    public Vector3 GetTipPosition(int hand, int finger)
    {
        switch (finger)
        {
            case 0:
                rlt = hands[hand].GetThumbTipPosition();
                break;
            case 1:
                rlt = hands[hand].GetIndexTipPosition();
                break;
            case 2:
                rlt = hands[hand].GetMiddleTipPosition();
                break;
            case 3:
                rlt = hands[hand].GetRingTipPosition();
                break;
            case 4:
                rlt = hands[hand].GetPinkyTipPosition();
                break;
        }
        return rlt;
    }
    public Vector3 GetJointPosition(int hand, int finger, int index)
    {
        rlt = hands[hand].GetJointPosition(finger, index);
        return rlt;
    }

    Quaternion rltRot = Quaternion.identity;
    public Quaternion GetJointRotation(int hand, int finger, int index)
    {
        rltRot = hands[hand].GetJointRotation(finger, index);
        return rltRot;
    }

    public float GetFingerLength(int hand, int finger)
    {
        float len = 0;
        switch (finger)
        {
            case 0:
                len = hands[hand].GetThumbLength();
                break;
            case 1:
                len = hands[hand].GetIndexLength();
                break;
            case 2:
                len = hands[hand].GetMiddleLength();
                break;
            case 3:
                len = hands[hand].GetRingLength();
                break;
            case 4:
                len = hands[hand].GetPinkyLength();
                break;
        }
        return len;
    }
    public int GetNumOfHands()
    {
        return hand_num;
    }
    public OpenMGHand GetHand(int hand)
    {
        if (hand < hands.Length)
        {
            if (hands[hand] != null)
                return hands[hand];
        }
        return null;
    }
    public virtual void Update()
    {
    }
    public virtual void Start()
    {
    }
    public virtual void Close()
    {
    }
}
