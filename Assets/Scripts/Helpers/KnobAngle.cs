/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobAngle : MonoBehaviour
{
    [Header("Snap Options")]
    [Tooltip("If True the SnapGraphics tranfsorm will have its local Y rotation snapped to the nearest degrees specified in SnapDegrees")]
    public bool SnapToDegrees = false;

    [Tooltip("Snap the Y rotation to the nearest")]
    public float SnapDegrees = 5f;

    [Tooltip("The Transform of the object to be rotated if SnapToDegrees is true")]
    public Transform SnapGraphics;

    AudioSource audioSource;

    [Tooltip("Randomize pitch of SnapSound by this amount")]
    public float RandomizePitch = 0.001f;

    [Header("Text Label (Optional)")]
    public TextMesh LabelToUpdate;

    [Header("Change Events")]
    public FloatEvent onHingeChange;
    public FloatEvent onHingeSnapChange;

    Rigidbody rigid;

    private float _lastDegrees = 0;
    private float _lastSnapDegrees = 0;
    HingeJoint hj;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        hj = GetComponent<HingeJoint>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update degrees our transform is representing
        float degrees = getSmoothedValue(transform.localEulerAngles.y);

        // Call event if necessary
        if (degrees != _lastDegrees)
        {
            OnHingeChange(degrees);
        }

        _lastDegrees = degrees;

        // Check for snapping a graphics transform
        float nearestSnap = getSmoothedValue(Mathf.Round(degrees / SnapDegrees) * SnapDegrees);

        // If snapping update graphics and call events
        if (SnapToDegrees)
        {
            // Check for snap event
            if (nearestSnap != _lastSnapDegrees)
            {
                OnSnapChange(nearestSnap);
            }
            _lastSnapDegrees = nearestSnap;
        }

        float val = getSmoothedValue(SnapToDegrees ? nearestSnap : degrees);
        LabelToUpdate.GetComponent<TextMesh>().text = (val/SnapDegrees).ToString("n0");
    }

    public void OnSnapChange(float yAngle)
    {
        if (SnapGraphics)
        {
            SnapGraphics.localEulerAngles = new Vector3(SnapGraphics.localEulerAngles.x, yAngle, SnapGraphics.localEulerAngles.z);
        }

        // play audio
        audioSource.spatialize = true; // only Oculus supports it now
        audioSource.pitch = getRandomizedPitch(RandomizePitch);
        audioSource.spatialBlend = 1.0f;
        audioSource.volume = 1.0f;
        audioSource.Play();

        // Call event
        if (onHingeSnapChange != null)
        {
            onHingeSnapChange.Invoke(yAngle);
        }
    }

    public void OnHingeChange(float hingeAmount)
    {
        // Call event
        if (onHingeChange != null)
        {
            onHingeChange.Invoke(hingeAmount);
        }
    }
    float getRandomizedPitch(float randomAmount)
    {

        if (randomAmount != 0)
        {
            float randomPitch = Random.Range(-randomAmount, randomAmount);
            return Time.timeScale + randomPitch;
        }

        return Time.timeScale;
    }
    float getSmoothedValue(float val)
    {
        if (val < 0)
        {
            val = 360 - val;
        }
        if (val == 360)
        {
            val = 0;
        }

        return val;
    }
}
