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

    Color left1 = new Color(0,1,1,1);
    Color left2 = new Color(1, 1, 1, 0);
    Color right1 = new Color(1,0,1,1);
    Color right2 = new Color(1, 1, 1, 0);



    public OSC osc;


    void Awake()
    {
        thisCamera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        //no mouse input
        if ((Input.GetMouseButton(0) == false && (Input.GetMouseButton(1) == false)))
        {
            OscMessage message3 = new OscMessage();
            message3.address = "/Playing";
            message3.values.Add(0);
            osc.Send(message3);
        }
        else
        {
            OscMessage message3 = new OscMessage();
            message3.address = "/Playing";
            message3.values.Add(1);
            osc.Send(message3);
        }

        //left click
        if (Input.GetMouseButtonDown(0))
        {
            OscMessage message2 = new OscMessage();
            message2.address = "/Button";
            message2.values.Add(0);
            osc.Send(message2);

            CreateLine(0);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 tempFingerPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, thisCamera.nearClipPlane * 100);
            mouseWorld = thisCamera.ScreenToWorldPoint(tempFingerPos);


            if (Vector3.Distance(mouseWorld, fingerPositions[fingerPositions.Count - 1]) > .1f)
            {
                UpdateLine(mouseWorld);
            }

            OscMessage message1 = new OscMessage();
            message1.address = "/mouseY";
            message1.values.Add((int)((mouseWorld.y+17)/2.43));
            osc.Send(message1);



        }

        //right click
        if (Input.GetMouseButtonDown(1))
        {
            OscMessage message2 = new OscMessage();
            message2.address = "/Button";
            message2.values.Add(1);
            osc.Send(message2);


            CreateLine(1);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 tempFingerPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, thisCamera.nearClipPlane * 100);
            mouseWorld = thisCamera.ScreenToWorldPoint(tempFingerPos);


            if (Vector3.Distance(mouseWorld, fingerPositions[fingerPositions.Count - 1]) > .1f)
            {
                UpdateLine(mouseWorld);
            }

            OscMessage message1 = new OscMessage();
            message1.address = "/mouseY";
            message1.values.Add((int)((mouseWorld.y + 17) / 2.43));
            osc.Send(message1);
        }

    }




    void CreateLine(int mouseButton)
    {
        currentLine = new GameObject("line");

        lineRenderer = currentLine.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        if (Input.GetMouseButtonDown(0))
        {
            lineRenderer.SetColors(left1, left2);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            lineRenderer.SetColors(right1, right2);
        }


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