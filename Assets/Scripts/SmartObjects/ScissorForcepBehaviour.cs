﻿/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenMg.SmartObjects;

public class ScissorForcepBehaviour : SmartObjectBehaviour
{
    [Header("VARIABLES LOCAL TO THIS SMARTOBJECT", order = 0)]

    [Header("Position/Rotation of SmartObject about Anchor", order = 1)]

    [Tooltip("Position at anchor Left Hand")]
    public Vector3 positionAtLeftHandAnchor;

    [Tooltip("Rotation at anchor Left Hand")]
    public Vector3 rotationAtLeftHandAnchor;

    [Tooltip("Position at anchor Right Hand")]
    public Vector3 positionAtRightHandAnchor;

    [Tooltip("Rotation at anchor Right Hand")]
    public Vector3 rotationAtRightHandAnchor;

    [Header("Press/Unpress SmartObject transforms", order = 2)]

    [Tooltip("source rotation Left Shank")]
    public Vector3 unpressRotationLeftShank;

    [Tooltip("destination rotation Left Shank")]
    public Vector3 pressRotationLeftShank;

    [Tooltip("source rotation Right Shank")]
    public Vector3 unpressRotationRightShank;

    [Tooltip("destination rotation Right Shank")]
    public Vector3 pressRotationRightShank;

    [Tooltip("source rotation Thumb")]
    public Vector3 unpressRotationThumb;

    [Tooltip("destination rotation Thumb")]
    public Vector3 pressRotationThumb;

    override public void Awake()
    {
        base.Awake();

    }

    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
    }

    override public void AttachToHand()
    {
        base.AttachToHand();

        if (touchFlag && (!rightHand.GetComponent<HasGrabbed>().hasObjectGrabbed))
        {
            // no interaction defined for left hand as of now
            if (interactionBehaviour.closestHoveringHand.ToString().Contains("left"))
            {

            }

            // if right hand is the closest hovering hand
            else
            {
                if (showFakeHand)
                {
                    fakeHand.SetActive(true);
                    rightHandRenderer.material = fadeMatFull;
                }
                else
                {
                    rightHandRenderer.material = fadeMatPartial;
                }
                transform.parent = rightHandAnchor;
                transform.localPosition = positionAtRightHandAnchor;
                transform.localRotation = Quaternion.Euler(rotationAtRightHandAnchor);
            }
            interactionBehaviour.ignoreContact = true;

            // as a whole
            transform.GetComponent<BoxCollider>().enabled = false;
            rigidBody.isKinematic = true;
            rigidBody.useGravity = false;

            pressFlag = true;
            touchFlag = false;
        }
    }

    override public void RemoveFromHand()
    {
        base.RemoveFromHand();

        if (touchFlag == false)
        {
            if (showFakeHand)
            {
                fakeHand.SetActive(false);
            }
            leftHandRenderer.material = opaqueMat;
            rightHandRenderer.material = opaqueMat;
            transform.parent = homeTransform;

            // as a whole
            transform.GetComponent<BoxCollider>().enabled = true;
            rigidBody.isKinematic = false;
            rigidBody.useGravity = true;

            Invoke("enableContact", mainControl.GetComponent<ActivationDeactivationControls>().timeToActivate);
        }
    }
    public void enableContact()
    {
        touchFlag = true;
        pressFlag = false;
        interactionBehaviour.ignoreContact = false;
    }
    public void Press()
    {
        if (pressFlag)
        {
            FindChild(transform.gameObject, "Left_Shank").transform.localRotation = Quaternion.Euler(pressRotationLeftShank);
            FindChild(transform.gameObject, "Right_Shank").transform.localRotation = Quaternion.Euler(pressRotationRightShank);
            if (showFakeHand)
            {
                FindChild(transform.gameObject, "R_Finger_Thumb_A").transform.localRotation = Quaternion.Euler(pressRotationThumb);
            }
        }
    }

    public void UnPress()
    {
        if (pressFlag)
        {
            FindChild(transform.gameObject, "Left_Shank").transform.localRotation = Quaternion.Euler(unpressRotationLeftShank);
            FindChild(transform.gameObject, "Right_Shank").transform.localRotation = Quaternion.Euler(unpressRotationRightShank);
            if (showFakeHand)
            {
                FindChild(transform.gameObject, "R_Finger_Thumb_A").transform.localRotation = Quaternion.Euler(unpressRotationThumb);
            }
        }
    }
}
