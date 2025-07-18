using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplerPositionManager : MonoBehaviour
{

    public GameObject drumkit;
    public GameObject centerEyeAnchor;
    public GameObject drumsticks;

    private Vector3 centerEyePos;


    // Start is called before the first frame update
    void Start()
    {
        centerEyePos = centerEyeAnchor.transform.position;
        drumkit.transform.position = new Vector3(centerEyePos.x, centerEyePos.y + 0.8f, centerEyePos.z + 0.2f);
        drumsticks.transform.position = new Vector3(centerEyePos.x, centerEyePos.y + 0.8f, centerEyePos.z);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
