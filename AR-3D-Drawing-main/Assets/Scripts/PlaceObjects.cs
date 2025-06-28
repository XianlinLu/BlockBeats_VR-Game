using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]

public class PlaceObjects : MonoBehaviour
{
    private GameObject spawNew;

    [SerializeField]
    public GameObject[] objectArray;

    private GameObject ObjectToPlace;

    public Camera arCamera;

    private bool sliderChanging;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
         ObjectToPlace = objectArray[0];
    }

    public void sliderIsChanging()
    {
        sliderChanging = true;
    }

    public void sliderNotChanging()
    {
        sliderChanging = false;
    }

    bool TryGetTouchPosision(out Vector2 touchPosition)
    {
        if (Input.touchCount>0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;

        return false;
    }

    

    void Update()
    {
        if(!TryGetTouchPosision(out Vector2 touchPosition))
            return;

        if (!sliderChanging)
        {
            Vector3 TouchPosition = arCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0.3f));
            spawNew = Instantiate(ObjectToPlace, TouchPosition, Random.rotation);
        }
        

       
    }

    public void FoamMaterial()
    {
        ObjectToPlace = objectArray[0];
    }

    public void WaterMaterial()
    {
        ObjectToPlace = objectArray[1];
    }

    public void MetalMaterial()
    {
        ObjectToPlace = objectArray[2];
    }

    public void ClearObjects()
    {
        Destroy(spawNew);
    }
}
