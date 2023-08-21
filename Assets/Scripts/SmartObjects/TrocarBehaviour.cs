/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenMg.SmartObjects;

public class TrocarBehaviour : SmartObjectBehaviour
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

        if (touchFlag)// && (!rightHand.GetComponent<HasGrabbed>().hasObjectGrabbed))
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
            rigidBody.isKinematic = true;
            rigidBody.useGravity = false;

            touchFlag = false;
        }
    }

    override public void RemoveFromHand()
    {
        base.RemoveFromHand();

        if (isActiveAndEnabled)
        {
            StartCoroutine(RemoveFromHandCoroutine());
        }
    }

    private IEnumerator RemoveFromHandCoroutine()
    {
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
            yield return new WaitForSeconds(1.0f);
            enableContact();
        }
    }

    private void enableContact()
    {
        touchFlag = true;
        interactionBehaviour.ignoreContact = false;
    }
}
