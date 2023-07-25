/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            reset_scene();
        }
    }

    public void reset_scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }    

    public void ResetPosition()
    {
        transform.localPosition = new Vector3(0, 0, -0.038f);
    }
}
