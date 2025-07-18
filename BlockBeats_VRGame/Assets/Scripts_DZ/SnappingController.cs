using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SnappingController : MonoBehaviour
{
    // Role of Block
    public enum Role
    {
        BuiltBlock,
        SnappingBlock
    }
    public Role currentRole;

    public bool[] interfaceArray = new bool[6];
    public string[] snappedObjNameArray = new string[6];
    //public Dictionary<bool, string> interfaceObjects = new Dictionary<bool, string>();

    public int currentBuiltBlockInterfaceIndex;
    public int currentSnapBlockInterfaceIndex;

    // Snapping Preview Object
    private GameObject previewObj;

    // Storing previous orientation to check for audio effect change
    private Vector3 prevOrientation;

    // Start is called before the first frame update
    void Start()
    {
        // Intialize interface array - all faces are available to snap in the beginning
        //for (int i = 0; i < interfaceArray.Length; i++)
        //{
        //  interfaceArray[i] = false;
        //}

        prevOrientation = new Vector3(0f, 0f, 0f);

    }

    // Update is called once per frame
    void Update()
    {

    }

    //////// BUILT BLOCK RELATED //////////
    private void OnTriggerEnter(Collider other)
    {
        if (currentRole == Role.BuiltBlock)
        {
            Transform otherTF = other.gameObject.transform;

            if (otherTF.tag == "element" && !previewObj && otherTF.GetComponent<BlockManager>().InPreview == false)
            {
                //duplicate to show preview, otherwise hand-tracking overwrites object position
                previewObj = Instantiate(otherTF.gameObject);
                previewObj.tag = "preview";
                Destroy(previewObj.transform.GetChild(0).gameObject);

                // Set other (snapping) block property to being in preview
                otherTF.GetComponent<BlockManager>().InPreview = true;

            }
        }



    }

    private void OnTriggerExit(Collider other)
    {
        if (currentRole == Role.BuiltBlock)
        {
            if (previewObj)
            {
                if (other.transform.GetComponent<BlockManager>())
                {
                    // Set other (snapping) block property to being out of preview
                    other.transform.GetComponent<BlockManager>().InPreview = false;
                }
                

                Destroy(previewObj.gameObject);

            } else
            {
                if (other.transform.GetComponent<BlockManager>())
                {
                    // Set other (snapping) block property to being out of preview
                    other.transform.GetComponent<BlockManager>().InPreview = false;
                }
            }

            if (other.tag == "snapped")
            {
                other.gameObject.tag = "element";

                // Set other (snapping) block property to being out of preview
                other.transform.GetComponent<BlockManager>().InPreview = false;

                // Change other Block Role to SnappingRole (Block becomes available to snap again)
                // Access SnappingController in child
                if (other.transform.GetChild(0))
                {
                    other.transform.GetChild(0).GetComponent<SnappingController>().currentRole = Role.SnappingBlock;
                }

                // Update face availability - interface is now available
                //int interfaceIndex = System.Array.IndexOf(snappedObjNameArray, other.gameObject.name);
                int interfaceIndex = -1;

                for (int i = 0; i < snappedObjNameArray.Length; i++)
                {

                    if (snappedObjNameArray[i].Trim().Equals(other.gameObject.name))
                    {
                        interfaceIndex = i;
                    }
                }

                
                print("INTERFACE INDEX:" + interfaceIndex);
                interfaceArray[interfaceIndex] = false;
                snappedObjNameArray[interfaceIndex] = null;

                
            }

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (currentRole == Role.BuiltBlock)
        {
            Transform otherTF = other.gameObject.transform;
            if (otherTF.tag == "element")
            {
                // correct the other's orientation
                // rounding xyz rotation to closest 0, 90 or 180 degrees
                Vector3 rotationAngles = otherTF.eulerAngles;
                rotationAngles.x = Mathf.Round(rotationAngles.x / 90) * 90;
                rotationAngles.y = Mathf.Round(rotationAngles.y / 90) * 90;
                rotationAngles.z = Mathf.Round(rotationAngles.z / 90) * 90;
                //print(rotationAngles);
                otherTF.eulerAngles = rotationAngles;

                Vector3 TFPos = otherTF.position;
                Vector3 basePos = transform.parent.position;

                if (previewObj)
                {
                    previewObj.transform.eulerAngles = rotationAngles;

                    // add position snapping
                    // six snapping points
                    Vector3[] snapPtArray = new Vector3[6];
                    snapPtArray[0] = new Vector3(basePos.x - 0.2f, basePos.y, basePos.z); // A
                    snapPtArray[1] = new Vector3(basePos.x + 0.2f, basePos.y, basePos.z); // A
                    snapPtArray[2] = new Vector3(basePos.x, basePos.y + 0.2f, basePos.z); // B
                    snapPtArray[3] = new Vector3(basePos.x, basePos.y - 0.2f, basePos.z); // B
                    snapPtArray[4] = new Vector3(basePos.x, basePos.y, basePos.z - 0.2f); // C
                    snapPtArray[5] = new Vector3(basePos.x, basePos.y, basePos.z + 0.2f); // C


                    // calculate distances to snapping points
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

                    // Check face availability - good to snap if face is available
                    if (interfaceArray[correctSnapPtIndex] == false)
                    {
                        // NOTE: preview gameoject and actual object both exist
                        //previewObj.transform.position = snapPtArray[correctSnapPtIndex];
                        previewObj.transform.DOMove(snapPtArray[correctSnapPtIndex], 0.1f).SetEase(Ease.OutSine);
                        otherTF.position = snapPtArray[correctSnapPtIndex];

                        // TEMP ----- Switch audio effects ------
                        currentBuiltBlockInterfaceIndex = correctSnapPtIndex;

                        if (!prevOrientation.Equals(rotationAngles))
                        {
                            if (prevOrientation.x != rotationAngles.x && otherTF.GetComponent<AudioBehavior>())
                            {
                                // switch to FX track 1
                                otherTF.GetComponent<AudioBehavior>().PlayBase();
                            }
                            if (prevOrientation.y != rotationAngles.y && otherTF.GetComponent<AudioBehavior>())
                            {
                                // switch to FX track 1
                                otherTF.GetComponent<AudioBehavior>().PlayFX1();
                            }
                            if (prevOrientation.z != rotationAngles.z && otherTF.GetComponent<AudioBehavior>())
                            {
                                // switch to FX track 1
                                otherTF.GetComponent<AudioBehavior>().PlayFX2();
                            }

                            prevOrientation = rotationAngles;
                            print(rotationAngles);
                        }


                        // TEMP ----- Switch audio effects ------

                        // If hand stops grabbing, destroy preview to confirm snapping
                        if (otherTF.GetComponent<BlockManager>().IsGrabbing == false)
                        {
                            otherTF.gameObject.tag = "snapped";

                            // Change other Block Role to BuiltRole
                            // Access SnappingController in child
                            if (otherTF.GetChild(0))
                            {
                                otherTF.GetChild(0).GetComponent<SnappingController>().currentRole = Role.BuiltBlock;
                            }

                            Destroy(previewObj.gameObject);

                            // Make face not available for snapping
                            interfaceArray[correctSnapPtIndex] = true;
                            snappedObjNameArray[correctSnapPtIndex] = other.gameObject.name;
                        }
                    } else
                    {
                        if (previewObj)
                        {
                            Destroy(previewObj.gameObject);
                        }
                    }

                    
                    
                }
            }
        }

        if (currentRole == Role.SnappingBlock)
        {
            Transform otherTF = other.gameObject.transform;
            if (otherTF.tag == "snapped")
            {
                // Calculating six snapping positions to keep track of colliding faces does not
                // work because this does not account for changing orientation
                // In the future, may need to use child colliders to rewrite the snapping logic
            }
        }


    }





    
}
