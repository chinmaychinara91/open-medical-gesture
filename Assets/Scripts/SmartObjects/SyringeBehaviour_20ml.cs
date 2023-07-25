/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenMg.SmartObjects;
public class SyringeBehaviour_20ml : SmartObjectBehaviour
{
    [Header("VARIABLES LOCAL TO THIS SMARTOBJECT", order = 0)]

    [Tooltip("Gameobject pointing to the Fluid component")]
    public GameObject fluidComponent;

    [Tooltip("Gameobject pointing to the Cap component")]
    public Transform capComponent;

    [Header("Press/Unpress SmartObject transforms", order = 1)]

    [Tooltip("source position")]
    public Vector3 syringeUnpressPosition;

    [Tooltip("destination position")]
    public Vector3 syringePressPosition;

    [Tooltip("source rotation")]
    public Vector3 syringeUnpressRotation;

    [Tooltip("destination rotation")]
    public Vector3 syringePressRotation;

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
                //transform.parent = anchor_l;
                //transform.localPosition = new Vector3(0.000209f, 0.00148f, 0f);
                //transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
                //hands_l_rend.material = fadeMat;
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
                transform.localPosition = new Vector3(-0.0001316255f, 0.00210285f, -4.635423e-05f);
                transform.localRotation = Quaternion.Euler(-94.696f, -433.53f, 353.586f);
            }
            interactionBehaviour.ignoreContact = true;
            rigidBody.isKinematic = true;
            rigidBody.useGravity = false;
            //GetComponent<Collider>().isTrigger = false;
            transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            transform.GetChild(0).GetComponent<Rigidbody>().useGravity = false;
            //transform.GetChild(0).GetComponent<Collider>().isTrigger = true;

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
            rigidBody.isKinematic = false;
            rigidBody.useGravity = true;
            //GetComponent<Collider>().isTrigger = false;
            transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            transform.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
            //transform.GetChild(0).GetComponent<Collider>().isTrigger = false;
            Invoke("enableContact", mainControl.GetComponent<ActivationDeactivationControls>().timeToActivate);
        }
    }
    public void enableContact()
    {
        touchFlag = true;
        pressFlag = false;
        interactionBehaviour.ignoreContact = false;
    }

    public void PressSyringe()
    {
        if (pressFlag)
        {
            StartCoroutine(
                LerpPosition(
                    FindChild(transform.gameObject, "Piston").transform,
                    FindChild(transform.gameObject, "Piston").transform.localPosition,
                    syringePressPosition,
                    0.5f));
            StartCoroutine(
                LerpScale(
                    fluidComponent.transform, fluidComponent.transform.localScale,
                    new Vector3(0, fluidComponent.transform.localScale.y,
                    fluidComponent.transform.localScale.z),
                    0.5f));

            if (showFakeHand)
            {
                StartCoroutine(
                    LerpRotation(
                        FindChild(transform.gameObject, "R_Finger_Thumb_A").transform,
                        FindChild(transform.gameObject, "R_Finger_Thumb_A").transform.localRotation,
                        Quaternion.Euler(syringePressRotation),
                        0.5f));
            }
        }
    }

    public void UnPressSyringe()
    {
        if (pressFlag)
        {
            StartCoroutine(
                LerpPosition(
                    FindChild(transform.gameObject, "Piston").transform,
                    FindChild(transform.gameObject, "Piston").transform.localPosition,
                    syringeUnpressPosition,
                    0.5f));

            if (showFakeHand)
            {
                StartCoroutine(
                    LerpRotation(
                        FindChild(transform.gameObject, "R_Finger_Thumb_A").transform,
                        FindChild(transform.gameObject, "R_Finger_Thumb_A").transform.localRotation,
                        Quaternion.Euler(syringeUnpressRotation),
                        0.5f));
            }
        }
    }
}
