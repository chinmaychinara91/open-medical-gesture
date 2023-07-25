/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System;
using UnityEngine;

// Mapping Quest2 to the OpenMG Hand Model
public class Quest2HandTrackingProvider : OpenMGHandTrackingProvider
{
    // Get the OVR Hand
    public OVRHand ovrLeftHand;
    public OVRHand ovrRightHand;

    private OVRSkeleton ovrLeftHandSkeleton;
    private OVRSkeleton ovrRightHandSkeleton;

    // Start is called before the first frame update
    public override void Start()
    {
        ovrLeftHand = m_DataSource.transform.FindChildRecursive("LeftOVRHand").GetComponent<OVRHand>();
        if(ovrLeftHand == null)
        {
            Debug.LogError("Cannot track left hand as no OVR Hand!");
        }
        ovrRightHand = m_DataSource.transform.FindChildRecursive("RightOVRHand").GetComponent<OVRHand>();
        if (ovrRightHand == null)
        {
            Debug.LogError("Cannot track right hand as no OVR Hand!");
        }
        ovrLeftHandSkeleton = m_DataSource.transform.FindChildRecursive("LeftOVRHand").GetComponent<OVRSkeleton>();
        ovrRightHandSkeleton = m_DataSource.transform.FindChildRecursive("RightOVRHand").GetComponent<OVRSkeleton>();
    }

    Vector3 joint_pos = Vector3.zero;
    //Quaternion hand_rot = Quaternion.identity;
    Quaternion joint_rot = Quaternion.identity;
    Vector3 velocity = Vector3.zero;

    // for pinch distance measurement
    Vector3 left_index_tip_pos = Vector3.zero;
    Vector3 right_index_tip_pos = Vector3.zero;
    Vector3 left_thumb_tip_pos = Vector3.zero;
    Vector3 right_thumb_tip_pos = Vector3.zero;

    // Update is called once per frame
    public override void Update()
    {
        // if only left hand is tracked
        if(ovrLeftHand.IsTracked && !ovrRightHand.IsTracked)
        {
            hand_num = 1;

            for(int i=0; i<ovrLeftHandSkeleton.Bones.Count; i++)
            {
                joint_pos = ovrLeftHandSkeleton.Bones[i].Transform.position;
                joint_rot = ovrLeftHandSkeleton.Bones[i].Transform.rotation;

                // palm position and hand rotation
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Body_LeftHandPalm)
                {
                    UpdatePalmPosition(joint_pos, 0);
                    UpdateHandRotation(joint_rot, 0);
                }
                
                // thumb
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Thumb1)
                {
                    UpdateThumbPosition(joint_pos, 0, 0);
                    UpdateThumbRotation(joint_rot, 0, 0);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Thumb2)
                {
                    UpdateThumbPosition(joint_pos, 0, 1);
                    UpdateThumbRotation(joint_rot, 0, 1);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Thumb3)
                {
                    UpdateThumbPosition(joint_pos, 0, 2);
                    UpdateThumbRotation(joint_rot, 0, 2);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_ThumbTip)
                {
                    UpdateThumbPosition(joint_pos, 0, 3);
                    UpdateThumbRotation(joint_rot, 0, 3);

                    left_thumb_tip_pos = joint_pos;
                }

                // index
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Index1)
                {
                    UpdateIndexPosition(joint_pos, 0, 0);
                    UpdateIndexRotation(joint_rot, 0, 0);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Index2)
                {
                    UpdateIndexPosition(joint_pos, 0, 1);
                    UpdateIndexRotation(joint_rot, 0, 1);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Index3)
                {
                    UpdateIndexPosition(joint_pos, 0, 2);
                    UpdateIndexRotation(joint_rot, 0, 2);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_IndexTip)
                {
                    UpdateIndexPosition(joint_pos, 0, 3);
                    UpdateIndexRotation(joint_rot, 0, 3);

                    left_index_tip_pos = joint_pos;
                }

