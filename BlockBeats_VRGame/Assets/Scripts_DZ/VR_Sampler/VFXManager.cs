using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXManager : MonoBehaviour
{
    public VisualEffect sparks;
    private float oriX;
    private float oriY;
    private float oriZ;

    // Start is called before the first frame update
    void Start()
    {
        sparks.gameObject.SetActive(false);
        oriX = sparks.transform.position.x;
        oriY = sparks.transform.position.y;
        oriZ = sparks.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createSpark()
    {
        GameObject newSpark = GameObject.Instantiate(sparks.gameObject);
        newSpark.transform.position = new Vector3(Random.Range(oriX-1f, oriX+1f), Random.Range(oriY-1f, oriY+1f), Random.Range(oriZ-1f, oriZ+1f));
        //float randScale = Random.Range(0.1f, 1f);
        newSpark.transform.localScale = new Vector3(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f));
        newSpark.gameObject.SetActive(true);
        StartCoroutine(DestroyVFXObj(newSpark));
    }

    IEnumerator DestroyVFXObj(GameObject toDestroy)
    {
        yield return new WaitForSeconds(3);
        Destroy(toDestroy);
    }
}
