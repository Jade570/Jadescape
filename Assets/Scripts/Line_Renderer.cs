<<<<<<< HEAD
﻿using System.Collections.Generic;
=======
﻿#if !UNITY_EDITOR && UNITY_ANDROID 
#define ANDROID_DEVICE
#endif

using Pvr_UnitySDKAPI;
using System.Collections.Generic;
>>>>>>> Develop
using UnityEngine;

public class Line_Renderer : MonoBehaviour
{

    public LibPdInstance pdPatch;
    List<Vector3> linePoints = new List<Vector3>();
<<<<<<< HEAD

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




    void Awake()
    {
        thisCamera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
=======
    public GameObject dot2;
    public GameObject Camera2;

    private GameObject currentLine;

    private Vector3 mouseWorld;
    private Vector3 tempFingerPos;

    public float startWidth = 0.1f;
    public float endWidth = 0.1f;
    public float threshold = 0.001f;

    Camera thisCamera;
    LineRenderer lineRenderer;

    Color lineColor_1_start = new Color(0,1,1,1);
    Color lineColor_1_end = new Color(1, 1, 1, 1);
    private int vertexCount = 0;
 

    void Awake()
    {
       thisCamera = Camera2.GetComponent<Camera>();
>>>>>>> Develop
    }

    void Update()
    {
<<<<<<< HEAD
        //no mouse input
        if ((Input.GetMouseButton(0) == false && (Input.GetMouseButton(1) == false)))
        {
            pdPatch.SendFloat("Playing", 0);
        }
        else
        {
            pdPatch.SendFloat("Playing", 1);
        }

        //left click
        if (Input.GetMouseButtonDown(0))
        {

            CreateLine(0);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 tempFingerPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, thisCamera.nearClipPlane * 100);
            mouseWorld = thisCamera.ScreenToWorldPoint(tempFingerPos);


            //if (Vector3.Distance(mouseWorld, fingerPositions[fingerPositions.Count - 1]) > .1f)
            //{
                UpdateLine(mouseWorld);
            //}

            pdPatch.SendFloat("mouseY", (int)((mouseWorld.y + 17) / 2.43));


        }

        //right click
        if (Input.GetMouseButtonDown(1))
        {

            CreateLine(1);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 tempFingerPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, thisCamera.nearClipPlane * 100);
            mouseWorld = thisCamera.ScreenToWorldPoint(tempFingerPos);


           // if (Vector3.Distance(mouseWorld, fingerPositions[fingerPositions.Count - 1]) > .1f)
            //{
                UpdateLine(mouseWorld);
            //}


            pdPatch.SendFloat("mouseY", (int)((mouseWorld.y + 17) / 2.43));
        }

    }




    void CreateLine(int mouseButton)
    {
        currentLine = new GameObject("line");

        lineRenderer = currentLine.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        if (Input.GetMouseButtonDown(0))
        {
            lineRenderer.SetColors(left1,right1);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            lineRenderer.SetColors(right1, left1);
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
=======
        // 입력이 없을 때는 퓨어데이터모듈에 데이터0을 보냄
        //if ((Input.GetMouseButton(0) == false && (Input.GetMouseButton(1) == false)))
        /*
        if (Pvr_UnitySDKAPI.Controller.UPvr_GetKey(0, Pvr_KeyCode.TRIGGER) == false)
        {
            
        }
        else
        {
            
        }
        */

        //클릭 시작하면 Line을 만듬
        if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER)|| Input.GetMouseButtonDown(0))
        {
            CreateLine();
         }

        //클릭한 채로 유지하고 있으면 Line을 연장함
        
            if (Pvr_UnitySDKAPI.Controller.UPvr_GetKey(0, Pvr_KeyCode.TRIGGER))
            {
                tempFingerPos = new Vector3(dot2.transform.position.x, dot2.transform.position.y, dot2.transform.position.z);
                UpdateLine(tempFingerPos);
            }
            else if (Input.GetMouseButton(0))
            {
                tempFingerPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, thisCamera.nearClipPlane * 100);
                mouseWorld = thisCamera.ScreenToWorldPoint(tempFingerPos);
                UpdateLine(mouseWorld);
            }
        else
        {
            //  pdPatch.SendFloat("Playing", 0);
        }
        // pdPatch.SendFloat("mouseY", (int)((tempFingerPos.y + 17) / 2.43));

        //원래코드
        //Vector3 tempFingerPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, thisCamera.nearClipPlane * 100);
        //mouseWorld = thisCamera.ScreenToWorldPoint(tempFingerPos);
        //if (Vector3.Distance(mouseWorld, fingerPositions[fingerPositions.Count - 1]) > .1f)
        //{
        //   UpdateLine(mouseWorld);
        //}
        // pdPatch.SendFloat("mouseY", (int)((mouseWorld.y + 17) / 2.43));
    }

    void CreateLine()
    {
        currentLine = new GameObject("line");
   
        lineRenderer = currentLine.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // 지정해놓은 public 변수에 맞춰서 선의 width값 지정해주는 거 추가함
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;

        // 선 색깔 지정
        lineRenderer.startColor = lineColor_1_start;
        lineRenderer.endColor = lineColor_1_end;

        // Line의 vertex 개수를 세는 변수를 초기화
        vertexCount = 0;

        //처음 Line을 만들면 vertex가 기본적으로 두 개 생김. 이걸 없애야 함.
        lineRenderer.positionCount = 0;
    }

    void UpdateLine(Vector3 newFingerPos)
    {
        // pdPatch.SendFloat("Playing", 1);
        // pdPatch.SendFloat("mouseY", (int)((newFingerPos.y + 17) / 2.43));
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(vertexCount, newFingerPos);
        vertexCount++;
  
>>>>>>> Develop
    }


}