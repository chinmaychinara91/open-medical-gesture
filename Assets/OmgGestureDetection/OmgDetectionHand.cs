/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using UnityEngine;
using OpenMGHandModel;

partial class OmgGestureDetectionManager
{
    public class OmgDetectionHand
    {
        OmgDetectionFinger[] m_Fingers = new OmgDetectionFinger[5];

        OpenMGHand m_Hand;

        void Start()
        {

        }

        public void SetHand(OpenMGHand a_Hand)
        {
            m_Hand = a_Hand;
            foreach (OpenMGFinger finger in m_Hand.Fingers)
            {
                m_Fingers[(int)finger.Type] = new OmgDetectionFinger();
                m_Fingers[(int)finger.Type].SetFinger(finger);
            }
        }

        public bool CheckWithDetails(FingerExtendedDetails fingerDetails)
        {
            return ((IsFingerExtended(EFinger.eThumb) == fingerDetails.bThumbExtended) &&
                    (IsFingerExtended(EFinger.eIndex) == fingerDetails.bIndexExtended) &&
                    (IsFingerExtended(EFinger.eMiddle) == fingerDetails.bMiddleExtended) &&
                    (IsFingerExtended(EFinger.eRing) == fingerDetails.bRingExtended) &&
                    (IsFingerExtended(EFinger.ePinky) == fingerDetails.bPinkeyExtended));
        }

        public int NumberOfFingersExtended()
        {
            int num = 0;

            for (int i = 0; i <= (int)EFinger.ePinky; i++)
            {
                EFinger finger = EFinger.eThumb + i;
                if (IsFingerExtended(finger))
                {
                    num++;
                }
            }

            return num;
        }

        public bool IsFingerExtended(EFinger a_finger)
        {
            return GetFinger(a_finger).IsExtended();
        }

        public bool IsSet()
        {
            foreach (OmgDetectionFinger finger in m_Fingers)
            {
                if (finger != null)
                {
                    if (finger.GetFingerType() == EFinger.eUnknown)
                    {
                        return false;
                    }
                }
            }

            return m_Hand != null;
        }

        public bool IsClosed(float a_fTolerence)
        {
            return m_Hand.GetFistStrength() > a_fTolerence;
        }

        public bool IsClosed(float a_fTolerence, float b_fTolerance)
        {
            return ((m_Hand.GetFistStrength() > a_fTolerence) & (m_Hand.GetFistStrength() < b_fTolerance));
        }

        public bool IsPinching()
        {
            return m_Hand.GetPinchStatus();
        }

        public float GetPinchDistance()
        {
            return m_Hand.GetPinchDistance();
        }

        public bool IsPinching(float a_fTolerence)
        {
            return m_Hand.GetPinchStrength() > a_fTolerence;
        }

        public Vector3 GetDirectionToFingers()
        {
            return m_Hand.GetDirectionToFingers();
        }

        public Vector3 GetDirectionToThumb()
        {
            return m_Hand.GetDirectionToThumb();
        }

        public Vector3 GetDirectionAwayFromPalm()
        {
            return m_Hand.GetDirectionAwayFromPalm();
        }

        public Vector3 GetPositionBetweenPinch()
        {
            return m_Hand.GetPinchPosition();
        }

        public Vector3 GetHandAxis(EHandAxis a_HandAxis)
        {
            switch (a_HandAxis)
            {
                case EHandAxis.ePalmDirection:
                    return m_Hand.GetDirectionAwayFromPalm();

                case EHandAxis.eThumbDirection:
                    return m_Hand.GetDirectionToThumb();

                case EHandAxis.eFingerDirection:
                    return m_Hand.GetDirectionToFingers();

                default:
                    break;
            }

            return Vector3.zero;
        }

        public Vector3 GetHandPosition()
        {
           return m_Hand.GetPalmPosition();
        }

        public Quaternion GetRotation()
        {
            return m_Hand.GetHandRotation();
        }

        public Vector3 GetWristPosition()
        {
            return m_Hand.GetWristPosition();
        }

        public Vector3 GetVelocity()
        {
            return m_Hand.GetPalmVelocity();
        }

        public OmgDetectionFinger GetFinger(EFinger a_FingerType)
        {
            return m_Fingers[(int)a_FingerType];
        }

        public float DistanceBetweenFingers(EFinger a_Finger0, EFinger a_Finger1)
        {
            if (a_Finger0 == a_Finger1)
            {
                return 0.0f;
            }
            else
            {
                Vector3 fingerPosition1 = GetFinger(a_Finger0).GetTipPosition();
                Vector3 fingerPosition2 = GetFinger(a_Finger1).GetTipPosition();
                return Vector3.Distance(fingerPosition1, fingerPosition2);
            }
        }

    }
}
