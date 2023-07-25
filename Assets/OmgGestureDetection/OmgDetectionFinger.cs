/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using UnityEngine;
using OpenMGHandModel;

partial class OmgGestureDetectionManager
{
    public class OmgDetectionFinger
    {
        OpenMGFinger m_Finger;
        OpenMGFinger.FingerType m_Type;

        public void SetFinger(OpenMGFinger a_Finger)
        {
            m_Type = a_Finger.Type;
            m_Finger = a_Finger;
        }

        public EFinger GetFingerType()
        {
            return (EFinger)((int)m_Type);
        }

        public Quaternion GetFingerRotation()
        {
            return m_Finger.GetJointRotation(0);
        }

        public Vector3 GetFingerDirection()
        {
            return m_Finger.GetFingerDirection();
        }

        public Vector3 GetTipPosition()
        {
            return m_Finger.GetTipPosition();
        }

        public Quaternion GetBoneRotation(ESpecificBone a_Bone)
        {
            return m_Finger.GetJointRotation((int)a_Bone);
        }

        public Vector3 GetBoneDirection(ESpecificBone a_Bone)
        {
            return m_Finger.GetBoneDirection((int)a_Bone);
        }

        public Vector3 GetBonePosition(ESpecificBone a_Bone)
        {
            if ((int)a_Bone == 0)
                return (m_Finger.GetJointPosition(0) + m_Finger.GetJointPosition(1)) / 2;
            else if ((int)a_Bone == 1)
                return (m_Finger.GetJointPosition(1) + m_Finger.GetJointPosition(2)) / 2;
            else if ((int)a_Bone == 2)
                return (m_Finger.GetJointPosition(2) + m_Finger.GetJointPosition(3)) / 2;
            else
                return Vector3.zero;
        }

        public bool IsExtended()
        {
            return m_Finger.IsExtended;
        }
    }
}
