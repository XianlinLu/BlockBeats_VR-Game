using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.iOS;

public class SceneHandler : MonoBehaviour
{
    public GameObject canvasObject;
    private string[] sceneArray = new string[] {"CloudScene", "RainScene", "ThunderScene", "SunScene", "MoonScene"};
 
    private bool canvasOff = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && canvasOff) {
            canvasObject.SetActive(true);
            canvasOff = false;
        }
    }

    public void SwitchScene() {
        Scene scene = SceneManager.GetActiveScene();
        int currIndex = System.Array.IndexOf(sceneArray, scene.name);
        if (currIndex == 4) {
            currIndex = -1;
        }
        currIndex += 1;
        SceneManager.LoadScene(sceneArray[currIndex]);
    }

    public void HideCanvas() {
        canvasObject.SetActive(false);
        StartCoroutine(HideCanvasBufferTime());
    }

    IEnumerator HideCanvasBufferTime() {
        yield return new WaitForSeconds(2);
        canvasOff = true;

    }
}
