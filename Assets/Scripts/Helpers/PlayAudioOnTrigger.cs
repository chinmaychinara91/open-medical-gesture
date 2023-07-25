/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    public float minVelocity = 0;
    public float maxVelocity = 2;
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("capsule"))
        {
            VelocityEstimator estimator = other.GetComponent<VelocityEstimator>();
            float v = estimator.GetVelocityEstimate().magnitude;
            float volume = Mathf.InverseLerp(minVelocity, maxVelocity, v);
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(clip, volume); //, volume);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
