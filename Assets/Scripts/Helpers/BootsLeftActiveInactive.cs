/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsLeftActiveInactive : MonoBehaviour
{
    public void Pressed()
    {
        if (!this.transform.gameObject.activeInHierarchy)
        {
            this.transform.gameObject.SetActive(true);
        }
        else
        {
            this.transform.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
