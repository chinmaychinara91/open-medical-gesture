using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensityControl : MonoBehaviour
{
    Light lit;

    // Start is called before the first frame update
    void Start()
    {
        lit = GetComponent<Light>();
    }

    // Update is called once per frame
    public void LightIntensity(float intensity)
    {
        float val = intensity / 360f;
        lit.intensity = val;
    }
}
