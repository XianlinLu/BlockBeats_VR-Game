using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class paintable : MonoBehaviour
{
    public GameObject Brush;
    public float BrushSize;
    public RenderTexture RTexture;

    public GameObject Brush_2;
    Renderer m_Renderer;


    private bool sliderChanging;

    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = Brush_2.gameObject.GetComponent<Renderer> ();
        m_Renderer.sharedMaterial.mainTexture = Resources.Load("blank") as Texture;
        BrushSize = 0.1f;
        sliderChanging = false;
    }

    public void ChangeSize (float newSize)
    {
        BrushSize = newSize;
    }

    public void sliderIsChanging()
    {
        sliderChanging = true;
    }

    public void sliderNotChanging()
    {
        sliderChanging = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && !sliderChanging)
        { 
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(Ray, out hit))
            {
                var go = Instantiate(Brush, hit.point , transform.rotation, transform);
                go.transform.localScale = new Vector3(1, 1, 0.5f) * BrushSize;
            }
        }

    }

    public void ClearDraw()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            //Do something with child
            Destroy(child);
        }
        
    }

    public void Save()
    {
        // StartCoroutine(CoSave());

        RenderTexture.active = RTexture;

        var texture2D = new Texture2D(RTexture.width, RTexture.height);
        texture2D.ReadPixels(new Rect (0,0,RTexture.width, RTexture.height),0,0);
        texture2D.Apply();

        //update the texture
        m_Renderer.sharedMaterial.mainTexture = texture2D;

    }

    private IEnumerator CoSave()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log(Application.dataPath + "/Resources/2DTexture.png");

        RenderTexture.active = RTexture;

        var texture2D = new Texture2D(RTexture.width, RTexture.height);
        texture2D.ReadPixels(new Rect (0,0,RTexture.width, RTexture.height),0,0);
        texture2D.Apply();

        m_Renderer.sharedMaterial.mainTexture = texture2D;


        // var data = texture2D.EncodeToPNG();

        // File.WriteAllBytes(Application.dataPath + "/Resources/2DTexture.png", data);
    }
}
