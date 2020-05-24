#if !UNITY_EDITOR && UNITY_ANDROID 
#define ANDROID_DEVICE
#endif

using Pvr_UnitySDKAPI;
using System.Collections.Generic;
using UnityEngine;

public class Line_Renderer : MonoBehaviour
{

    public LibPdInstance pdPatch;
    List<Vector3> linePoints = new List<Vector3>();
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
    }

    void Update()
    {
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
  
    }


}