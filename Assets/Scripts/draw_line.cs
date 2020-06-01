using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draw_line : MonoBehaviour
{
    private GameObject currentLine;
    private LineRenderer lineRenderer;
 
    public List<Vector2> fingerPositions;
    public OSC osc;

    void Start()
    {
        Debug.Log(Screen.width + "    " +Screen.height);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Input.mousePosition;
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f)
            {
                UpdateLine(tempFingerPos);
            }

            OscMessage message = new OscMessage();
            message.address = "/mouseY";
            message.values.Add(Input.mousePosition.y);
            osc.Send(message);
        }

    }


    void CreateLine()
    {
        //currentLine = Instantiate(new GameObject("line"), Vector3.forward*1000 /*Input.mousePosition*/, Quaternion.identity);
        currentLine = new GameObject("line");
        //transform.Translate(Vector3.forward*1000);
        lineRenderer = currentLine.AddComponent<LineRenderer>();

        fingerPositions.Clear();
        fingerPositions.Add((Input.mousePosition));
        fingerPositions.Add((Input.mousePosition));

        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
    }


    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
    }

}
