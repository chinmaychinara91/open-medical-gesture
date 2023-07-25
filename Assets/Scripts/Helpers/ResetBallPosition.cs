/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBallPosition : MonoBehaviour
{
    private Vector3 localPos;
    private Quaternion localRot;

    // Start is called before the first frame update
    void Start()
    {
        localPos = transform.localPosition;
        localRot = transform.localRotation;
    }

    public void Reset()
    {
        transform.localPosition = localPos;
        transform.localRotation = localRot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
