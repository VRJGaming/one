using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    public class rayCast : MonoBehaviour
    {
    //============================================
    public ParticleSystem extingusherParticle;
    public Animator anim;
    public AudioClip VibeClip, VibeClipTemp;
    public GameObject lever;
    GameObject pin, extig;
    bool garbbed, exGrabbed, pressed;
    public float rVal;
   // public  AudioSource myAudio;
    [Space (15)]


    //==============================================
    public Transform LastObject;

        private float distance;
        public GameObject parentobj;
        public GameObject Ori_Parent, Controller;
        public GameObject pinobj;
       



    RaycastHit hit;
        public float rayDist;
        [SerializeField] Transform Dot;
      

        // Start is called before the first frame update
        void Start()
        {
            //lineRen.transform.parent=lineRen.transform;
        }

    // Update is called once per frame

    //private void OnTriggerStay(Collider other)
    //{

        
    //    if (rVal > 0.5f)
    //    {
    //        if (other.tag == "pin")
    //        {
    //            // Debug.Log("pin grabbed");
    //            other.transform.parent = transform;
    //            garbbed = true;
    //            pin = other.gameObject;
    //            // OVRHaptics.Channels[1].Mix(new OVRHapticsClip(VibeClip));

    //        }

    //        if (other.tag == "lever")
    //        {
    //            pressed = true;
    //            pushLever();
    //            // OVRHaptics.Channels[1].Mix(new OVRHapticsClip(VibeClip) );

    //        }

    //    }
    //}
    void Update()
        {
        if (pin)
        {
            pin.transform.parent = null;
            //  OVRHaptics.Channels[1].Clear();

            Rigidbody rigi = pin.AddComponent<Rigidbody>();
            rigi.isKinematic = false;
            rigi.useGravity = true;
            lever.GetComponent<Collider>().enabled = true;

        }

        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDist))
            {
                Dot.position = hit.point;
            if (hit.transform.gameObject.GetComponent<Button>() != null)
            {
                Dot.gameObject.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_Color", Color.blue);

                //  Debug.LogError("Works");
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.P))
                {
                    // Debug.Log("p Button prssed");
                    hit.transform.gameObject.GetComponent<Button>().onClick.Invoke();
                }

            }
            else
            {
                Dot.gameObject.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_Color", Color.white);
            }

            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.P))
                {
                if(hit.collider.gameObject.tag=="lever")
                {
                    pushLever();
                }
                    if (hit.collider.gameObject.tag == "water")
                    {
                        //Debug.LogError(hit.transform.gameObject.name);
                        Debug.Log("p prssed");
                        if (LastObject == null)
                    {
                        try
                        {
                            Ori_Parent = hit.transform.parent.gameObject;
                        }
                        catch (Exception e)
                        {

                        }

                    }
                        LastObject = hit.transform;
                        hit.transform.parent = Controller.transform;
                        //if (hit.transform.GetComponent<Rigidbody>() != null)
                        //{
                        //    hit.transform.GetComponent<Rigidbody>().useGravity = false;
                        //    hit.transform.GetComponent<Rigidbody>().isKinematic = true;
                        //}
                    }
                    if (hit.collider.gameObject.tag == "pin")
                    {
                        if (LastObject == null)
                    {
                        try { 
                            Ori_Parent = hit.transform.parent.gameObject;
                            }
                        catch (Exception e)
                            {

                            }
                }
                        LastObject = hit.transform;
                        hit.transform.parent = Controller.transform;

                    }


                }


                if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyUp(KeyCode.P))
                {
                 
                    if (LastObject != null)
                    {
                    if (Ori_Parent!=null)
                    {
                        LastObject.parent = Ori_Parent.transform;
                    }
                    else
                    {
                        LastObject.parent = null;
                    }
                        Debug.Log("o pressed");
                        //hit.transform.GetComponent<Rigidbody>().useGravity = true;
                        //hit.transform.GetComponent<Rigidbody>().isKinematic = false;
                        LastObject = null;
                 }
                    if (hit.collider.gameObject.tag == "pin")
                    {
                    pinobj.AddComponent<Rigidbody>();
                    }

                releseLever();


                }
            }


        //=====================================
        if (garbbed && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.P))
        {
            garbbed = false;
            //  Debug.Log("pin relesed");
            if (pin)
            {
                pin.transform.parent = null;
                //  OVRHaptics.Channels[1].Clear();

                Rigidbody rigi = pin.AddComponent<Rigidbody>();
                rigi.isKinematic = false;
                rigi.useGravity = true;
                lever.GetComponent<Collider>().enabled = true;

            }

        }



    }

    //================================
    public void pushLever()
    {

        anim.SetBool("push", true);
        var emmi = extingusherParticle.emission;
        emmi.enabled = true;
        if (!anim.GetComponent<AudioSource>().isPlaying) anim.GetComponent<AudioSource>().Play();


    }
   
    public void releseLever()
    {

        anim.SetBool("push", false);
        var emmi = extingusherParticle.emission;
        emmi.enabled = false;
        anim.GetComponent<AudioSource>().Stop();


    }

}