                // middle
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Middle1)
                {
                    UpdateMiddlePosition(joint_pos, 0, 0);
                    UpdateMiddleRotation(joint_rot, 0, 0);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Middle2)
                {
                    UpdateMiddlePosition(joint_pos, 0, 1);
                    UpdateMiddleRotation(joint_rot, 0, 1);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Middle3)
                {
                    UpdateMiddlePosition(joint_pos, 0, 2);
                    UpdateMiddleRotation(joint_rot, 0, 2);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_MiddleTip)
                {
                    UpdateMiddlePosition(joint_pos, 0, 3);
                    UpdateMiddleRotation(joint_rot, 0, 3);
                }

                // ring
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Ring1)
                {
                    UpdateRingPosition(joint_pos, 0, 0);
                    UpdateRingRotation(joint_rot, 0, 0);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Ring2)
                {
                    UpdateRingPosition(joint_pos, 0, 1);
                    UpdateRingRotation(joint_rot, 0, 1);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Ring3)
                {
                    UpdateRingPosition(joint_pos, 0, 2);
                    UpdateRingRotation(joint_rot, 0, 2);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_RingTip)
                {
                    UpdateRingPosition(joint_pos, 0, 3);
                    UpdateRingRotation(joint_rot, 0, 3);
                }

                // Pinky
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Pinky1)
                {
                    UpdatePinkyPosition(joint_pos, 0, 0);
                    UpdatePinkyRotation(joint_rot, 0, 0);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Pinky2)
                {
                    UpdatePinkyPosition(joint_pos, 0, 1);
                    UpdatePinkyRotation(joint_rot, 0, 1);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Pinky3)
                {
                    UpdatePinkyPosition(joint_pos, 0, 2);
                    UpdatePinkyRotation(joint_rot, 0, 2);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_PinkyTip)
                {
                    UpdatePinkyPosition(joint_pos, 0, 3);
                    UpdatePinkyRotation(joint_rot, 0, 3);
                }

                // pinching
                hands[0].SetPinchStatus(ovrLeftHand.GetFingerIsPinching(OVRHand.HandFinger.Index));

                // pinch strength
                hands[0].SetPinchStrength(ovrLeftHand.GetFingerPinchStrength(OVRHand.HandFinger.Index));

                // pinch distance
                float dist = Vector3.Distance(left_index_tip_pos, left_thumb_tip_pos);
                hands[0].SetPinchDistance(dist);

                // pinch position
                Vector3 avg = (left_index_tip_pos + left_thumb_tip_pos) * 0.5f;
                hands[0].SetPinchPosition(avg);

                // wrist position
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Body_LeftHandWrist)
                {
                    hands[0].SetWristPosition(joint_pos);
                }
            }

            hands[0].isLeft = true;
            hands[0].isRight = false;
        }

        // if only right hand is tracked
        else if (ovrRightHand.IsTracked && !ovrLeftHand.IsTracked)
        {
            hand_num = 1;

            for (int i = 0; i < ovrRightHandSkeleton.Bones.Count; i++)
            {
                joint_pos = ovrRightHandSkeleton.Bones[i].Transform.position;
                joint_rot = ovrRightHandSkeleton.Bones[i].Transform.rotation;

                // palm position and hand rotation
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Body_RightHandPalm)
                {
                    UpdatePalmPosition(joint_pos, 1);
                    UpdateHandRotation(joint_rot, 1);
                }

                // thumb
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Thumb1)
                {
                    UpdateThumbPosition(joint_pos, 1, 0);
                    UpdateThumbRotation(joint_rot, 1, 0);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Thumb2)
                {
                    UpdateThumbPosition(joint_pos, 1, 1);
                    UpdateThumbRotation(joint_rot, 1, 1);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Thumb3)
                {
                    UpdateThumbPosition(joint_pos, 1, 2);
                    UpdateThumbRotation(joint_rot, 1, 2);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_ThumbTip)
                {
                    UpdateThumbPosition(joint_pos, 1, 3);
                    UpdateThumbRotation(joint_rot, 1, 3);

                    right_thumb_tip_pos = joint_pos;
                }

                // index
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Index1)
                {
                    UpdateIndexPosition(joint_pos, 1, 0);
                    UpdateIndexRotation(joint_rot, 1, 0);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Index2)
                {
                    UpdateIndexPosition(joint_pos, 1, 1);
                    UpdateIndexRotation(joint_rot, 1, 1);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Index3)
                {
                    UpdateIndexPosition(joint_pos, 1, 2);
                    UpdateIndexRotation(joint_rot, 1, 2);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_IndexTip)
                {
                    UpdateIndexPosition(joint_pos, 1, 3);
                    UpdateIndexRotation(joint_rot, 1, 3);

                    right_index_tip_pos = joint_pos;
                }

                // middle
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Middle1)
                {
                    UpdateMiddlePosition(joint_pos, 1, 0);
                    UpdateMiddleRotation(joint_rot, 1, 0);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Middle2)
                {
                    UpdateMiddlePosition(joint_pos, 1, 1);
                    UpdateMiddleRotation(joint_rot, 1, 1);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Middle3)
                {
                    UpdateMiddlePosition(joint_pos, 1, 2);
                    UpdateMiddleRotation(joint_rot, 1, 2);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_MiddleTip)
                {
                    UpdateMiddlePosition(joint_pos, 1, 3);
                    UpdateMiddleRotation(joint_rot, 1, 3);
                }

                // ring
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Ring1)
                {
                    UpdateRingPosition(joint_pos, 1, 0);
                    UpdateRingRotation(joint_rot, 1, 0);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Ring2)
                {
                    UpdateRingPosition(joint_pos, 1, 1);
                    UpdateRingRotation(joint_rot, 1, 1);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Ring3)
                {
                    UpdateRingPosition(joint_pos, 1, 2);
                    UpdateRingRotation(joint_rot, 1, 2);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_RingTip)
                {
                    UpdateRingPosition(joint_pos, 1, 3);
                    UpdateRingRotation(joint_rot, 1, 3);
                }

                // Pinky
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Pinky1)
                {
                    UpdatePinkyPosition(joint_pos, 1, 0);
                    UpdatePinkyRotation(joint_rot, 1, 0);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Pinky2)
                {
                    UpdatePinkyPosition(joint_pos, 1, 1);
                    UpdatePinkyRotation(joint_rot, 1, 1);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Pinky3)
                {
                    UpdatePinkyPosition(joint_pos, 1, 2);
                    UpdatePinkyRotation(joint_rot, 1, 2);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_PinkyTip)
                {
                    UpdatePinkyPosition(joint_pos, 1, 3);
                    UpdatePinkyRotation(joint_rot, 1, 3);
                }

                // pinching
                hands[0].SetPinchStatus(ovrRightHand.GetFingerIsPinching(OVRHand.HandFinger.Index));

                // pinch strength
                hands[0].SetPinchStrength(ovrRightHand.GetFingerPinchStrength(OVRHand.HandFinger.Index));

                // pinch distance
                float dist = Vector3.Distance(right_index_tip_pos, right_thumb_tip_pos);
                hands[0].SetPinchDistance(dist);

                // pinch position
                Vector3 avg = (right_index_tip_pos + right_thumb_tip_pos) * 0.5f;
                hands[0].SetPinchPosition(avg);

                // wrist position
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Body_RightHandWrist)
                {
                    hands[0].SetWristPosition(joint_pos);
                }

                // velocity
                //if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_WristRoot)
                //{
                //    float speed = Vector3.Distance(lastPosition, ovrRightHandSkeleton.Bones[i].Transform.position) / Time.deltaTime;
                //    //update previous positions
                //    lastPosition = ovrRightHandSkeleton.Bones[i].Transform.position;
                //    rightHandDebug = String.Format("{0:0.00}", speed);
                //    //Debug.Log("Speed: " + String.Format("{0:0.00}", rightHandDebug));
                //}
            }

            hands[0].isRight = true;
            hands[0].isLeft = false;
        }

        // if both hands are tracked
        else
        {
            hand_num = 2;

            for (int i = 0; i < ovrLeftHandSkeleton.Bones.Count; i++)
            {
                joint_pos = ovrLeftHandSkeleton.Bones[i].Transform.position;
                joint_rot = ovrLeftHandSkeleton.Bones[i].Transform.rotation;

                // palm position and hand rotation
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Body_LeftHandPalm)
                {
                    UpdatePalmPosition(joint_pos, 0);
                    UpdateHandRotation(joint_rot, 0);
                }

                // thumb
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Thumb1)
                {
                    UpdateThumbPosition(joint_pos, 0, 0);
                    UpdateThumbRotation(joint_rot, 0, 0);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Thumb2)
                {
                    UpdateThumbPosition(joint_pos, 0, 1);
                    UpdateThumbRotation(joint_rot, 0, 1);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Thumb3)
                {
                    UpdateThumbPosition(joint_pos, 0, 2);
                    UpdateThumbRotation(joint_rot, 0, 2);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_ThumbTip)
                {
                    UpdateThumbPosition(joint_pos, 0, 3);
                    UpdateThumbRotation(joint_rot, 0, 3);

                    left_thumb_tip_pos = joint_pos;
                }

                // index
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Index1)
                {
                    UpdateIndexPosition(joint_pos, 0, 0);
                    UpdateIndexRotation(joint_rot, 0, 0);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Index2)
                {
                    UpdateIndexPosition(joint_pos, 0, 1);
                    UpdateIndexRotation(joint_rot, 0, 1);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Index3)
                {
                    UpdateIndexPosition(joint_pos, 0, 2);
                    UpdateIndexRotation(joint_rot, 0, 2);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_IndexTip)
                {
                    UpdateIndexPosition(joint_pos, 0, 3);
                    UpdateIndexRotation(joint_rot, 0, 3);

                    left_index_tip_pos = joint_pos;
                }

                // middle
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Middle1)
                {
                    UpdateMiddlePosition(joint_pos, 0, 0);
                    UpdateMiddleRotation(joint_rot, 0, 0);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Middle2)
                {
                    UpdateMiddlePosition(joint_pos, 0, 1);
                    UpdateMiddleRotation(joint_rot, 0, 1);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Middle3)
                {
                    UpdateMiddlePosition(joint_pos, 0, 2);
                    UpdateMiddleRotation(joint_rot, 0, 2);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_MiddleTip)
                {
                    UpdateMiddlePosition(joint_pos, 0, 3);
                    UpdateMiddleRotation(joint_rot, 0, 3);
                }

                // ring
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Ring1)
                {
                    UpdateRingPosition(joint_pos, 0, 0);
                    UpdateRingRotation(joint_rot, 0, 0);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Ring2)
                {
                    UpdateRingPosition(joint_pos, 0, 1);
                    UpdateRingRotation(joint_rot, 0, 1);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Ring3)
                {
                    UpdateRingPosition(joint_pos, 0, 2);
                    UpdateRingRotation(joint_rot, 0, 2);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_RingTip)
                {
                    UpdateRingPosition(joint_pos, 0, 3);
                    UpdateRingRotation(joint_rot, 0, 3);
                }

                // Pinky
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Pinky1)
                {
                    UpdatePinkyPosition(joint_pos, 0, 0);
                    UpdatePinkyRotation(joint_rot, 0, 0);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Pinky2)
                {
                    UpdatePinkyPosition(joint_pos, 0, 1);
                    UpdatePinkyRotation(joint_rot, 0, 1);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Pinky3)
                {
                    UpdatePinkyPosition(joint_pos, 0, 2);
                    UpdatePinkyRotation(joint_rot, 0, 2);
                }
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_PinkyTip)
                {
                    UpdatePinkyPosition(joint_pos, 0, 3);
                    UpdatePinkyRotation(joint_rot, 0, 3);
                }

                // pinching
                hands[0].SetPinchStatus(ovrLeftHand.GetFingerIsPinching(OVRHand.HandFinger.Index));

                // pinch strength
                hands[0].SetPinchStrength(ovrLeftHand.GetFingerPinchStrength(OVRHand.HandFinger.Index));

                // pinch distance
                float dist = Vector3.Distance(left_index_tip_pos, left_thumb_tip_pos);
                hands[0].SetPinchDistance(dist);

                // pinch position
                Vector3 avg = (left_index_tip_pos + left_thumb_tip_pos) * 0.5f;
                hands[0].SetPinchPosition(avg);

                // wrist position
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Body_LeftHandWrist)
                {
                    hands[0].SetWristPosition(joint_pos);
                }
            }

            for (int i = 0; i < ovrRightHandSkeleton.Bones.Count; i++)
            {
                joint_pos = ovrRightHandSkeleton.Bones[i].Transform.position;
                joint_rot = ovrRightHandSkeleton.Bones[i].Transform.rotation;

                // palm position and hand rotation
                if (ovrLeftHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Body_RightHandPalm)
                {
                    UpdatePalmPosition(joint_pos, 1);
                    UpdateHandRotation(joint_rot, 1);
                }

                // thumb
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Thumb1)
                {
                    UpdateThumbPosition(joint_pos, 1, 0);
                    UpdateThumbRotation(joint_rot, 1, 0);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Thumb2)
                {
                    UpdateThumbPosition(joint_pos, 1, 1);
                    UpdateThumbRotation(joint_rot, 1, 1);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Thumb3)
                {
                    UpdateThumbPosition(joint_pos, 1, 2);
                    UpdateThumbRotation(joint_rot, 1, 2);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_ThumbTip)
                {
                    UpdateThumbPosition(joint_pos, 1, 3);
                    UpdateThumbRotation(joint_rot, 1, 3);

                    right_thumb_tip_pos = joint_pos;
                }

                // index
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Index1)
                {
                    UpdateIndexPosition(joint_pos, 1, 0);
                    UpdateIndexRotation(joint_rot, 1, 0);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Index2)
                {
                    UpdateIndexPosition(joint_pos, 1, 1);
                    UpdateIndexRotation(joint_rot, 1, 1);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Index3)
                {
                    UpdateIndexPosition(joint_pos, 1, 2);
                    UpdateIndexRotation(joint_rot, 1, 2);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_IndexTip)
                {
                    UpdateIndexPosition(joint_pos, 1, 3);
                    UpdateIndexRotation(joint_rot, 1, 3);

                    right_index_tip_pos = joint_pos;
                }

                // middle
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Middle1)
                {
                    UpdateMiddlePosition(joint_pos, 1, 0);
                    UpdateMiddleRotation(joint_rot, 1, 0);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Middle2)
                {
                    UpdateMiddlePosition(joint_pos, 1, 1);
                    UpdateMiddleRotation(joint_rot, 1, 1);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Middle3)
                {
                    UpdateMiddlePosition(joint_pos, 1, 2);
                    UpdateMiddleRotation(joint_rot, 1, 2);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_MiddleTip)
                {
                    UpdateMiddlePosition(joint_pos, 1, 3);
                    UpdateMiddleRotation(joint_rot, 1, 3);
                }

                // ring
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Ring1)
                {
                    UpdateRingPosition(joint_pos, 1, 0);
                    UpdateRingRotation(joint_rot, 1, 0);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Ring2)
                {
                    UpdateRingPosition(joint_pos, 1, 1);
                    UpdateRingRotation(joint_rot, 1, 1);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Ring3)
                {
                    UpdateRingPosition(joint_pos, 1, 2);
                    UpdateRingRotation(joint_rot, 1, 2);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_RingTip)
                {
                    UpdateRingPosition(joint_pos, 1, 3);
                    UpdateRingRotation(joint_rot, 1, 3);
                }

                // Pinky
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Pinky1)
                {
                    UpdatePinkyPosition(joint_pos, 1, 0);
                    UpdatePinkyRotation(joint_rot, 1, 0);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Pinky2)
                {
                    UpdatePinkyPosition(joint_pos, 1, 1);
                    UpdatePinkyRotation(joint_rot, 1, 1);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Pinky3)
                {
                    UpdatePinkyPosition(joint_pos, 1, 2);
                    UpdatePinkyRotation(joint_rot, 1, 2);
                }
                if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_PinkyTip)
                {
                    UpdatePinkyPosition(joint_pos, 1, 3);
                    UpdatePinkyRotation(joint_rot, 1, 3);
                }

                // pinching
                hands[1].SetPinchStatus(ovrRightHand.GetFingerIsPinching(OVRHand.HandFinger.Index));

                // pinch strength
                hands[1].SetPinchStrength(ovrRightHand.GetFingerPinchStrength(OVRHand.HandFinger.Index));

                // pinch distance
                float dist = Vector3.Distance(right_index_tip_pos, right_thumb_tip_pos);
                hands[1].SetPinchDistance(dist);

                // pinch position
                Vector3 avg = (right_index_tip_pos + right_thumb_tip_pos) * 0.5f;
                hands[1].SetPinchPosition(avg);

                // wrist position
                if(ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Body_RightHandWrist)
                {
                    hands[1].SetWristPosition(joint_pos);
                }
                
                // velocity
                //if (ovrRightHandSkeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_WristRoot)
                //{
                //    float speed = Vector3.Distance(lastPosition, ovrRightHandSkeleton.Bones[i].Transform.position) / Time.deltaTime;
                //    //update previous positions
                //    lastPosition = ovrRightHandSkeleton.Bones[i].Transform.position;
                //    rightHandDebug = String.Format("{0:0.00}", speed);
                //    //Debug.Log("Speed: " + String.Format("{0:0.00}", rightHandDebug));
                //}
            }

            hands[0].isLeft = true;
            hands[1].isRight = true;
        }
    }

    public GameObject RightHandAnchor;
    private Vector3 lastPosition;
}
