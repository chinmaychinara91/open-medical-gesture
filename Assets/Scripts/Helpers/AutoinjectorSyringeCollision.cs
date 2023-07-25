/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class AutoinjectorSyringeCollision : MonoBehaviour
{
    public AudioSource aud;
    public GameObject g;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == g)
        {
            aud.Play();
            transform.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
