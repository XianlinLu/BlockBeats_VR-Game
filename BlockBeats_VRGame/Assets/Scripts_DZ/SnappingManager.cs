using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingManager : MonoBehaviour
{
    public GameObject fakeSnapper;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
        Transform tf = other.gameObject.transform;
        //tf.parent = this.transform.parent;
        //tf.localPosition = new Vector3(4f, 0f, 0f);
        if (tf.tag == "element")
        {
            print("HAHAHA");
            //tf.gameObject.SetActive(false);
            //fakeSnapper.SetActive(true);
            tf.parent = this.transform;
            tf.localPosition = new Vector3(4f, 0f, 0f);
            tf.GetComponent<Rigidbody>().isKinematic = true;
        }





    }
}
