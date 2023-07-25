/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmgPinchGesture : OmgGestureBase
{

    public EHand m_Hand;

    void Start()
    {

    }

    void Update()
    {

    }

    public override bool Detected()
    {
        if (OmgGestureDetectionManager.Get().IsHandSet(m_Hand))
        {
            return OmgGestureDetectionManager.Get().GetHand(m_Hand).IsPinching();
        }

        return false;
    }
}

