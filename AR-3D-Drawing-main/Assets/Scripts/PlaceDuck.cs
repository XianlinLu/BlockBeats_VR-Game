using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]

public class PlaceDuck : MonoBehaviour
{
    private GameObject spawNew;


    public GameObject ObjectToPlace;

    public Camera arCamera;

    private bool buttonClick;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public GameObject respawnPrefab;
    public GameObject[] respawns;

    void Start()
    {

         respawns = GameObject.FindGameObjectsWithTag("DUCK");
    }

    public void buttonDown()
    {
        buttonClick = true;
    }

    public void buttonUp()
    {
        buttonClick = false;
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

        if (!buttonClick)
        {
            Vector3 TouchPosition = arCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0.3f));
            spawNew = Instantiate(ObjectToPlace, TouchPosition, transform.rotation);
        }
        
        

        
    }


    void OnMouseDown()
    {
        Vector3 mousePosition = arCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.3f));
        
      
            Debug.Log("Click");
            spawNew = Instantiate(ObjectToPlace, mousePosition, transform.rotation);
        
    }


    public void ClearObjects()
    {
        foreach (GameObject respawn in respawns)
        {
            Destroy(respawn);
        }

        Destroy( GameObject.Find("DUCK(Clone)"));


        foreach(GameObject respawn in GameObject.FindGameObjectsWithTag("DUCK"))
        {
            if(respawn.name == "DUCK(Clone)")
            {
                Destroy(respawn);
            }
        }
            }
}
