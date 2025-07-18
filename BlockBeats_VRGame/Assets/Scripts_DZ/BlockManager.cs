using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{

    // Grab states
    private bool grabStarted = false;
    private bool grabProcess = false;

    public bool inPreview = false;

    // Properties
    public bool IsGrabbing
    {
        get { return grabProcess; }
    }

    public bool InPreview
    {
        get { return inPreview; }
        set { inPreview = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //////// Called by Pointable Unity Event Wrapper /////////

    public void isGrabbed()
    {
        grabStarted = true;
        //print("GRAB START");
    }

    public void isReleased()
    {
        grabStarted = false;
        grabProcess = false;
        //print("GRAB END");
    }

    public void isGrabbing()
    {
        if (grabStarted)
        {
            grabProcess = true;
            //print("isGRABBBING");
        }

    }

    ///////// Called by Pointable Unity Event Wrapper END /////////
}
