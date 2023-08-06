/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System;
using TMPro;
using UnityEngine;

public class HandTrackingDeviceController : MonoBehaviour 
{
    public enum DeviceName 
    {
        LeapMotion_FacingCeiling,
        LeapMotion_HmdMounted,
        Quest2 
    }

    public DeviceName m_DeviceName = DeviceName.LeapMotion_FacingCeiling;
    public bool isUsingRightHand = true;
    public OpenMGHandTrackingProvider m_DataProvider;
    public GameObject[] sensors;

    //public GameObject[] Hand;
    public GameObject m_Joint_Prefab;
    private GameObject[] joints = new GameObject[10];
    private bool display_joints = false;

    // Use this for initialization
    void Awake() 
    {        
        switch (m_DeviceName)
        {
            case DeviceName.LeapMotion_FacingCeiling:
                {
                    m_DataProvider = new LeapMotionHandTrackingProvider();
                    m_DataProvider.m_DataSource = sensors[(int)DeviceName.LeapMotion_FacingCeiling];
                    sensors[(int)DeviceName.Quest2].SetActive(false);
                    sensors[(int)DeviceName.LeapMotion_HmdMounted].transform.parent.gameObject.SetActive(false);
                    break;
                }
            case DeviceName.LeapMotion_HmdMounted:
                {
                    m_DataProvider = new LeapMotionHandTrackingProvider();
                    m_DataProvider.m_DataSource = sensors[(int)DeviceName.LeapMotion_HmdMounted];
                    sensors[(int)DeviceName.Quest2].SetActive(false);
                    sensors[(int)DeviceName.LeapMotion_FacingCeiling].transform.parent.gameObject.SetActive(false);
                    break;
                }
            case DeviceName.Quest2:
                {
                    m_DataProvider = new Quest2HandTrackingProvider();
                    m_DataProvider.m_DataSource = sensors[(int)DeviceName.Quest2];
                    sensors[(int)DeviceName.LeapMotion_FacingCeiling].transform.parent.gameObject.SetActive(false);
                    sensors[(int)DeviceName.LeapMotion_HmdMounted].transform.parent.gameObject.SetActive(false);
                    //display_joints = true;
                    break;
                }
        }

        for (int i = 0; i < joints.Length; i++)
        {
            joints[i] = Instantiate(m_Joint_Prefab);
            joints[i].GetComponent<MeshRenderer>().enabled = display_joints;
        }

        m_DataProvider.Start();
    }

    // Update is called once per frame
    void Update () 
    {
        m_DataProvider.Update();
        ManipulateJpoints();

        transform.GetChild(0).GetComponent<TMP_Text>().text = m_DataProvider.leftHandDebug;
        transform.GetChild(1).GetComponent<TMP_Text>().text = m_DataProvider.rightHandDebug;
    }

    public void OnDestroy()
    {
        m_DataProvider.Close();
    }
    public void ManipulateJpoints()
    {
        int count = 0;
        for(int i=0; i<2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                joints[count].transform.position = m_DataProvider.GetTipPosition(i, j);
                count++;
            }
        }
    }

    public void SwitchDevice(string device)
    {
        m_DeviceName = (DeviceName)Enum.Parse(typeof(DeviceName), device, true); // case insensitive
        m_DataProvider.Close();
        switch (m_DeviceName)
        {
            case DeviceName.LeapMotion_FacingCeiling:
                {
                    m_DataProvider = new LeapMotionHandTrackingProvider();
                    m_DataProvider.m_DataSource = sensors[(int)DeviceName.LeapMotion_FacingCeiling];
                    sensors[(int)DeviceName.Quest2].SetActive(false);
                    sensors[(int)DeviceName.LeapMotion_HmdMounted].transform.parent.gameObject.SetActive(false);
                    break;
                }
            case DeviceName.LeapMotion_HmdMounted:
                {
                    m_DataProvider = new LeapMotionHandTrackingProvider();
                    m_DataProvider.m_DataSource = sensors[(int)DeviceName.LeapMotion_HmdMounted];
                    sensors[(int)DeviceName.Quest2].SetActive(false);
                    sensors[(int)DeviceName.LeapMotion_FacingCeiling].transform.parent.gameObject.SetActive(false);
                    break;
                }
            case DeviceName.Quest2:
                {
                    m_DataProvider = new Quest2HandTrackingProvider();
                    m_DataProvider.m_DataSource = sensors[(int)DeviceName.Quest2];
                    sensors[(int)DeviceName.LeapMotion_FacingCeiling].transform.parent.gameObject.SetActive(false);
                    sensors[(int)DeviceName.LeapMotion_HmdMounted].transform.parent.gameObject.SetActive(false);
                    //display_joints = true;
                    break;
                }
        }
        for (int i = 0; i < joints.Length; i++)
        {
            joints[i].GetComponent<MeshRenderer>().enabled = display_joints;
        }
        m_DataProvider.Start();
    }
}
