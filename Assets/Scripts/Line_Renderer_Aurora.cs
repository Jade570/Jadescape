﻿#if !UNITY_EDITOR && UNITY_ANDROID 
#define ANDROID_DEVICE
#endif

using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Line_Renderer_Aurora : MonoBehaviour {

    public GameObject dot2;
    public GameObject Camera2;
    public GameObject aurora;
    GameObject newLine;

    private Vector3 tempFingerPos;

    private Vector3 previousFingerPos;

    Camera thisCamera;
    bool PalleteToggle;
    Vector2 TouchPos;
    public float ColorPos;
    Color PalleteColor;
    public GameObject ColorPallete;
    public GameObject CurrentCollorDisplay;

    int reloadStack;

    void Awake () {
        //PC 시뮬레이션을 위한 카메라 가져오기 - 이거 카메라 굳이 안가져오고 mainCamera 함수쓰면 될거같은데
        thisCamera = Camera2.GetComponent<Camera> ();

        //컬러 팔레트 관련 초기화
        PalleteToggle = false;
        ColorPallete.SetActive (PalleteToggle);
        PalleteColor = new Color (255, 0, 0);
        CurrentCollorDisplay.GetComponent<Image> ().color = PalleteColor;
    }

    void Update () {
        LineInteraction ();
        ColorPalleteInteraction ();

        if (Pvr_UnitySDKAPI.Controller.UPvr_GetKey(0, Pvr_KeyCode.APP) || Input.GetMouseButton(1))
        {
            reloadStack++;
        }else
        {
            reloadStack = 0;
        }

        if (reloadStack > 130)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    void ColorPalleteInteraction () {
        if (Controller.UPvr_GetKeyDown (0, Pvr_KeyCode.TOUCHPAD) || Input.GetMouseButtonDown (2)) {
            PalleteToggle = true;
            ColorPallete.SetActive (PalleteToggle);
        }

        if (PalleteToggle) {

            if (Input.GetMouseButton (2)) {
                TouchPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
                ColorPos = Mathf.Atan2 (TouchPos.y - Screen.height / 2, TouchPos.x - Screen.width / 2) * Mathf.Rad2Deg / 360;
            } else if (Controller.UPvr_IsTouching (0)) {
                //(추후 확인 필요) atan2의 치역이 -90에서 90인지 아닌지 아직 확인안했음.
                //터치패드의 위치를 중심점에서의 각도로 변환한 뒤 0-1범위의 hue값에 집어넣기 위해 변환
                TouchPos = Controller.UPvr_GetTouchPadPosition (0);
                ColorPos = Mathf.Atan2 (TouchPos.y - 127, TouchPos.x - 127) * Mathf.Rad2Deg / 360;
            }

            if (ColorPos < 0) {
                ColorPos += 1.0f;
            }
            CurrentCollorDisplay.GetComponent<Image> ().color = Color.HSVToRGB (ColorPos, 1, 1);
        }

        if (Controller.UPvr_GetKeyUp (0, Pvr_KeyCode.TOUCHPAD) || (Input.GetMouseButtonUp (2))) {
            //현재 골라둔 색깔로 오로라 색깔 선택
            PalleteColor = Color.HSVToRGB (ColorPos, 1, 1);
            PalleteToggle = false;
            ColorPallete.SetActive (PalleteToggle);
        }

    }
    void LineInteraction () {
        //클릭 시작하면 Line을 만듬
        if (Controller.UPvr_GetKeyDown (0, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonDown (0)) {
            // 마우스 오른쪽 버튼이 눌려있거나, 컨트롤러의 앱 버튼이 눌려있으면 루프음이 됨
            if (Pvr_UnitySDKAPI.Controller.UPvr_GetKey (0, Pvr_KeyCode.APP) || Input.GetMouseButton (1)) {
                CreateLine (true);
            } else {
                CreateLine (false);
            }
        }

        //클릭한 채로 유지하고 있으면 Line을 연장함
        if (Controller.UPvr_GetKey (0, Pvr_KeyCode.TRIGGER)) {
            //dot2는 controller0(첫번째 컨트롤러)의 자손 오브젝트인데, 컨트롤러가 가리키는 방향쪽에 항상 떠 있음
            tempFingerPos = new Vector3 (dot2.transform.position.x, dot2.transform.position.y, dot2.transform.position.z);
            UpdateLine (tempFingerPos);
        } else if (Input.GetMouseButton (0)) {
            //마우스 인풋이랑 카메라에서 일정거리 떨어진 z좌표를 가지고 그걸로 공간좌표를 환산
            //(z값이 속한 카메라 시선방향과의 접면으로 좌표를 이동시켜주지 않으면 마우스xy좌표가 너무 커서 선이 미칠듯이 길어짐)
            tempFingerPos = thisCamera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, thisCamera.nearClipPlane * 100));
            UpdateLine (tempFingerPos);
        }

        //선 만들기가 끝났을 때 하는 행동
        if (Controller.UPvr_GetKeyUp (0, Pvr_KeyCode.TRIGGER) || (Input.GetMouseButtonUp (0))) {
            //현재 만들어진 오브젝트쪽 스크립트에 만들기가 끝났다는걸 bool을 true로 바꿔서 알려줌
            newLine.GetComponent<Aurora_edited> ().IsMakingDone = true;
            //updateLine에서 이루어지는 보간이 더이상 작동안하도록 해주는 조치(update함수에 설명있음)
            previousFingerPos = new Vector3 (0, 0, 0);
        }
    }

    void CreateLine (bool loop) {
        //오로라 프리팹을 소환
        newLine = Instantiate (aurora, new Vector3 (0, 0, 0), Quaternion.identity);

        // 선 색깔 지정
        newLine.GetComponent<Aurora_edited> ().GradientSet (PalleteColor);

        newLine.GetComponent<Aurora_edited> ().loop = loop;

    }

    void UpdateLine (Vector3 newFingerPos) {

        // 선을 다 만들면 previousFingerPos가 0,0,0으로 세팅되게 해 두었음. 따라서 선을 처음 만들기 시작할땐 보간을 하지 않음
        if (previousFingerPos != new Vector3 (0, 0, 0)) {
            // 보간이 얼마나 촘촘하게 이뤄질지 distance 함수 뒤에 있는 계수로 정함
            int bogan = (int) (Vector3.Distance (newFingerPos, previousFingerPos) * 30);
            for (float i = 1; i < bogan; i++) {
                //파티클 위치값 추가 (for문의 반복회수(보간이 이루어지도록 설정한 거리)만큼 이전 위치와 현재 위치 사이를 나누어 촘촘하게 함)
                newLine.GetComponent<Aurora_edited> ().vertexs.Add (Vector3.Lerp (previousFingerPos, newFingerPos, i / bogan));

            }
            //몇번의 보간이 이뤄졌는지 보고
            //Debug.LogWarning (bogan);

        }

        //보간작업이 끝나면 previousFingerPos를 다음 프레임을 위해 갱신
        previousFingerPos = newFingerPos;

        //파티클 위치값 추가
        newLine.GetComponent<Aurora_edited> ().vertexs.Add (newFingerPos);

        //vertex란 List에 위치값을 추가해준 만큼 파티클의 개수도 같이 늘림
        newLine.GetComponent<Aurora_edited> ().SetParticleCount ();
    }

}