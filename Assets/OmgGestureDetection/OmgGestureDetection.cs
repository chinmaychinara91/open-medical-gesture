/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using UnityEngine;
using UnityEngine.Events;

public class OmgGestureDetection : MonoBehaviour
{
    [Space(5)]
    OmgGestureBase[] m_Gestures;

    [Space(5)]
    public UnityEvent OnGestureStart;
    public UnityEvent OnGestureEnd;
    public UnityEvent OnGestureStay;

    public bool m_bDetected = false;

    void Awake()
    {
        m_Gestures = gameObject.GetComponents<OmgGestureBase>();
    }

    void Start()
    {

    }

    void Update()
    {
        int NumTrue = 0;

        foreach (OmgGestureBase gesture in m_Gestures)
        {
            if (gesture.IsDetected())
            {
                NumTrue++;
            }
        }

        if (NumTrue >= m_Gestures.Length)
        {
            if (!m_bDetected)
            {
                OnGestureStart.Invoke();
                m_bDetected = true;
            }

            OnGestureStay.Invoke();
        }
        else
        {
            if (m_bDetected)
            {
                OnGestureEnd.Invoke();
                m_bDetected = false;
            }
        }
    }
}
