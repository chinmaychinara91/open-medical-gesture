using Oculus.Interaction.Grab;
using Oculus.Interaction.GrabAPI;
using Oculus.Interaction.HandGrab;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public struct LeapHandGrabData
{
    public Vector3 hand_pos;
    public Quaternion hand_rot;
    public List<Vector3> index_pos;
    public List<Quaternion> index_rot;
    public List<Vector3> middle_pos;
    public List<Quaternion> middle_rot;
    public List<Vector3> pinky_pos;
    public List<Quaternion> pinky_rot;
    public List<Vector3> ring_pos;
    public List<Quaternion> ring_rot;
    public List<Vector3> thumb_pos;
    public List<Quaternion> thumb_rot;
}

public class LeapHandGrabDataCollection : ScriptableObject
{
    [SerializeField]
    private LeapHandGrabData _interactablesData;

    public LeapHandGrabData InteractablesData => _interactablesData;

    public void StoreInteractables(LeapHandGrabData interactablesData)
    {
        _interactablesData = interactablesData;
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }
}
