using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 이 스크립트에 추가할 기능
 1. 주어진 시간동안 점점 희미해지다가 사라지면서 게임오브젝트 자체삭제
 2. LineUpdate가 끝났다는 신호를 받으면 마지막 vertex 여러개를 바탕으로 vec3방향을 정해서 어느 방향으로 떠다니게 만들건지 정해야 함.
    이 때 중요한 건 사용자를 중심으로 공전하듯이 돌아다녀야 한다는 거.
 3. (지우기 기능) collider를 추가하고 컨트롤러로 선을 선택했을 때 지워지도록
 4. (루프 구분) 루프 오브젝트일 경우 주어진 시간 무한이게 하고 싶은데 어떻게 구분할까? bool을 하나 추가하면될까
 */

public class Line_Stance : MonoBehaviour {

    public float existLength;
    public bool loop;
    public bool IsMakingDone;
    public float FadeSpeed;

    private Vector3 floatDir;
    LineRenderer lineRenderer;
    Color startColor_Now, endColor_Now;

    // Start is called before the first frame update
    void Start () {
        lineRenderer = this.GetComponent<LineRenderer> ();
        startColor_Now = lineRenderer.startColor;
        endColor_Now = lineRenderer.endColor;
    }

    // Update is called once per frame
    void Update () {
        if (IsMakingDone == true) {

            //선의 앞색깔과 뒷색깔을 점점 투명하게 만듬
            startColor_Now.a -= 0.01f * FadeSpeed;
            endColor_Now.a -= 0.01f * FadeSpeed;
            lineRenderer.startColor = startColor_Now;
            lineRenderer.endColor = endColor_Now;
            
            
        }

            //선이 투명해지면 스크립트가 붙어있는 게임오브젝트를 파괴
        if (startColor_Now.a < 0 &&  endColor_Now.a < 0) {
            Destroy (gameObject);
        }

    }
}