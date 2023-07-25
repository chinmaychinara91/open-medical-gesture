/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using UnityEngine;
using Leap.Unity;
using Leap;
using System.Runtime.InteropServices;

public class LeapMotionHandTrackingProvider : OpenMGHandTrackingProvider
{
    public LeapProvider LeapDataProvider;
    public Frame curFrame;
      
    // Use this for initialization
    public override void Start() {

        LeapDataProvider = m_DataSource.GetComponent<LeapProvider>();
        if (LeapDataProvider == null)
        {
            Debug.LogError("Cannot use LeapImageRetriever if there is no LeapProvider!");
        }
    }

    Vector3 joint_pos = Vector3.zero;
    Quaternion hand_rot = Quaternion.identity;
    Quaternion joint_rot = Quaternion.identity;
    Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    public override void Update() {
        if (LeapDataProvider != null)
        {
            curFrame = LeapDataProvider.CurrentFrame;
            hand_num = curFrame.Hands.Count;
            for (int i = 0; i < hand_num; i++)
            {
                joint_pos = curFrame.Hands[i].PalmPosition.ToVector3();
                UpdatePalmPosition(joint_pos, i);
                hand_rot = curFrame.Hands[i].Rotation.ToQuaternion();
                UpdateHandRotation(hand_rot, i);
                velocity = curFrame.Hands[i].PalmVelocity.ToVector3();
                UpdatePalmVelocity(velocity, i);

                //thumb
                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_THUMB].bones[0].NextJoint.ToVector3();
                UpdateThumbPosition(joint_pos, i, 0);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_THUMB].bones[0].Rotation.ToQuaternion();
                UpdateThumbRotation(joint_rot, i, 0);

                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_THUMB].bones[1].NextJoint.ToVector3();
                UpdateThumbPosition(joint_pos, i, 1);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_THUMB].bones[1].Rotation.ToQuaternion();
                UpdateThumbRotation(joint_rot, i, 1);

                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_THUMB].bones[2].NextJoint.ToVector3();
                UpdateThumbPosition(joint_pos, i, 2);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_THUMB].bones[2].Rotation.ToQuaternion();
                UpdateThumbRotation(joint_rot, i, 2);

                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_THUMB].bones[3].NextJoint.ToVector3();
                UpdateThumbPosition(joint_pos, i, 3);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_THUMB].bones[3].Rotation.ToQuaternion();
                UpdateThumbRotation(joint_rot, i, 3);

                hands[i].SetThumbDirection(curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_THUMB].Direction.ToVector3());

                //index
                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_INDEX].bones[0].NextJoint.ToVector3();
                UpdateIndexPosition(joint_pos, i, 0);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_INDEX].bones[0].Rotation.ToQuaternion();
                UpdateIndexRotation(joint_rot, i, 0);

                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_INDEX].bones[1].NextJoint.ToVector3();
                UpdateIndexPosition(joint_pos, i, 1);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_INDEX].bones[1].Rotation.ToQuaternion();
                UpdateIndexRotation(joint_rot, i, 1);

                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_INDEX].bones[2].NextJoint.ToVector3();
                UpdateIndexPosition(joint_pos, i, 2);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_INDEX].bones[2].Rotation.ToQuaternion();
                UpdateIndexRotation(joint_rot, i, 2);

                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_INDEX].bones[3].NextJoint.ToVector3();
                UpdateIndexPosition(joint_pos, i, 3);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_INDEX].bones[3].Rotation.ToQuaternion();
                UpdateIndexRotation(joint_rot, i, 3);

                hands[i].SetIndexDirection(curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_INDEX].Direction.ToVector3());

                //middle
                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_MIDDLE].bones[0].NextJoint.ToVector3();
                UpdateMiddlePosition(joint_pos, i, 0);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_MIDDLE].bones[0].Rotation.ToQuaternion();
                UpdateMiddleRotation(joint_rot, i, 0);

                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_MIDDLE].bones[1].NextJoint.ToVector3();
                UpdateMiddlePosition(joint_pos, i, 1);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_MIDDLE].bones[1].Rotation.ToQuaternion();
                UpdateMiddleRotation(joint_rot, i, 1);

                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_MIDDLE].bones[2].NextJoint.ToVector3();
                UpdateMiddlePosition(joint_pos, i, 2);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_MIDDLE].bones[2].Rotation.ToQuaternion();
                UpdateMiddleRotation(joint_rot, i, 2);

                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_MIDDLE].bones[3].NextJoint.ToVector3();
                UpdateMiddlePosition(joint_pos, i, 3);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_MIDDLE].bones[3].Rotation.ToQuaternion();
                UpdateMiddleRotation(joint_rot, i, 3);

                hands[i].SetMiddleDirection(curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_MIDDLE].Direction.ToVector3());

                //ring
                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_RING].bones[0].NextJoint.ToVector3();
                UpdateRingPosition(joint_pos, i, 0);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_RING].bones[0].Rotation.ToQuaternion();
                UpdateRingRotation(joint_rot, i, 0);

                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_RING].bones[1].NextJoint.ToVector3();
                UpdateRingPosition(joint_pos, i, 1);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_RING].bones[1].Rotation.ToQuaternion();
                UpdateRingRotation(joint_rot, i, 1);

                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_RING].bones[2].NextJoint.ToVector3();
                UpdateRingPosition(joint_pos, i, 2);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_RING].bones[2].Rotation.ToQuaternion();
                UpdateRingRotation(joint_rot, i, 2);

                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_RING].bones[3].NextJoint.ToVector3();
                UpdateRingPosition(joint_pos, i, 3);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_RING].bones[3].Rotation.ToQuaternion();
                UpdateRingRotation(joint_rot, i, 3);

                hands[i].SetRingDirection(curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_RING].Direction.ToVector3());

                //pinky
                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_PINKY].bones[0].NextJoint.ToVector3();
                UpdatePinkyPosition(joint_pos, i, 0);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_PINKY].bones[0].Rotation.ToQuaternion();
                UpdatePinkyRotation(joint_rot, i, 0);

                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_PINKY].bones[1].NextJoint.ToVector3();
                UpdatePinkyPosition(joint_pos, i, 1);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_PINKY].bones[1].Rotation.ToQuaternion();
                UpdatePinkyRotation(joint_rot, i, 1);

                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_PINKY].bones[2].NextJoint.ToVector3();
                UpdatePinkyPosition(joint_pos, i, 2);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_PINKY].bones[2].Rotation.ToQuaternion();
                UpdatePinkyRotation(joint_rot, i, 2);

                joint_pos = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_PINKY].bones[3].NextJoint.ToVector3();
                UpdatePinkyPosition(joint_pos, i, 3);
                joint_rot = curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_PINKY].bones[3].Rotation.ToQuaternion();
                UpdatePinkyRotation(joint_rot, i, 3);

                hands[i].SetPinkyDirection(curFrame.Hands[i].Fingers[(int)Finger.FingerType.TYPE_PINKY].Direction.ToVector3());

                // finger extension
                hands[i].Fingers[0].IsExtended = curFrame.Hands[i].Fingers[0].IsExtended;
                hands[i].Fingers[1].IsExtended = curFrame.Hands[i].Fingers[1].IsExtended;
                hands[i].Fingers[2].IsExtended = curFrame.Hands[i].Fingers[2].IsExtended;
                hands[i].Fingers[3].IsExtended = curFrame.Hands[i].Fingers[3].IsExtended;
                hands[i].Fingers[4].IsExtended = curFrame.Hands[i].Fingers[4].IsExtended;

                // fist strength
                hands[i].SetFistStrenth(curFrame.Hands[i].GetFistStrength());

                // pinching
                hands[i].SetPinchStatus(curFrame.Hands[i].IsPinching());

                // pinch distance
                hands[i].SetPinchDistance(curFrame.Hands[i].PinchDistance);

                // pinch strength
                hands[i].SetPinchStrength(curFrame.Hands[i].PinchStrength);

                // pinch position
                hands[i].SetPinchPosition(curFrame.Hands[i].GetPinchPosition());

                // wrist position
                hands[i].SetWristPosition(curFrame.Hands[i].WristPosition.ToVector3());

                // radial, palmar, distal
                hands[i].SetDirectionToFingers(curFrame.Hands[i].DistalAxis());
                hands[i].SetDirectionToThumb(curFrame.Hands[i].RadialAxis());
                hands[i].SetDirectionAwayFromPalm(curFrame.Hands[i].PalmarAxis());

                hands[i].SetPalmNormalDirection(curFrame.Hands[i].PalmNormal.ToVector3());
                hands[i].isRight = curFrame.Hands[i].IsRight;
                hands[i].isLeft = curFrame.Hands[i].IsLeft;
            }
        }
    }
}
