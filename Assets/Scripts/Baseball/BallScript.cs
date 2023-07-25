/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Windows.Speech;
using Leap.Unity.Interaction;
using UnityEngine.SceneManagement;

public class BallScript : MonoBehaviour
{
    public GameObject parentBone_L;
    public GameObject parentBone_R;
    public GameObject modelBone;
    public GameObject thrower;
    public GameObject Dummy;
    public Rigidbody rigidBody;
    public Transform score_NPC;
    public Transform score_Me;
    private Vector3 origPos;
    private Quaternion origRot;
    bool flag = true;
    int score_count_NPC = 0;
    int score_count_Me = 0;
    InteractionBehaviour intBe;
    bool user_flag = false;
    bool collided = false;
    public AudioClip cheer;
    public AudioClip boo;
    public Camera leapCamera;
    Transform mainHand;

    // for the position of hand
    Vector3 newpos, prevpos, movement, fwd;
    public TextMesh debugText;
    bool thrown = false;

    void Awake()
    {
        origPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        origRot = new Quaternion(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
    }

    // Start is called before the first frame update
    void Start()
    {
        intBe = transform.GetComponent<InteractionBehaviour>();
        transform.parent = modelBone.transform;
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        thrower.transform.GetComponent<DrawTrajectory>().HideLine();
    }

    void Update()
    {
        
        if (!flag)
        {
            newpos = mainHand.transform.position;
            movement = (newpos - prevpos);

            if (Vector3.Dot(fwd, movement) < -0.02f)
            {
                //debugText.text = "moving forward";
                if (!thrown)
                {
                    transform.parent = null;
                    rigidBody.isKinematic = false;
                    rigidBody.useGravity = true;

                    //ballTransform.GetComponent<Rigidbody>().rotation = modelBone.transform.rotation;

                    Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(0f, 3f), UnityEngine.Random.Range(0f, 7f));
                    Vector3 dir = (Dummy.transform.position + randomOffset) - transform.position;
                    float randomFactor = UnityEngine.Random.Range(2000f, 2100f);
                    //debugText.text = intBe.ignoreContact.ToString();
                    dir = dir.normalized;
                    Vector3 forceV = dir * randomFactor;
                    
                    //Vector3 forceV = (transform.forward + new Vector3(0f,
                    //                                                  0.04f,
                    //                                                  0)) * 1700;

                    rigidBody.AddForce(forceV);
                    transform.GetComponent<DrawTrajectory>().UpdateTrajectory(forceV, rigidBody, transform.position);
                    thrown = true;
                    user_flag = true;
                    //flag = true;
                }

            }
            else
            {
                //debugText.text = "moving  backwards";
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!flag)
        {
            prevpos = mainHand.transform.position;
            fwd = mainHand.transform.forward;
        }

        if (Input.GetKeyDown("r"))
        {
            rigidBody.isKinematic = true;
            rigidBody.useGravity = false;
            transform.parent = modelBone.transform;
            gameObject.transform.position = origPos;
            gameObject.transform.rotation = origRot;
            thrower.transform.GetComponent<DrawTrajectory>().HideLine();
        }

        if (user_flag == true) // npc catches
        {
            if (Vector3.Distance(transform.position, thrower.transform.position) <= 2.5f && !collided)
            {
                thrower.GetComponent<Thrower>().BallHold();
                user_flag = false;
                score_count_Me += 1;
                score_Me.GetComponent<TextMesh>().text = "Me: " + score_count_Me.ToString();
                score_NPC.GetComponent<AudioSource>().Stop();
                score_Me.GetComponent<AudioSource>().clip = cheer;
                score_Me.GetComponent<AudioSource>().Play();
                user_flag = false;
                collided = false;
            }


            if (collided) //drop catch
            {
                float distance = Vector3.Distance(transform.position, leapCamera.transform.position);
                //print(distance);
                if (distance <= 2f) // drop catch, play boo
                {
                    score_NPC.GetComponent<AudioSource>().Stop();
                    score_Me.GetComponent<AudioSource>().Stop();
                    score_Me.GetComponent<AudioSource>().clip = boo;
                    score_Me.GetComponent<AudioSource>().Play();
                }

                else // throw far from NPC (my bad throw)
                {
                    score_count_Me -= 1;
                    score_Me.GetComponent<TextMesh>().text = "Me: " + score_count_Me.ToString();

                    score_NPC.GetComponent<AudioSource>().Stop();
                    score_Me.GetComponent<AudioSource>().Stop();
                    score_Me.GetComponent<AudioSource>().clip = boo;
                    score_Me.GetComponent<AudioSource>().Play();
                }

                collided = false;
                user_flag = false;
            }
            //if (collided) // far from NPC
            //{
            //    print("here3");
            //    score_count_Me -= 1;
            //    score_Me.GetComponent<TextMesh>().text = "Me: " + score_count_Me.ToString();
            //    collided = false;
            //    user_flag = false;
            //}
        }

        if (user_flag == false & collided)
        {
            //score_count_Me -= 1;
            //score_Me.GetComponent<TextMesh>().text = "Me: " + score_count_Me.ToString();
            score_NPC.GetComponent<AudioSource>().Stop();
            score_Me.GetComponent<AudioSource>().clip = boo;
            score_Me.GetComponent<AudioSource>().Play();
            collided = false;
        }

    }
    public void ReleaseMe()
    {
        transform.parent = null;
        rigidBody.isKinematic = false;
        rigidBody.useGravity = true;
        transform.rotation = modelBone.transform.rotation;

        Vector3 forceV = (transform.forward + new Vector3(UnityEngine.Random.Range(-0.06f, 0.01f),
                                                          UnityEngine.Random.Range(0.01f, 0.04f),
                                                          0)) * 1700;
        //Vector3 forceV = (transform.forward + new Vector3(0f,
        //                                                  0.04f,
        //                                                  0)) * 1700;

        rigidBody.AddForce(forceV);
        thrower.transform.GetComponent<DrawTrajectory>().UpdateTrajectory(forceV, rigidBody, transform.position);
    }

    public void UserCatches() // I catch
    {
        if (flag)
        {
            if (intBe.closestHoveringHand.ToString().Contains("right"))
            {
                transform.parent = parentBone_R.transform;
                transform.position = parentBone_R.transform.GetChild(1).position;
            }
            else
            {
                transform.parent = parentBone_L.transform;
                transform.position = parentBone_L.transform.GetChild(1).position;
            }

            mainHand = transform.parent;
            //transform.parent = parentBone.transform;
            rigidBody.isKinematic = true;
            rigidBody.useGravity = false;
            intBe.ignoreContact = true;
            score_count_NPC += 1;
            score_NPC.GetComponent<TextMesh>().text = "NPC: " + score_count_NPC.ToString();
            score_Me.GetComponent<AudioSource>().Stop();
            score_NPC.GetComponent<AudioSource>().clip = cheer;
            score_NPC.GetComponent<AudioSource>().Play();
            flag = false;
            //Invoke("MakeBallGrabbable", 0.3f);
        } 
    }

    //public void  MakeBallGrabbable()
    //{
    //    transform.parent = null;
    //    intBe.ignoreContact = false;
    //    //transform.GetComponent<InteractionBehaviour>().ignoreGrasping = false;
    //    rigidBody.isKinematic = false;
    //    rigidBody.useGravity = true;
    //    rigidBody.angularDrag = 0.05f;
    //    rigidBody.mass = .5f;
    //    user_flag = true;
    //    //rigidBody.AddForce(transform.forward * 10);
    //}

    public void reset_ball()
    {
        flag = true;
        user_flag = false;
        thrown = false;
        collided = false;
        newpos = new Vector3();
        prevpos = new Vector3();
        movement = new Vector3();
        fwd = new Vector3();
        intBe.ignoreContact = false;
        rigidBody.isKinematic = true;
        rigidBody.useGravity = false;
        rigidBody.angularDrag = 10f;
        rigidBody.mass = 2f;
        transform.parent = modelBone.transform;
        transform.position = origPos;
        transform.rotation = origRot;
        thrower.transform.GetComponent<DrawTrajectory>().HideLine();
        transform.GetComponent<DrawTrajectory>().HideLine();
        score_Me.GetComponent<AudioSource>().Stop();
        score_NPC.GetComponent<AudioSource>().Stop();
    }

    public void reset_ball_temp()
    {
        flag = true;
        user_flag = false;
        collided = false;
        intBe.ignoreContact = false;
        rigidBody.isKinematic = true;
        rigidBody.useGravity = false;
        rigidBody.angularDrag = 10f;
        rigidBody.mass = 2f;
        transform.parent = modelBone.transform;
        transform.position = origPos;
        transform.rotation = origRot;
        
        //thrower.transform.GetComponent<DrawTrajectory>().HideLine();
        //score_Me.GetComponent<AudioSource>().Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "plane")
        {
            collided = true;
        }
    }
}
