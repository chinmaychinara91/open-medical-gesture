/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class Thrower : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();


    public GameObject theBall;
    private Vector3 origPos;
    private Quaternion origRot;
    //bool throw_state = false;

    private void Start()
    {
        actions.Add("release", BallRelease);
        actions.Add("reset", reset_scene);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    // Start is called before the first frame update
    void Awake()
    {
        origPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        origRot.x = transform.localRotation.x;
        origRot.y = transform.localRotation.y;
        origRot.z = transform.localRotation.z;
        origRot.w = transform.localRotation.w;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            //transform.GetComponent<Animation>().Play();
            BallRelease();
        }

        if(Input.GetKeyDown("r"))
        {
            //gameObject.transform.position = origPos;
            //gameObject.transform.rotation = origRot;
            reset_scene();
        }
    }

    public void BallRelease()
    {
        transform.GetComponent<Animation>().Stop();
        transform.position = origPos;
        transform.rotation = origRot;
        theBall.transform.GetComponent<BallScript>().reset_ball();
        transform.GetComponent<Animation>().Play();
    }

    public void BallHold()
    {
        transform.GetComponent<Animation>().Stop();
        transform.position = origPos;
        transform.rotation = origRot;
        theBall.transform.GetComponent<BallScript>().reset_ball();
    }
    public void ThrowBall()
    {
        print("Throwing !");
        var ballScript = theBall.GetComponent<BallScript>();
        ballScript.ReleaseMe();
    }

    public void reset_thrower()
    {
        gameObject.transform.position = origPos;
        gameObject.transform.rotation = origRot;
        theBall.transform.GetComponent<BallScript>().reset_ball_temp();
    }

    public void reset_scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
