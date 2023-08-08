/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using UnityEngine;
using Leap.Unity.Interaction;
using Oculus.Interaction.HandGrab;
using Leap.Unity;

namespace OpenMg.SmartObjects
{
    // use this as base class for any smart object behaviour
    public class SmartObjectBehaviour : MonoBehaviour
    {
        // public variables
        [Tooltip("Transform pointing to the Left Hand")]
        public Transform leftHand;

        [Tooltip("Transform pointing to the Right Hand")]
        public Transform rightHand;

        [Tooltip("Opaque material")]
        public Material opaqueMat;

        [Tooltip("Partially faded material")]
        public Material fadeMatPartial;

        [Tooltip("Fully faded material")]
        public Material fadeMatFull;

        [Tooltip("The base transform of the object")]
        public Transform homeTransform;

        [Tooltip("Control Activate/Deactivate time for contact")]
        public Transform mainControl;

        [Tooltip("The fake hand holding the smart object")]
        public GameObject fakeHand;

        [Tooltip("The fake hand holding the smart object")]
        public GameObject fakeHandLeft;

        [Tooltip("The fake hand holding the smart object")]
        public bool showFakeHand;


        // Hidden in Inspector (will be asigned at runtime)
        [HideInInspector]
        public InteractionBehaviour interactionBehaviour; // get the interaction attached to the tracked hand(s)

        [HideInInspector]
        public Renderer leftHandRenderer; // to toggle between left tracked hand and positioned hand render

        [HideInInspector]
        public Renderer rightHandRenderer; // to toggle between right tracked hand and positioned hand render

        [HideInInspector]
        public Transform leftHandAnchor; // about left wrist (maunually place the smartobject aligned under left wrist and note the transform)

        [HideInInspector]
        public Transform rightHandAnchor; // about right wrist (maunually place the smartobject aligned under right wrist and note the transform)

        [HideInInspector]
        public bool touchFlag = true; // for picking up an object

        [HideInInspector]
        public bool pressFlag = false; // for instruments with press functionality e.g. syringes

        [HideInInspector]
        public Rigidbody rigidBody; // get the attached Rigidbody 

        [HideInInspector]
        public Vector3 origPos; // capture start postion of object (may come to use later)

        [HideInInspector]
        public Quaternion origRot; // capture start rotation of object (may come to use later)

        //[HideInInspector]
        public HandGrabInteractor handGrabR; // get oculus right hand grab status

        public HandTrackingDeviceController device;

        public virtual void Awake()
        {
            origPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            origRot = new Quaternion(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
        }

        public virtual void Start()
        {
            if(device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_FacingCeiling)
            {
                leftHand = FindChild(device.sensors[(int)HandTrackingDeviceController.DeviceName.LeapMotion_FacingCeiling].transform.parent.gameObject, "Hand_L").transform;
                rightHand = FindChild(device.sensors[(int)HandTrackingDeviceController.DeviceName.LeapMotion_FacingCeiling].transform.parent.gameObject, "Hand_R").transform;
            }
            else if (device.m_DeviceName == HandTrackingDeviceController.DeviceName.LeapMotion_HmdMounted)
            {
                leftHand = FindChild(device.sensors[(int)HandTrackingDeviceController.DeviceName.LeapMotion_HmdMounted].transform.parent.gameObject, "Hand_L").transform;
                rightHand = FindChild(device.sensors[(int)HandTrackingDeviceController.DeviceName.LeapMotion_HmdMounted].transform.parent.gameObject, "Hand_R").transform;
            }
            if (device.m_DeviceName != HandTrackingDeviceController.DeviceName.Quest2)
            {
                leftHandRenderer = FindChild(leftHand.gameObject, "LeftHandRenderer").GetComponent<Renderer>();
                rightHandRenderer = FindChild(rightHand.gameObject, "RightHandRenderer").GetComponent<Renderer>();
                leftHandAnchor = FindChild(leftHand.gameObject, "L_Wrist").transform;
                rightHandAnchor = FindChild(rightHand.gameObject, "R_Wrist").transform;
                interactionBehaviour = GetComponent<InteractionBehaviour>();
            }
            rigidBody = GetComponent<Rigidbody>();
        }

        public virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RemoveFromHand();
            }
        }

        // attach instrument to Hand
        public virtual void AttachToHand()
        {

        }

        // Remove instrument from Hand
        public virtual void RemoveFromHand()
        {

        }

        #region Helper functions
        // find child of a gameobject with certain name
        public GameObject FindChild(GameObject parent, string childName)
        {
            Transform[] everyChildren = parent.GetComponentsInChildren<Transform>(true);

            foreach (var child in everyChildren)
            {
                if (child.name == childName)
                {
                    return child.gameObject;
                }
            }

            return null;
        }

        // currently used by syringes only
        public IEnumerator LerpScale(Transform transformToChange, Vector3 startScale, Vector3 targetScale, float duration)
        {
            float time = 0;
            while (time < duration)
            {
                transformToChange.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            transformToChange.localScale = targetScale;
        }

        // currently used by syringes only
        public IEnumerator LerpPosition(Transform transformToChange, Vector3 startPosition, Vector3 targetPosition, float duration)
        {
            float time = 0;
            while (time < duration)
            {
                transformToChange.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            transformToChange.localPosition = targetPosition;
        }

        // currently used by syringes only
        public IEnumerator LerpRotation(Transform transformToChange, Quaternion startValue, Quaternion endValue, float duration)
        {
            float time = 0;
            while (time < duration)
            {
                transformToChange.localRotation = Quaternion.Lerp(startValue, endValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            transformToChange.localRotation = endValue;
        }

        // currently used by syringes only
        public IEnumerator LerpRotationHandPose(HandPose transformToChange, Quaternion startValue, Quaternion endValue, float duration)
        {
            float time = 0;
            while (time < duration)
            {
                transformToChange.JointRotations[0] = Quaternion.Lerp(startValue, endValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            transformToChange.JointRotations[0] = endValue;
        }
        #endregion Helper functions
    }
}
