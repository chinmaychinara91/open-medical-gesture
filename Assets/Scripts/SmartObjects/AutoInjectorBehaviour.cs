/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenMg.SmartObjects;

public class AutoInjectorBehaviour : SmartObjectBehaviour
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
                transform.localPosition = new Vector3(-0.000394f, 0.00086f, 1e-05f);
                transform.localRotation = Quaternion.Euler(-80.982f, 85.84701f, -175.792f);
            }
            interactionBehaviour.ignoreContact = true;
            rigidBody.isKinematic = true;
            rigidBody.useGravity = false;
            //GetComponent<Collider>().isTrigger = false;
            transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            transform.GetChild(0).GetComponent<Rigidbody>().useGravity = false;
            //transform.GetChild(0).GetComponent<Collider>().isTrigger = true;

            touchFlag = false;
        }
    }

    override public void RemoveFromHand()
    {
        base.RemoveFromHand();

        StartCoroutine(RemoveFromHandCoroutine());
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
            //GetComponent<Collider>().isTrigger = false;
            transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            transform.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
            //transform.GetChild(0).GetComponent<Collider>().isTrigger = false;
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
