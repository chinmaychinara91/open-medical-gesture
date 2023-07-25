/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using Leap;
using Leap.Unity;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace OpenMGHandModel
{
    public class OpenMGHand
    {
        const int num_fingers = 5;
        public List<OpenMGFinger> Fingers;

        private OpenMGFinger m_Thumb;
        private OpenMGFinger m_Index;
        private OpenMGFinger m_Middle;
        private OpenMGFinger m_Ring;
        private OpenMGFinger m_Pinky;

        private Vector3 palm_position = Vector3.zero;
        private Vector3 palm_direction = Vector3.zero;
        private Quaternion palm_orietation = Quaternion.identity;
        private Quaternion hand_rotation = Quaternion.identity;
        private Vector3 palm_velocity = Vector3.zero;
        public bool isRight = true;
        public bool isLeft = true;

        // hand actions
        public float fistStrength = 0;
        public bool isPinching = false;
        public float pinchDistance = 0;
        public float pinchStrength = 0;
        public Vector3 wristPosition = Vector3.zero;
        public Vector3 pinchPosition = Vector3.zero;
        public Vector3 distalAxis = Vector3.zero;
        public Vector3 radialAxis = Vector3.zero;
        public Vector3 palmarAxis = Vector3.zero;

        public OpenMGHand()
        {
            Init();
        }

        public void Init()
        {
            Fingers = new List<OpenMGFinger>(num_fingers);
            Fingers.Add(new OpenMGFinger());
            Fingers.Add(new OpenMGFinger());
            Fingers.Add(new OpenMGFinger());
            Fingers.Add(new OpenMGFinger());
            Fingers.Add(new OpenMGFinger());

            m_Thumb = Fingers[0];
            m_Index = Fingers[1];
            m_Middle = Fingers[2];
            m_Ring = Fingers[3];
            m_Pinky = Fingers[4];
        }

        public void SetPalmQuaternion(Quaternion quat)
        {
            palm_orietation = quat;
            palm_direction = palm_orietation.eulerAngles;
            palm_direction.Normalize();
        }

        public Vector3 GetPalmPosition()
        {
            return palm_position;
        }

        public Vector3 GetPalmNormalDirection()
        {
            return palm_direction;
        }

        public Vector3 GetPalmVelocity()
        {
            return palm_velocity;
        }

        public void SetPalmNormalDirection(Vector3 dir)
        {
            palm_direction = dir;
        }

        public void SetPalmPosition(Vector3 pos)
        {
            palm_position = pos;
        }

        public void SetPalmVelocity(Vector3 vel)
        {
            palm_velocity = vel;
        }

        public void SetHandRotation(Quaternion quat)
        {
            hand_rotation = quat;
        }

        public Quaternion GetHandRotation()
        {
            return hand_rotation;
        }

        Vector3 rlt = Vector3.zero;
        public Vector3 GetJointPosition(int finger, int index)
        {
            switch (finger)
            {
                case 0:
                    rlt = m_Thumb.GetJointPosition(index);
                    break;
                case 1:
                    rlt = m_Index.GetJointPosition(index);
                    break;
                case 2:
                    rlt = m_Middle.GetJointPosition(index);
                    break;
                case 3:
                    rlt = m_Ring.GetJointPosition(index);
                    break;
                case 4:
                    rlt = m_Pinky.GetJointPosition(index);
                    break;

            }
            return rlt;
        }

        Quaternion rltRot = Quaternion.identity;
        public Quaternion GetJointRotation(int finger, int index)
        {
            switch (finger)
            {
                case 0:
                    rltRot = m_Thumb.GetJointRotation(index);
                    break;
                case 1:
                    rltRot = m_Index.GetJointRotation(index);
                    break;
                case 2:
                    rltRot = m_Middle.GetJointRotation(index);
                    break;
                case 3:
                    rltRot = m_Ring.GetJointRotation(index);
                    break;
                case 4:
                    rltRot = m_Pinky.GetJointRotation(index);
                    break;

            }
            return rltRot;
        }

        public void SetThumbJointPosition(Vector3 pos, int joint)
        {
            m_Thumb.SetJointPosition(pos, joint);
        }
        public void SetThumbJointRotation(Quaternion rot, int joint)
        {
            m_Thumb.SetJointRotation(rot, joint);
        }
        public Vector3 GetThumbTipPosition()
        {
            return m_Thumb.GetTipPosition();
        }
        public float GetThumbLength()
        {
            return m_Thumb.GetDistanceFromTipTo(palm_position);
        }
        public void SetIndexJointPosition(Vector3 pos, int joint)
        {
            m_Index.SetJointPosition(pos, joint);
        }
        public void SetIndexJointRotation(Quaternion rot, int joint)
        {
            m_Index.SetJointRotation(rot, joint);
        }
        public Vector3 GetIndexTipPosition()
        {
            return m_Index.GetTipPosition();
        }
        public float GetIndexLength()
        {
            return m_Index.GetDistanceFromTipTo(palm_position);
        }
        public void SetMiddleJointPosition(Vector3 pos, int joint)
        {
            m_Middle.SetJointPosition(pos, joint);
        }
        public void SetMiddleJointRotation(Quaternion rot, int joint)
        {
            m_Middle.SetJointRotation(rot, joint);
        }
        public Vector3 GetMiddleTipPosition()
        {
            return m_Middle.GetTipPosition();
        }
        public float GetMiddleLength()
        {
            return m_Middle.GetDistanceFromTipTo(palm_position);
        }
        public void SetRingJointPosition(Vector3 pos, int joint)
        {
            m_Ring.SetJointPosition(pos, joint);
        }
        public void SetRingJointRotation(Quaternion rot, int joint)
        {
            m_Ring.SetJointRotation(rot, joint);
        }
        public Vector3 GetRingTipPosition()
        {
            return m_Ring.GetTipPosition();
        }
        public float GetRingLength()
        {
            return m_Ring.GetDistanceFromTipTo(palm_position);
        }
        public void SetPinkyJointPosition(Vector3 pos, int joint)
        {
            m_Pinky.SetJointPosition(pos, joint);
        }
        public void SetPinkyJointRotation(Quaternion rot, int joint)
        {
            m_Pinky.SetJointRotation(rot, joint);
        }
        public Vector3 GetPinkyTipPosition()
        {
            return m_Pinky.GetTipPosition();
        }
        public float GetPinkyLength()
        {
            return m_Pinky.GetDistanceFromTipTo(palm_position);
        }
        public int GetFingersCount()
        {
            return num_fingers;
        }

        public void SetIndexDirection(Vector3 dir)
        {
            m_Index.SetFingerDirection(dir);
        }
        public void SetMiddleDirection(Vector3 dir)
        {
            m_Middle.SetFingerDirection(dir);
        }
        public void SetPinkyDirection(Vector3 dir)
        {
            m_Pinky.SetFingerDirection(dir);
        }
        public void SetRingDirection(Vector3 dir)
        {
            m_Ring.SetFingerDirection(dir);
        }
        public void SetThumbDirection(Vector3 dir)
        {
            m_Thumb.SetFingerDirection(dir);
        }

        public void SetFistStrenth(float val)
        {
            fistStrength = val;
        }

        public float GetFistStrength()
        {
            return fistStrength;
        }

        public void SetPinchDistance(float val)
        {
            pinchDistance = val;
        }

        public float GetPinchDistance()
        {
            return pinchDistance;
        }

        public void SetPinchStrength(float val)
        {
            pinchStrength = val;
        }

        public float GetPinchStrength()
        {
            return pinchStrength;
        }

        public void SetPinchPosition(Vector3 pos)
        {
            pinchPosition = pos;
        }

        public Vector3 GetPinchPosition()
        {
            return pinchPosition;
        }

        public void SetWristPosition(Vector3 pos)
        {
            wristPosition = pos;
        }

        public Vector3 GetWristPosition()
        {
            return wristPosition;
        }

        public void SetDirectionToFingers(Vector3 pos)
        {
            distalAxis = pos;
        }

        public Vector3 GetDirectionToFingers()
        {
            return distalAxis;
        }

        public void SetDirectionToThumb(Vector3 pos)
        {
            radialAxis = pos;
        }

        public Vector3 GetDirectionToThumb()
        {
            return radialAxis;
        }

        public void SetDirectionAwayFromPalm(Vector3 pos)
        {
            palmarAxis = pos;
        }

        public Vector3 GetDirectionAwayFromPalm()
        {
            return palmarAxis;
        }

        public void SetPinchStatus(bool status)
        {
            isPinching = status;
        }

        public bool GetPinchStatus()
        {
            return isPinching;
        }
    }
}