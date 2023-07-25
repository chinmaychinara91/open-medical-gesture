/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionManager : MonoBehaviour
{
    private AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        aud = this.transform.GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1)
            aud.Play();
    }

    void OnCollisionStay(Collision collision)
    {

    }

    void OnCollisionExit(Collision collision)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
