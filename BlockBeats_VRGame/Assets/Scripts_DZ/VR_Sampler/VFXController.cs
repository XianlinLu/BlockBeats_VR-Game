using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXController : MonoBehaviour
{

    private bool isPoked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExamplePoke()
    {
        transform.GetComponent<VisualEffect>().SetBool("example_button", !isPoked);

        isPoked = !isPoked;
    }
}
