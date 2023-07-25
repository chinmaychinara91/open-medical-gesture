/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasGrabbed : MonoBehaviour
{
    public bool hasObjectGrabbed = false;
    Renderer handRend;

    // Start is called before the first frame update
    void Start()
    {
        handRend = transform.GetChild(0).GetComponent<Renderer>();
        //print(handMaterialName);
    }

    // Update is called once per frame
    void Update()
    {
        if (handRend.material.name.Contains("opaque"))
        {
            hasObjectGrabbed = false;
        }
        if (handRend.material.name.Contains("fade"))
        {
            hasObjectGrabbed = true;
        }
    }
}
