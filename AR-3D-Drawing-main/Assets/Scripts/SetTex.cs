using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTex : MonoBehaviour
{
    public GameObject Brush_2;
    Renderer m_Renderer;

    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = Brush_2.gameObject.GetComponent<Renderer> ();

    }

    // Update is called once per frame
    void Update()
    {
        m_Renderer.material.mainTexture = Resources.Load("2DTexture") as Texture;
    }
}
