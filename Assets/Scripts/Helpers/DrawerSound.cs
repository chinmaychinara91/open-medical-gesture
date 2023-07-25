/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerSound : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip DrawerOpenSound;
    public float DrawerOpenValue = 80f;

    public AudioClip DrawerCloseSound;
    public float DrawerCloseValue = 20f;

    bool playedOpenSound = false;
    bool playedCloseSound = false;

    public float debugDrawerValue;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnDrawerUpdate(float drawerValue)
    {
        debugDrawerValue = drawerValue;
        if (drawerValue >= 50f)
        {
            audioSource.enabled = false;
        }
        else
        {
            audioSource.enabled = true;
        }

        // Open Sound
        if (drawerValue < DrawerOpenValue && !playedOpenSound && DrawerOpenSound != null)
        {
            //VRUtils.Instance.PlaySpatialClipAt(DrawerOpenSound, transform.position, 1f);
            audioSource.clip = DrawerOpenSound;
            audioSource.spatialize = true; // only Oculus supports it now
            audioSource.pitch = 1.0f;
            audioSource.spatialBlend = 1.0f;
            audioSource.volume = 1.0f;
            audioSource.Play();
            playedOpenSound = true;
        }
        // Reset Open Sound
        if (drawerValue > DrawerOpenValue)
        {
            playedOpenSound = false;
        }

        // Close Sound
        if (drawerValue > DrawerCloseValue && !playedCloseSound && DrawerCloseSound != null)
        {
            //VRUtils.Instance.PlaySpatialClipAt(DrawerCloseSound, transform.position, 1f);
            audioSource.clip = DrawerCloseSound;
            audioSource.spatialize = true; // only Oculus supports it now
            audioSource.pitch = 1.0f;
            audioSource.spatialBlend = 1.0f;
            audioSource.volume = 1.0f;
            audioSource.Play();
            playedCloseSound = true;
        }

        // Reset Close Sound
        if (drawerValue < DrawerCloseValue)
        {
            playedCloseSound = false;
        }
    }
}
