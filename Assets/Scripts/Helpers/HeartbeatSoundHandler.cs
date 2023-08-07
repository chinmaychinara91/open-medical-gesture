﻿/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class HeartbeatSoundHandler : MonoBehaviour
{
    public HandTrackingDeviceController device;
    AudioSource aud;
    public GameObject handFacingGesture;
    public GameObject trackedLeftHandModel;
    public GameObject trackedRightHandModel;
    public Material opaqueMat;
    public Material fadeMat;
    public Material fadeMatFull;
    bool touched = false;

    public GameObject rigthHandOculus;

    // for temporary instantiation of the hand
    GameObject rightHand_temp; 
    GameObject leftHand_temp;


    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        //aud.time = aud.clip.length * .8f;
        if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_FacingCeiling)
        {
            trackedLeftHandModel = FindChild(device.sensors[(int)HandTrackingDeviceController.DeviceName.LeapMotion_FacingCeiling].transform.parent.gameObject, "Hand_L");
            trackedRightHandModel = FindChild(device.sensors[(int)HandTrackingDeviceController.DeviceName.LeapMotion_FacingCeiling].transform.parent.gameObject, "Hand_R");
        }
        else if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_HmdMounted)
        {
            trackedLeftHandModel = FindChild(device.sensors[(int)HandTrackingDeviceController.DeviceName.LeapMotion_HmdMounted].transform.parent.gameObject, "Hand_L");
            trackedRightHandModel = FindChild(device.sensors[(int)HandTrackingDeviceController.DeviceName.LeapMotion_HmdMounted].transform.parent.gameObject, "Hand_R");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.Quest2 && Input.GetKeyDown(KeyCode.W))
        {
            StopAudioOculus();
        }
    }

    public void PlayAudio()
    {
        if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_FacingCeiling
            || device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_HmdMounted)
        { 
            if (handFacingGesture.GetComponent<OmgHandFacingGesture>().Detected() & !touched)
            {
                aud.Play();
                aud.loop = true;

                rightHand_temp = Instantiate(trackedRightHandModel, trackedRightHandModel.transform.parent);
                Destroy(rightHand_temp.transform.GetComponent<RiggedHand>());
                Destroy(rightHand_temp.transform.GetComponent<HandEnableDisable>());
                Destroy(rightHand_temp.transform.GetComponent<HasGrabbed>());
                rightHand_temp.SetActive(true);

                trackedRightHandModel.transform.GetChild(0).GetComponent<Renderer>().material = fadeMatFull;
                touched = true;
            }
        }
    }
    public void StopAudio()
    {
        if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_FacingCeiling
            || device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_HmdMounted)
        {
            aud.loop = false;
            aud.Stop();

            Destroy(rightHand_temp);
            trackedRightHandModel.transform.GetChild(0).GetComponent<Renderer>().material = opaqueMat;

            touched = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.Quest2)
        {
            if (collision.gameObject.tag == "PlaneOculusRightHand")
            {
                PlayAudioOculus();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.Quest2)
        {
            StopAudioOculus();
        }
    }

    public void PlayAudioOculus()
    {
        rigthHandOculus.GetComponent<OVRHand>().enabled = false;
        aud.Play();
        aud.loop = true;
    }

    public void StopAudioOculus()
    {
        aud.loop = false;
        aud.Stop();
        rigthHandOculus.GetComponent<OVRHand>().enabled = true;
    }

    // find child of a gameobject with certain name
    public GameObject FindChild(GameObject parent, string childName)
    {
        Transform[] everyChildren = parent.GetComponentsInChildren<Transform>(true);

        foreach (var child in everyChildren)
        {
            if (child.name == childName)
            {
                return child.gameObject;
            }
        }

        return null;
    }
}
