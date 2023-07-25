/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmgFistGesture : OmgGestureBase
{
    public EHand m_Hand;
    public Transform handFacingDown;

    [Range(0.0f, 1.0f)]
    public float m_ClosedPercentage = 0.6f;

    public override bool Detected()
    {
        if (OmgGestureDetectionManager.Get().IsHandSet(m_Hand))
        {
            if (OmgGestureDetectionManager.Get().GetHand(m_Hand).IsClosed(m_ClosedPercentage))
            {
                return true;
            }
        }

        return false;
    }
}