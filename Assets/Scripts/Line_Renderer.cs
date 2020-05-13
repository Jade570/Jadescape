using System.Collections.Generic;
using UnityEngine;

public class Line_Renderer : MonoBehaviour
{
    List<Vector3> linePoints = new List<Vector3>();

    private GameObject currentLine;
    private Vector3 mouseWorld;

    public float startWidth = 1.0f;
    public float endWidth = 1.0f;
    public float threshold = 0.001f;

    public List<Vector3> fingerPositions;

    Camera thisCamera;
    LineRenderer lineRenderer;

    public OSC osc;


    void Awake()
    {
        thisCamera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 tempFingerPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, thisCamera.nearClipPlane * 100);
            mouseWorld = thisCamera.ScreenToWorldPoint(tempFingerPos);


            if (Vector3.Distance(mouseWorld, fingerPositions[fingerPositions.Count - 1]) > .1f)
            {
                UpdateLine(mouseWorld);
            }

            OscMessage message = new OscMessage();
            message.address = "/mouseY";
            message.values.Add(mouseWorld.y);
            osc.Send(message);
        }

    }




    void CreateLine()
    {
        currentLine = new GameObject("line");

        lineRenderer = currentLine.AddComponent<LineRenderer>();

        fingerPositions.Clear();
            Vector3 tempPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, thisCamera.nearClipPlane * 100);

        fingerPositions.Add((thisCamera.ScreenToWorldPoint(tempPos)));
        fingerPositions.Add((thisCamera.ScreenToWorldPoint(tempPos)));

        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
    }


    void UpdateLine(Vector3 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
    }


}