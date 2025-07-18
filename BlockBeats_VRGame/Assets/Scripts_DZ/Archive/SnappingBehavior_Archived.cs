using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/*
 * This script is attached to the snapping collider game object for each combined block
 */

public class SnappingBehavior_Archived : MonoBehaviour
{
    //temp
    //public GameObject snappingPreviewCube;

    private GameObject previewObj;
    private bool isSnapped = false;
    private Rigidbody parentRB;
    private Rigidbody otherRB;
    private GameObject otherGO;
    public enum Role
    {
        BuiltBlock,
        SnappingBlock
    }
    public Role currentRole;


    // Start is called before the first frame update
    void Start()
    {
        //get parent rigidbody
        parentRB = transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Transform otherTF = other.gameObject.transform;
        otherRB = other.transform.GetComponent<Rigidbody>();
        otherGO = other.gameObject;

        if (otherTF.tag == "element" && currentRole == Role.BuiltBlock && !previewObj)
        {
            //duplicate to show preview, otherwise hand-tracking overwrites object position
            previewObj = Instantiate(otherTF.gameObject);

        }
        
 
    }

    private void OnTriggerStay(Collider other)
    {

        Transform otherTF = other.gameObject.transform;
        otherRB = other.transform.GetComponent<Rigidbody>();
        otherGO = other.gameObject;

        if (otherTF.tag == "element" && currentRole == Role.BuiltBlock)
        {
            //correct the other's orientation
            //rounding xyz rotation to closest 0, 90 or 180 degrees
            Vector3 rotationAngles = otherTF.eulerAngles;
            rotationAngles.x = Mathf.Round(rotationAngles.x / 90) * 90;
            rotationAngles.y = Mathf.Round(rotationAngles.y / 90) * 90;
            rotationAngles.z = Mathf.Round(rotationAngles.z / 90) * 90;
            print(rotationAngles);
            otherTF.eulerAngles = rotationAngles;

            Vector3 TFPos = otherTF.position;
            Vector3 basePos = transform.parent.position;

            if (previewObj)
            {
                previewObj.transform.eulerAngles = rotationAngles;

                //add position snapping
                //six snapping points
                Vector3[] snapPtArray = new Vector3[6];
                snapPtArray[0] = new Vector3(basePos.x - 0.2f, basePos.y, basePos.z);
                snapPtArray[1] = new Vector3(basePos.x + 0.2f, basePos.y, basePos.z);
                snapPtArray[2] = new Vector3(basePos.x, basePos.y + 0.2f, basePos.z);
                snapPtArray[3] = new Vector3(basePos.x, basePos.y - 0.2f, basePos.z);
                snapPtArray[4] = new Vector3(basePos.x, basePos.y, basePos.z - 0.2f);
                snapPtArray[5] = new Vector3(basePos.x, basePos.y, basePos.z + 0.2f);


                //calculate distances to snapping points
                float minDis = 100f;
                int correctSnapPtIndex = 0;
                for (int i = 0; i < snapPtArray.Length; i++)
                {
                    
                    float distance = Vector3.Distance(TFPos, snapPtArray[i]);
                    if (distance < minDis)
                    {
                        minDis = distance;
                        correctSnapPtIndex = i;
                    }
                }
                //previewObj.transform.position = snapPtArray[correctSnapPtIndex];
                previewObj.transform.DOMove(snapPtArray[correctSnapPtIndex], 0.1f).SetEase(Ease.OutSine);
                otherTF.position = snapPtArray[correctSnapPtIndex];
                AfterSnapping();



            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        Transform otherTF = other.gameObject.transform;
        otherRB = other.transform.GetComponent<Rigidbody>();
        otherGO = other.gameObject;

        if (previewObj && otherTF.tag == "snapped" && currentRole == Role.BuiltBlock)
        {
            
            ExitSnap(other);
            Destroy(previewObj.gameObject);
            
        }
    }

    private void AfterSnapping()
    {
        if (otherRB)
        {
            isSnapped = true;
            otherRB.isKinematic = true;
            otherRB.useGravity = false;

            if (otherGO)
            {
                otherGO.tag = "snapped";
            }
            
        }
        
    }

    private void ExitSnap(Collider other)
    {
        print("CHECK1");

        if (otherRB)
        {

            print("CHECK2");

            isSnapped = false;

            //does not work; my guess is when other is grabbed, it is always kinematic
            otherRB.isKinematic = false; 

            otherRB.useGravity = true;

            other.gameObject.tag = "element";
        }

        

    }

    public void Grabbing()
    {
        //currentRole = Role.SnappingBlock;
        //parentRB.isKinematic = true;
        //parentRB.useGravity = false;
    }

    public void Releasing()
    {

        parentRB.useGravity = false;
        parentRB.isKinematic = false;

    }


}
