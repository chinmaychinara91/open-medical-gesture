using Oculus.Interaction.HandGrab;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LeapHandGrabPoseWizard : EditorWindow
{
    [SerializeField]
    private GameObject _hand;

    [SerializeField]
    private GameObject _dummyHand;

    [SerializeField]
    private KeyCode _recordKey = KeyCode.Space;

    private GUIStyle _richTextStyle;
    private Vector2 _scrollPos = Vector2.zero;

    [SerializeField]
    private LeapHandGrabDataCollection _posesCollection;

    LeapHandGrabData savedPoses = new LeapHandGrabData();

    [MenuItem("Leap/Interaction/Leap Hand Grab Pose Recorder")]
    private static void CreateWizard()
    {
        LeapHandGrabPoseWizard window = GetWindow<LeapHandGrabPoseWizard>();
        window.titleContent = new GUIContent("Leap Hand Grab Pose Recorder");
        window.Show();
    }

    private void OnEnable()
    {
        _richTextStyle = EditorGUIUtility.GetBuiltinSkin(EditorGUIUtility.isProSkin ? EditorSkin.Scene : EditorSkin.Inspector).label;
        _richTextStyle.richText = true;
        _richTextStyle.wordWrap = true;

    }

    //void Start()
    //{
    //    _dummyHand.GetComponent<Renderer>().material.SetFloat("_Mode", 2);
    //    Color color = _dummyHand.GetComponent<Renderer>().material.color;
    //    color.a = 0.5f;
    //    _dummyHand.GetComponent<Renderer>().material.color = color;
    //}

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown
            && e.keyCode == _recordKey)
        {
            RecordPose();
            e.Use();
        }
        GUILayout.Label("Generate HandGrabPoses for grabbing an item <b>using your Hand in Play Mode</b>.\nThen Store and retrieve them in Edit Mode to persist and tweak them.", _richTextStyle);

        _scrollPos = GUILayout.BeginScrollView(_scrollPos);
        GUILayout.Space(20);
        GUILayout.Label("<size=20>1</size>\nAssign the hand that will be tracked and the item for which you want to record HandGrabPoses", _richTextStyle);
        GUILayout.Label("Leap Hand used for recording poses:");
        GenerateObjectField(ref _hand);
        GUILayout.Label("Dummy Hand to record the hand grab poses for:");
        GenerateObjectField(ref _dummyHand);


        GUILayout.Space(20);
        GUILayout.Label("<size=20>2</size>", _richTextStyle);
        GUILayout.Label($"Press the big <b>Record</b> button with your free hand\nor the <b>{_recordKey}</b> key to record a HandGrabPose <b>(requires focus on this window)</b>.", _richTextStyle);
        _recordKey = (KeyCode)EditorGUILayout.EnumPopup(_recordKey);
        if (GUILayout.Button("Record HandGrabPose", GUILayout.Height(100)))
        {
            RecordPose();
        }

        GUILayout.Space(20);
        GUILayout.Label("<size=20>3</size>\nStore your poses before exiting <b>Play Mode</b>.\nIf no collection is provided <b>it will autogenerate one</b>", _richTextStyle);
        GenerateObjectField(ref _posesCollection);
        if (GUILayout.Button("Save To Collection"))
        {
            SaveToAsset();
        }

        GUILayout.Space(20);
        GUILayout.Label("<size=20>4</size>\nNow load the poses from the PosesCollection in <b>Edit Mode</b> to tweak and persist them as gameobjects", _richTextStyle);
        if (GUILayout.Button("Load From Collection"))
        {
            LoadFromAsset();
        }
        GUILayout.EndScrollView();
    }
    private void GenerateObjectField<T>(ref T obj) where T : Object
    {
        obj = EditorGUILayout.ObjectField(obj, typeof(T), true) as T;
    }

    public void RecordPose()
    {
        savedPoses = new LeapHandGrabData();
        
        GameObject main_source = FindChild(_hand, "HandContainer");
        GameObject main_target = _dummyHand;

        main_target.transform.position = main_source.transform.position;
        main_target.transform.rotation = main_source.transform.rotation;
        savedPoses.hand_pos = main_target.transform.position;
        savedPoses.hand_rot = main_target.transform.rotation;

        GameObject wrist_source = FindChild(_hand, "R_Wrist");
        GameObject wrist_target = FindChild(_dummyHand, "R_Wrist");

        // index
        savedPoses.index_pos = new List<Vector3>();
        savedPoses.index_rot = new List<Quaternion>();

        wrist_target.transform.GetChild(0).localPosition = wrist_source.transform.GetChild(0).localPosition;
        wrist_target.transform.GetChild(0).localRotation = wrist_source.transform.GetChild(0).localRotation;
        savedPoses.index_pos.Add(wrist_target.transform.GetChild(0).localPosition);
        savedPoses.index_rot.Add(wrist_target.transform.GetChild(0).localRotation);

        wrist_target.transform.GetChild(0).GetChild(0).localPosition = wrist_source.transform.GetChild(0).GetChild(0).localPosition;
        wrist_target.transform.GetChild(0).GetChild(0).localRotation = wrist_source.transform.GetChild(0).GetChild(0).localRotation;
        savedPoses.index_pos.Add(wrist_target.transform.GetChild(0).GetChild(0).localPosition);
        savedPoses.index_rot.Add(wrist_target.transform.GetChild(0).GetChild(0).localRotation);

        wrist_target.transform.GetChild(0).GetChild(0).GetChild(0).localPosition = wrist_source.transform.GetChild(0).GetChild(0).GetChild(0).localPosition;
        wrist_target.transform.GetChild(0).GetChild(0).GetChild(0).localRotation = wrist_source.transform.GetChild(0).GetChild(0).GetChild(0).localRotation;
        savedPoses.index_pos.Add(wrist_target.transform.GetChild(0).GetChild(0).GetChild(0).localPosition);
        savedPoses.index_rot.Add(wrist_target.transform.GetChild(0).GetChild(0).GetChild(0).localRotation);

        // middle
        savedPoses.middle_pos = new List<Vector3>();
        savedPoses.middle_rot = new List<Quaternion>();

        wrist_target.transform.GetChild(1).localPosition = wrist_source.transform.GetChild(1).localPosition;
        wrist_target.transform.GetChild(1).localRotation = wrist_source.transform.GetChild(1).localRotation;
        savedPoses.middle_pos.Add(wrist_target.transform.GetChild(1).localPosition);
        savedPoses.middle_rot.Add(wrist_target.transform.GetChild(1).localRotation);

        wrist_target.transform.GetChild(1).GetChild(0).localPosition = wrist_source.transform.GetChild(1).GetChild(0).localPosition;
        wrist_target.transform.GetChild(1).GetChild(0).localRotation = wrist_source.transform.GetChild(1).GetChild(0).localRotation;
        savedPoses.middle_pos.Add(wrist_target.transform.GetChild(1).GetChild(0).localPosition);
        savedPoses.middle_rot.Add(wrist_target.transform.GetChild(1).GetChild(0).localRotation);

        wrist_target.transform.GetChild(1).GetChild(0).GetChild(0).localPosition = wrist_source.transform.GetChild(1).GetChild(0).GetChild(0).localPosition;
        wrist_target.transform.GetChild(1).GetChild(0).GetChild(0).localRotation = wrist_source.transform.GetChild(1).GetChild(0).GetChild(0).localRotation;
        savedPoses.middle_pos.Add(wrist_target.transform.GetChild(1).GetChild(0).GetChild(0).localPosition);
        savedPoses.middle_rot.Add(wrist_target.transform.GetChild(1).GetChild(0).GetChild(0).localRotation);

        // pinky
        savedPoses.pinky_pos = new List<Vector3>();
        savedPoses.pinky_rot = new List<Quaternion>();

        wrist_target.transform.GetChild(2).localPosition = wrist_source.transform.GetChild(2).localPosition;
        wrist_target.transform.GetChild(2).localRotation = wrist_source.transform.GetChild(2).localRotation;
        savedPoses.pinky_pos.Add(wrist_target.transform.GetChild(2).localPosition);
        savedPoses.pinky_rot.Add(wrist_target.transform.GetChild(2).localRotation);

        wrist_target.transform.GetChild(2).GetChild(0).localPosition = wrist_source.transform.GetChild(2).GetChild(0).localPosition;
        wrist_target.transform.GetChild(2).GetChild(0).localRotation = wrist_source.transform.GetChild(2).GetChild(0).localRotation;
        savedPoses.pinky_pos.Add(wrist_target.transform.GetChild(2).GetChild(0).localPosition);
        savedPoses.pinky_rot.Add(wrist_target.transform.GetChild(2).GetChild(0).localRotation);

        wrist_target.transform.GetChild(2).GetChild(0).GetChild(0).localPosition = wrist_source.transform.GetChild(2).GetChild(0).GetChild(0).localPosition;
        wrist_target.transform.GetChild(2).GetChild(0).GetChild(0).localRotation = wrist_source.transform.GetChild(2).GetChild(0).GetChild(0).localRotation;
        savedPoses.pinky_pos.Add(wrist_target.transform.GetChild(2).GetChild(0).GetChild(0).localPosition);
        savedPoses.pinky_rot.Add(wrist_target.transform.GetChild(2).GetChild(0).GetChild(0).localRotation);

        // ring
        savedPoses.ring_pos = new List<Vector3>();
        savedPoses.ring_rot = new List<Quaternion>();

        wrist_target.transform.GetChild(3).localPosition = wrist_source.transform.GetChild(3).localPosition;
        wrist_target.transform.GetChild(3).localRotation = wrist_source.transform.GetChild(3).localRotation;
        savedPoses.ring_pos.Add(wrist_target.transform.GetChild(3).localPosition);
        savedPoses.ring_rot.Add(wrist_target.transform.GetChild(3).localRotation);

        wrist_target.transform.GetChild(3).GetChild(0).localPosition = wrist_source.transform.GetChild(3).GetChild(0).localPosition;
        wrist_target.transform.GetChild(3).GetChild(0).localRotation = wrist_source.transform.GetChild(3).GetChild(0).localRotation;
        savedPoses.ring_pos.Add(wrist_target.transform.GetChild(3).GetChild(0).localPosition);
        savedPoses.ring_rot.Add(wrist_target.transform.GetChild(3).GetChild(0).localRotation);

        wrist_target.transform.GetChild(3).GetChild(0).GetChild(0).localPosition = wrist_source.transform.GetChild(3).GetChild(0).GetChild(0).localPosition;
        wrist_target.transform.GetChild(3).GetChild(0).GetChild(0).localRotation = wrist_source.transform.GetChild(3).GetChild(0).GetChild(0).localRotation;
        savedPoses.ring_pos.Add(wrist_target.transform.GetChild(3).GetChild(0).GetChild(0).localPosition);
        savedPoses.ring_rot.Add(wrist_target.transform.GetChild(3).GetChild(0).GetChild(0).localRotation);

        // thumb
        savedPoses.thumb_pos = new List<Vector3>();
        savedPoses.thumb_rot = new List<Quaternion>();

        wrist_target.transform.GetChild(4).localPosition = wrist_source.transform.GetChild(4).localPosition;
        wrist_target.transform.GetChild(4).localRotation = wrist_source.transform.GetChild(4).localRotation;
        savedPoses.thumb_pos.Add(wrist_target.transform.GetChild(4).localPosition);
        savedPoses.thumb_rot.Add(wrist_target.transform.GetChild(4).localRotation);

        wrist_target.transform.GetChild(4).GetChild(0).localPosition = wrist_source.transform.GetChild(4).GetChild(0).localPosition;
        wrist_target.transform.GetChild(4).GetChild(0).localRotation = wrist_source.transform.GetChild(4).GetChild(0).localRotation;
        savedPoses.thumb_pos.Add(wrist_target.transform.GetChild(4).GetChild(0).localPosition);
        savedPoses.thumb_rot.Add(wrist_target.transform.GetChild(4).GetChild(0).localRotation);

        wrist_target.transform.GetChild(4).GetChild(0).GetChild(0).localPosition = wrist_source.transform.GetChild(4).GetChild(0).GetChild(0).localPosition;
        wrist_target.transform.GetChild(4).GetChild(0).GetChild(0).localRotation = wrist_source.transform.GetChild(4).GetChild(0).GetChild(0).localRotation;
        savedPoses.thumb_pos.Add(wrist_target.transform.GetChild(4).GetChild(0).GetChild(0).localPosition);
        savedPoses.thumb_rot.Add(wrist_target.transform.GetChild(4).GetChild(0).GetChild(0).localRotation);
    }

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

    private void SaveToAsset()
    {
        if (_posesCollection == null)
        {
            GenerateCollectionAsset();
        }

        _posesCollection.StoreInteractables(savedPoses);
    }

    public void GenerateCollectionAsset()
    {
        _posesCollection = CreateInstance<LeapHandGrabDataCollection>();
        string parentDir = Path.Combine("Assets", "LeapHandGrabDataCollection");
        if (!Directory.Exists(parentDir))
        {
            Directory.CreateDirectory(parentDir);
        }
        string name = _dummyHand.transform.parent != null ? _dummyHand.transform.parent.name : "Auto";
        AssetDatabase.CreateAsset(_posesCollection, Path.Combine(parentDir, $"{name}_LeapHandGrabData.asset"));
        AssetDatabase.SaveAssets();
    }

    private void LoadFromAsset()
    {
        if (_posesCollection == null)
        {
            return;
        }

        LeapHandGrabData handPose = _posesCollection.InteractablesData;

        GameObject main_target = _dummyHand;

        main_target.transform.position = handPose.hand_pos;
        main_target.transform.rotation = handPose.hand_rot;

        GameObject wrist_target = FindChild(_dummyHand, "R_Wrist");

        // index
        wrist_target.transform.GetChild(0).localPosition = handPose.index_pos[0];
        wrist_target.transform.GetChild(0).localRotation = handPose.index_rot[0];

        wrist_target.transform.GetChild(0).GetChild(0).localPosition = handPose.index_pos[1];
        wrist_target.transform.GetChild(0).GetChild(0).localRotation = handPose.index_rot[1];

        wrist_target.transform.GetChild(0).GetChild(0).GetChild(0).localPosition = handPose.index_pos[2];
        wrist_target.transform.GetChild(0).GetChild(0).GetChild(0).localRotation = handPose.index_rot[2];

        // middle
        wrist_target.transform.GetChild(1).localPosition = handPose.middle_pos[0];
        wrist_target.transform.GetChild(1).localRotation = handPose.middle_rot[0];
       
        wrist_target.transform.GetChild(1).GetChild(0).localPosition = handPose.middle_pos[1];
        wrist_target.transform.GetChild(1).GetChild(0).localRotation = handPose.middle_rot[1];
        
        wrist_target.transform.GetChild(1).GetChild(0).GetChild(0).localPosition = handPose.middle_pos[2];
        wrist_target.transform.GetChild(1).GetChild(0).GetChild(0).localRotation = handPose.middle_rot[2];
        
        // pinky
        wrist_target.transform.GetChild(2).localPosition = handPose.pinky_pos[0];
        wrist_target.transform.GetChild(2).localRotation = handPose.pinky_rot[0];
       
        wrist_target.transform.GetChild(2).GetChild(0).localPosition = handPose.pinky_pos[1];
        wrist_target.transform.GetChild(2).GetChild(0).localRotation = handPose.pinky_rot[1];

        wrist_target.transform.GetChild(2).GetChild(0).GetChild(0).localPosition = handPose.pinky_pos[2];
        wrist_target.transform.GetChild(2).GetChild(0).GetChild(0).localRotation = handPose.pinky_rot[2];

        // ring
        wrist_target.transform.GetChild(3).localPosition = handPose.ring_pos[0];
        wrist_target.transform.GetChild(3).localRotation = handPose.ring_rot[0];

        wrist_target.transform.GetChild(3).GetChild(0).localPosition = handPose.ring_pos[1];
        wrist_target.transform.GetChild(3).GetChild(0).localRotation = handPose.ring_rot[1];

        wrist_target.transform.GetChild(3).GetChild(0).GetChild(0).localPosition = handPose.ring_pos[2];
        wrist_target.transform.GetChild(3).GetChild(0).GetChild(0).localRotation = handPose.ring_rot[2];

        // thumb
        wrist_target.transform.GetChild(4).localPosition = handPose.thumb_pos[0];
        wrist_target.transform.GetChild(4).localRotation = handPose.thumb_rot[0];

        wrist_target.transform.GetChild(4).GetChild(0).localPosition = handPose.thumb_pos[1];
        wrist_target.transform.GetChild(4).GetChild(0).localRotation = handPose.thumb_rot[1];

        wrist_target.transform.GetChild(4).GetChild(0).GetChild(0).localPosition = handPose.thumb_pos[2];
        wrist_target.transform.GetChild(4).GetChild(0).GetChild(0).localRotation = handPose.thumb_rot[2];
    }
}
