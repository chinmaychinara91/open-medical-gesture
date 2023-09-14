/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenMg.SmartObjects;
using System;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
using System.Runtime.CompilerServices;

public class SyringeBehaviour_50ml : SmartObjectBehaviour
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

    [Tooltip("source rotation")]
    public Vector3 syringeUnpressRotationOculus;

    [Tooltip("destination rotation")]
    public Vector3 syringePressRotationOculus;

    public HandGrabInteractable handGrabInteractable;

    override public void Awake()
    {
        base.Awake();
    }

    // oculus
    bool attachHandFlag = false;

    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
    }

    override public void AttachToHand()
    {
        base.AttachToHand();

        if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_FacingCeiling
            || device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_HmdMounted)
        {
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
                    transform.localPosition = new Vector3(-9.7e-05f, 0.002204f, 8e-06f);
                    transform.localRotation = Quaternion.Euler(-6.627f, -86.468f, -94.94801f);
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
        else if(device.m_DeviceName == HandTrackingDeviceController.DeviceName.Quest2)
        {
            if (touchFlag)
            {
                //transform.parent = rightHandAnchor;
                transform.localPosition = new Vector3(-9.7e-05f, 0.002204f, 8e-06f);
                transform.localRotation = Quaternion.Euler(-6.627f, -86.468f, -94.94801f);

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
    }

    override public void RemoveFromHand()
    {
        base.RemoveFromHand();

        if(isActiveAndEnabled)
        {
            StartCoroutine(RemoveFromHandCoroutine());
        }
    }

    private IEnumerator RemoveFromHandCoroutine()
    {
        if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_FacingCeiling
            || device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_HmdMounted)
        {
            if (touchFlag == false && fakeHand.activeSelf)
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
        else if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.Quest2)
        {
            if (touchFlag == false)
            {
                //transform.parent = homeTransform;
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
    }

    private void enableContact()
    {
        touchFlag = true;
        pressFlag = false;

        if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_FacingCeiling
            || device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_HmdMounted)
            interactionBehaviour.ignoreContact = false;
    }

    // local functions
    public void PressSyringe()
    {
        if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_FacingCeiling
            || device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_HmdMounted)
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
        else if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.Quest2)
        {
            if (pressFlag)
            {
                //StartCoroutine(
                //    LerpRotationHandPose(
                //        handGrabInteractable.GetComponent<HandGrabPose>().HandPose,
                //        handGrabInteractable.GetComponent<HandGrabPose>().HandPose.JointRotations[0],
                //        Quaternion.Euler(syringePressRotationOculus),
                //        0.5f));

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
            }
        }
    }

    public void UnPressSyringe()
    {
        if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_FacingCeiling
            || device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_HmdMounted)
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
        else if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.Quest2)
        {
            if (pressFlag)
            {
                //StartCoroutine(
                //LerpRotationHandPose(
                //    handGrabInteractable.GetComponent<HandGrabPose>().HandPose,
                //    handGrabInteractable.GetComponent<HandGrabPose>().HandPose.JointRotations[0],
                //    Quaternion.Euler(syringeUnpressRotationOculus),
                //    0.5f));

                StartCoroutine(
                    LerpPosition(
                        FindChild(transform.gameObject, "Piston").transform,
                        FindChild(transform.gameObject, "Piston").transform.localPosition,
                        syringeUnpressPosition,
                        0.5f));
            }
        }
    }

    override public void Update()
    {
        base.Update();

        if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.Quest2)
        {
            if (handGrabR.IsGrabbing && !attachHandFlag
                && handGrabR.SelectedInteractable.transform.name == "HandGrabInteractable_Syringe_Liquid_50ml")
            {
                AttachToHand();
                attachHandFlag = true;
            }
            else if (!handGrabR.IsGrabbing && attachHandFlag)
            {
                attachHandFlag = false;
                RemoveFromHand();
            }
        }
    }
}
