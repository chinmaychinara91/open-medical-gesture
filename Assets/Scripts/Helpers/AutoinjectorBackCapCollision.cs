/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoinjectorBackCapCollision : MonoBehaviour
{
    public GameObject refParent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RemoveCap()
    {
        transform.parent = refParent.transform;
        //transform.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
}
