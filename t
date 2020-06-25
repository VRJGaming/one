using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


    public class pointerDown : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public CarController1 CarCon;
        public Audiomanager Audio;
        bool Forwardtemp,BackwardTemp;
        public float startTime, duration,gravityDuration,pitch;
       public Light[] breakLight;

    void Update()
    {
        if(CarCon.Vertical == 1)
        {
            float t = (Time.time - startTime) / duration;
            pitch = Mathf.SmoothStep(1, 1.5f, t);
            Audio.Audio.pitch = pitch;
        }else if (CarCon.Vertical == 0&& Forwardtemp)
        {
            float t = (Time.time - startTime) / gravityDuration;
            pitch = Mathf.SmoothStep(pitch, 1, t);
            Audio.Audio.pitch = pitch;
            if (pitch == 1)
            {
                Forwardtemp = false;
            }
        }
        if (CarCon.Vertical == -1)
        {
            float t = (Time.time - startTime) / duration;
            pitch = Mathf.SmoothStep(1, 1.5f, t);
            Audio.Audio.pitch = pitch;
        }
        else if (CarCon.Vertical == 0 && BackwardTemp)
        {
            float t = (Time.time - startTime) / gravityDuration;
            pitch = Mathf.SmoothStep(pitch, 1, t);
            Audio.Audio.pitch = pitch;
            if (pitch == 1)
            {
                BackwardTemp = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
        {
            if (gameObject.name == "Accelerator Forward") 
            {
                CarCon.Vertical = 1;
                startTime = Time.time;
                Forwardtemp = true;
            }
            else if (gameObject.name == "Accelerator  BackWard") 
            {
              foreach (var item in breakLight)
              {
                item.enabled = true;
              }
            CarCon.Vertical = -1;
            startTime = Time.time;
            BackwardTemp = true;
        }
        //else if (gameObject.name == "Left")
        //{
        //    CarCon.Horizontal = -1;
        //}
        //else if (gameObject.name == "Right")
        //{
        //    CarCon.Horizontal = 1;
        //}
        else if (gameObject.name == "Break")
        {
            CarCon.GetComponent<Rigidbody>().drag = 2;
            foreach (var item in breakLight)
            {
                item.enabled = true;
            }

        }
       // Debug.Log("PressedDown");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
       // Debug.Log("PressedUp");
        if (gameObject.name == "Accelerator Forward")
        {
            startTime = Time.time;
            CarCon.Vertical = 0;
        }

        else if (gameObject.name == "Accelerator  BackWard")
        {
            foreach (var item in breakLight)
            {
                item.enabled = false;
            }
            startTime = Time.time;
            CarCon.Vertical = 0;
        }
        //else if (gameObject.name == "Left")
        //{
        //    CarCon.Horizontal = 0;
        //}
        //else if (gameObject.name == "Right")
        //{
        //    CarCon.Horizontal = 0;
        //}
        else if (gameObject.name == "Break")
        {
            CarCon.GetComponent<Rigidbody>().drag = 0;
            foreach (var item in breakLight)
            {
                item.enabled = false;
            }

        }
    }

   
}

