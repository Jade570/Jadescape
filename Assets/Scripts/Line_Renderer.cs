﻿#if !UNITY_EDITOR && UNITY_ANDROID 
#define ANDROID_DEVICE
#endif

using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;

public class Line_Renderer : MonoBehaviour {

    // public LibPdInstance pdPatch;

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

    Color lineColor_1_start = new Color (0, 1, 1, 1);
    Color lineColor_1_end = new Color (1, 0, 1, 1);
    private int vertexCount = 0;

    void Awake () {
        thisCamera = Camera2.GetComponent<Camera> ();

    }

    void Update () {
        //클릭 시작하면 Line을 만듬
        if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown (0, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonDown (0)) {
            // 마우스 오른쪽 버튼이 눌려있거나, 컨트롤러의 앱 버튼이 눌려있으면 루프음이 됨
            if (Pvr_UnitySDKAPI.Controller.UPvr_GetKey (0, Pvr_KeyCode.APP)|| Input.GetMouseButton (1)) {
                CreateLine (true);
            } else {
                CreateLine (false);
            }
        }

        //클릭한 채로 유지하고 있으면 Line을 연장함
        if (Pvr_UnitySDKAPI.Controller.UPvr_GetKey (0, Pvr_KeyCode.TRIGGER)) {
            //dot2는 controller0(첫번째 컨트롤러)의 자손 오브젝트인데, 컨트롤러가 가리키는 방향쪽에 항상 떠 있음
            tempFingerPos = new Vector3 (dot2.transform.position.x, dot2.transform.position.y, dot2.transform.position.z);
            UpdateLine (tempFingerPos);
        } else if (Input.GetMouseButton (0)) {
            //마우스 인풋이랑 카메라에서 일정거리 떨어진 z좌표를 가지고 그걸로 공간좌표를 환산
            //(z값이 속한 카메라 시선방향과의 접면으로 좌표를 이동시켜주지 않으면 마우스xy좌표가 너무 커서 선이 미칠듯이 길어짐)
            tempFingerPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, thisCamera.nearClipPlane * 100);
            mouseWorld = thisCamera.ScreenToWorldPoint (tempFingerPos);
            UpdateLine (mouseWorld);
        } else {
            // 입력이 없을 때는 퓨어데이터모듈에 데이터0을 보냄
            //  pdPatch.SendFloat ("Playing", 0);
        }

        if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyUp (0, Pvr_KeyCode.TRIGGER) || (Input.GetMouseButtonUp (0))) {
            currentLine.GetComponent<Line_Stance> ().IsMakingDone = true;
            currentLine.GetComponent<Line_Stance> ().getDir();
        }
    }

    void CreateLine (bool loop) {

        currentLine = new GameObject ("Au");
        lineRenderer = currentLine.AddComponent<LineRenderer> ();
        lineRenderer.material = new Material (Shader.Find ("Sprites/Default"));

        //만들어진 라인이 공전하려면 월드스페이스 말고 오브젝트 포지션에 종속되어야함
        lineRenderer.useWorldSpace = false;

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

        //line이 만들어지고 난 후의 움직임을 관리하는 스크립트를 line에 붙여줌
        currentLine.AddComponent<Line_Stance> ();
        currentLine.GetComponent<Line_Stance> ().FadeSpeed = 0.1f;
        if (loop) { currentLine.GetComponent<Line_Stance> ().loop = true; } else { currentLine.GetComponent<Line_Stance> ().loop = false; }
    }
   

    void UpdateLine (Vector3 newFingerPos) {
        //현재 좌표에 맞춰 소리 내기. (컴퓨터에선 작동하는데 VR빌드하면 스크립트 전체가 마비됨)

        lineRenderer.positionCount++;
        lineRenderer.SetPosition (vertexCount, newFingerPos);
        vertexCount++;
    }

}