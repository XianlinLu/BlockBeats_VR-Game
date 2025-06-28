using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ARLine : MonoBehaviour
{
    private int positionCount = 0;

    private Vector3 prevPointDistance = Vector3.zero;
    
    private LineRenderer LineRenderer { get; set; }

    private LineSettings settings;

    private float _dddistance = 0.001f;

    public Slider _slider;

    

    public ARLine(LineSettings settings)
    {
        this.settings = settings;
    }

    public void ChangeDistance (float newDistance)
    {
        _dddistance = newDistance;
    }

    public void AddPoint(Vector3 position)
    {
        if(prevPointDistance == null)
            prevPointDistance = position;

        if(prevPointDistance != null && Mathf.Abs(Vector3.Distance(prevPointDistance, position)) >= settings.minDistanceBeforeNewPoint)
        {
            prevPointDistance = position;
            positionCount++;

            LineRenderer.positionCount = positionCount;

            // index 0 positionCount must be - 1
            LineRenderer.SetPosition(positionCount - 1, position);

            //Shake the line----------------------------------------------------------------
            //Get old points
            Vector3[] LinePos = new Vector3[LineRenderer.positionCount];
            LineRenderer.GetPositions(LinePos);
            //Store new points position 
            Vector3[] points = new Vector3[LineRenderer.positionCount];
            
            
            // _slider.onValueChanged.AddListener((_value) => {
            //     _dddistance = _value;
            // });

            for (int i = 0; i < LineRenderer.positionCount; i++)
            {
                LinePos[i].x += Random.Range(-_dddistance, _dddistance);
                LinePos[i].y += Random.Range(-_dddistance, _dddistance);
                LinePos[i].z += Random.Range(-_dddistance, _dddistance);

                points[i] = LinePos[i];
            }

       

            Debug.Log(_dddistance);
            LineRenderer.SetPositions(points);

            // applies simplification if reminder is 0
            if(LineRenderer.positionCount % settings.applySimplifyAfterPoints == 0 && settings.allowSimplification)
            {
                LineRenderer.Simplify(settings.tolerance);
            }
        }   
    }

    void Update ()
    {
        Debug.Log("hi");
    }

    public void AddNewLineRenderer(Transform parent, ARAnchor anchor, Vector3 position)
    {
        positionCount = 2;
        GameObject go = new GameObject($"LineRenderer");
        
        go.transform.parent = anchor?.transform ?? parent;
        go.transform.position = position;
        go.tag = settings.lineTagName;
        
        LineRenderer goLineRenderer = go.AddComponent<LineRenderer>();
        goLineRenderer.startWidth = settings.startWidth;
        goLineRenderer.endWidth = settings.endWidth;

        goLineRenderer.startColor = settings.startColor;
        goLineRenderer.endColor = settings.endColor;

        goLineRenderer.material = settings.defaultMaterial;
        goLineRenderer.useWorldSpace = true;
        goLineRenderer.positionCount = positionCount;

        goLineRenderer.numCornerVertices = settings.cornerVertices;
        goLineRenderer.numCapVertices = settings.endCapVertices;

        goLineRenderer.SetPosition(0, position);
        goLineRenderer.SetPosition(1, position);

        LineRenderer = goLineRenderer;

        ARDebugManager.Instance.LogInfo($"New line renderer created");
    } 
}