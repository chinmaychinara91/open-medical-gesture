using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandToneChange : MonoBehaviour
{
    //Simple script for demonstrating different hand skin color tones and materials - pinch with thumb and index to activate
    //Put this script on your hand prefab

    [Header("Materials to iterate through:")]
    [Header("This has to be on Hand prefab")]
    [SerializeField]
    private Material[] materials = new Material[0];
    [Header("Difference between color values:")]
    [SerializeField]
    private float changeToneBy = -0.07f;
    [Header("Max amount of color differences in one material:")]
    [SerializeField]
    private int changeToneMaxStep = 3;
    private SkinnedMeshRenderer rend;
    private int counter = 0, materialIndex = 0;
    [Header("Random skin material on start:")]
    [SerializeField]
    private bool randomSkin = false;
    private static int randomMat = -1, randomStep = -1;
    [Header("Mainly for testing how different tones look:")]
    [SerializeField]
    private bool pinchToChangeMaterial = true;
    private void Start()
    {
        rend = GetComponent<SkinnedMeshRenderer>();
        if (randomSkin)
            ApplyRandomMaterial();
    }
    void Update()
    {
        if (pinchToChangeMaterial && OVRInput.GetDown(OVRInput.Button.One))
            ChangeSkin();
    }
    #region SkinChanger
    private void ChangeSkin()
    {
        if (counter == changeToneMaxStep)
        {
            ResetSkinTone(rend.material);
            ChangeSkinMaterial();
            counter = 0;
            return;
        }
        ChangeSkinTone(rend.material, changeToneBy);
        counter += 1;
    }
    private void ChangeSkinTone(Material mat, float val)
    {
        Color col = mat.color;
        Color.RGBToHSV(col, out float h, out float s, out float v);
        v += val;
        col = Color.HSVToRGB(h, s, v);
        mat.SetColor("_Color", col);
    }
    private void ResetSkinTone(Material mat)
    {
        Color col = mat.color;
        Color.RGBToHSV(col, out float h, out float s, out float v);
        v = 1;
        col = Color.HSVToRGB(h, s, v);
        mat.SetColor("_Color", col);
    }
    private void ChangeSkinMaterial()
    {
        if (materialIndex < materials.Length - 1)
            materialIndex += 1;
        else
            materialIndex = 0;
        rend.material = materials[materialIndex];
    }
    #endregion
    private void ApplyRandomMaterial()
    {
        //random mat:
        if (randomMat == -1)
            randomMat = Random.Range(0, materials.Length);
        materialIndex = randomMat;
        rend.material = materials[materialIndex];
        //random skin tone:
        if (changeToneMaxStep == 0) return;
        if (randomStep == -1)
            randomStep = Random.Range(0, changeToneMaxStep);
        ChangeSkinTone(rend.material, randomStep * changeToneBy);
        counter = randomStep;
    }
}
