using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMouse : MonoBehaviour
{
    private GameObject spawNew;
    public GameObject ObjectToPlace;



     void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
             Debug.Log("Pressed primary button.");
            spawNew = Instantiate(ObjectToPlace, transform.position, transform.rotation); 
            spawNew.gameObject.tag="DUCK";
        }
           

    }

      public void ClearObjects()
    {
       

        foreach(GameObject respawn in GameObject.FindGameObjectsWithTag("DUCK"))
        {
            if(respawn.name == "DUCK(Clone)")
            {
                Destroy(respawn);
            }
        }
            }

 
}