using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 [[추가할 기능]]
 2. LineUpdate가 끝났다는 신호를 받으면 마지막 vertex 여러개를 바탕으로 vec3방향을 정해서 어느 방향으로 떠다니게 만들건지 정해야 함.
    이 때 중요한 건 사용자를 중심으로 공전하듯이 돌아다녀야 한다는 거.
 - 지우기 : collider를 추가하고 앱 버튼을 눌러 선을 선택하면 지워짐
 */

public class Line_Stance : MonoBehaviour {

    public float existLength;
    public bool loop;
    public bool IsMakingDone;
    public float FadeSpeed;

    public int numOfVertex;

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

        // 선만들기가 끝났고, 루프음이 아닐때만 점점 투명해짐
        if (IsMakingDone == true) {

            //공전

            if (loop == false) {
                //선의 앞색깔과 뒷색깔을 점점 투명하게 만듬
                startColor_Now.a -= 0.01f * FadeSpeed;
                endColor_Now.a -= 0.01f * FadeSpeed;
                lineRenderer.startColor = startColor_Now;
                lineRenderer.endColor = endColor_Now;
            } else { RoationAndRevolution (); }

        }

        // 사용자를 중심으로 공전

        //선이 투명해지면 스크립트가 붙어있는 게임오브젝트를 파괴
        if (startColor_Now.a < 0 && endColor_Now.a < 0) {
            Destroy (gameObject);
        }

    }

    public void getDir () {
        // 방향을 수십개의 vertex중 어떤 것을 골라 어떻게 계산해야 할지 아직 논의안함. 일단은 맨 앞이랑 맨 뒤 비교하는걸로
        floatDir = lineRenderer.GetPosition (lineRenderer.positionCount - 1) - lineRenderer.GetPosition (0);
        
       
        floatDir.Normalize ();
       // floatDir = new Vector3 (-floatDir.y,floatDir.x , floatDir.z);
       // floatDir = new Vector3 (-floatDir.y,floatDir.x , floatDir.z);

    }

    void RoationAndRevolution () {
        transform.Rotate (floatDir * Time.deltaTime * 10);
    }
}