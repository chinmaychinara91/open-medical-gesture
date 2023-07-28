/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using Oculus.Interaction;
using UnityEngine;
using OpenMg.SmartObjects;

public class ScalpelBehaviour : SmartObjectBehaviour
{
    public HandTrackingDeviceController _controller;

    [Header("VARIABLES LOCAL TO THIS SMARTOBJECT", order = 0)]

    [Header("Position/Rotation of SmartObject about Anchor", order = 1)]

    [Tooltip("Position at anchor Left Hand")]
    public Vector3 _positionAtLeftHandAnchor;

    [Tooltip("Rotation at anchor Left Hand")]
    public Vector3 _rotationAtLeftHandAnchor;

    [Tooltip("Position at anchor Right Hand")]
    public Vector3 _positionAtRightHandAnchor;

    [Tooltip("Rotation at anchor Right Hand")]
    public Vector3 _rotationAtRightHandAnchor;

    // Oculus
    //public TMPro.TextMeshPro debugText;

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
                if (showFakeHand)
                {
                    fakeHandLeft.SetActive(true);
                    leftHandRenderer.material = fadeMatFull;
                }
                else
                {
                    leftHandRenderer.material = fadeMatPartial;
                }
                transform.parent = leftHandAnchor;
                transform.localPosition = _positionAtLeftHandAnchor;
                transform.localRotation = Quaternion.Euler(_rotationAtLeftHandAnchor);
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
                transform.localPosition = _positionAtRightHandAnchor;
                transform.localRotation = Quaternion.Euler(_rotationAtRightHandAnchor);
            }
            
            interactionBehaviour.ignoreContact = true;
            rigidBody.isKinematic = true;
            rigidBody.useGravity = false;
            
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
            rigidBody.isKinematic = false;
            rigidBody.useGravity = true;
            Invoke("enableContact", mainControl.GetComponent<ActivationDeactivationControls>().timeToActivate);
        }
    }
    public void enableContact()
    {
        touchFlag = true;
        interactionBehaviour.ignoreContact = false;
    }
}
