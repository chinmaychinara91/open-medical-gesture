using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OmgGestureDetection))]
public class OmgGestureBase : MonoBehaviour
{
    public bool m_Debug;

    bool m_Isdetected = false;

    public bool IsDetected()
    {
        bool detect = Detected();

        if (m_Isdetected != detect)
        {
            if (m_Debug)
            {
                string response = detect ? "detected" : "undetected";
                Debug.Log(GetType().Name + ": " + response);
            }

            m_Isdetected = detect;
        }

        return detect;
    }

    public virtual bool Detected()
    {
        return false;
    }

}
