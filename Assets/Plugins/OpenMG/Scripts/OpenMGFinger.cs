/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using UnityEditor;
using UnityEngine;

namespace OpenMGHandModel
{
    public class OpenMGFinger
    {
        const int max_num_joints = 4;
        const int finger_tip_index = 3;
        const int finger_base_index = 0;
        private Vector3[] Joints = new Vector3[max_num_joints];
        private Quaternion[] JointsRot = new Quaternion[max_num_joints];
        private Vector3 fingerDirection =  new Vector3(0, 0, 0);
        private Vector3[] boneDirection = new Vector3[max_num_joints];
        public bool IsExtended;

        public enum FingerType
        {
            TYPE_THUMB = 0,
            TYPE_INDEX = 1,
            TYPE_MIDDLE = 2,
            TYPE_RING = 3,
            TYPE_PINKY = 4,
            TYPE_UNKNOWN = -1
        }

        public FingerType Type;

        public OpenMGFinger()
        {
            Init();
        }
        public void Init()
        {
            for (int i = 0; i < Joints.Length; i++)
            {
                Joints[i] = Vector3.zero;
            }
        }
        public Vector3[] GetJoints()
        {
            return Joints;
        }
        public Vector3 GetTipPosition()
        {
            return Joints[finger_tip_index];
        }
        public void SetJointPosition(Vector3 pos, int index)
        {
            Joints[index] = pos;
        }
        public Vector3 GetJointPosition(int index)
        {
            return Joints[index];
        }

        public void SetJointRotation(Quaternion rot, int index)
        {
            JointsRot[index] = rot;
        }
        public Quaternion GetJointRotation(int index)
        {
            return JointsRot[index];
        }

        public float GetLength()
        {
            return Vector3.Distance(Joints[finger_base_index], Joints[finger_tip_index]);
        }
        public float GetDistanceFromTipTo(Vector3 pos)
        {
            return Vector3.Distance(pos, Joints[finger_tip_index]);
        }

        public Vector3 SetFingerDirection(Vector3 dir)
        {
            fingerDirection = dir;
            return fingerDirection;
        }

        public Vector3 GetFingerDirection()
        {
            return fingerDirection;
        }

        public void SetBoneDirection(int bone, Vector3 dir)
        {
            boneDirection[bone] = dir;
        }

        public Vector3 GetBoneDirection(int bone)
        {
            return boneDirection[bone];
        }
    }
}