using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aurora_edited : MonoBehaviour {

    //헤더라는 걸 추가하면 인스펙터에서 굵은 글씨로 컴포넌트 소제목을 붙여주는구나!
    [Header ("Base Settings")]
    public int auroraSeed;
    // public이라 하더라도 range를 정할 수가 있구나
    [Range (10, 1000)]
    public int auroraParticlesCount;
    [Range (0, 0.99f)]
    public float auroraAnimationFrequency;
    [Range (0, 360f)]
    public float auroraCurvature;
    public Vector3 auroraSizes;
    public float auroraParticleThickness;

    [Header ("Aurora Lights")]
    public bool auroraLights;
    [Range (1, 100)]
    public int auroraLightsCount;
    public float auroraLightsRange;
    public float auroraLightsIntesity;

    [Header ("Colors")]
    public Gradient auroraColorMain;

    GradientColorKey[] gradientColor;

    [Header ("Resources")]
    public Material auroraMaterialMain;

    public bool IsMakingDone;
    public bool loop;

    void Start () {
        Initialize ();
    }

    //Local variables
    private ParticleSystem pSystem;
    private ParticleSystem.MainModule p_mMain;
    private ParticleSystem.EmissionModule p_mEmission;
    private ParticleSystemRenderer pRenderer;

    private ParticleSystem.Particle[] p_Particles;
    private Light[] l_Lights;
    public List<Vector3> vertexs = new List<Vector3> ();
    public float auroraTransparency;
    public float FadeSpeed;

    // Main Aurora Initialization
    private void Initialize () {

        GameObject m_Particle = new GameObject ("m_Particle");
        m_Particle.transform.SetParent (transform);

        //Create particle system
        pSystem = m_Particle.AddComponent<ParticleSystem> ();
        pRenderer = m_Particle.GetComponent<ParticleSystemRenderer> ();
        p_mEmission = pSystem.emission;
        p_mMain = pSystem.main;

        p_mEmission.enabled = false;
        p_mMain.startSpeed = 0;

        pRenderer.material = auroraMaterialMain;
        pRenderer.renderMode = ParticleSystemRenderMode.VerticalBillboard;
        pRenderer.maxParticleSize = 100f;

        p_mMain.maxParticles = 10;
        p_Particles = new ParticleSystem.Particle[10];
        // pSystem.Emit (100);
        pSystem.GetParticles (p_Particles);
        Random.InitState (0);
        auroraSeed = Time.frameCount;

        this.GetComponentInChildren<ParticleSystem> ().gameObject.layer = 8;

        auroraTransparency = 0.3f;

        pRenderer.renderingLayerMask = 8;
    }

    public void SetParticleCount () {
        if (vertexs.Count > 10) {
            p_mMain.maxParticles = vertexs.Count + 10;
            p_Particles = new ParticleSystem.Particle[vertexs.Count];
            pSystem.Emit (vertexs.Count);
            pSystem.GetParticles (p_Particles);
        }
    }

    public void GradientSet (Color NewGrad) {
        //그라데이션의 색상값 수동으로 바꾸는 법. 알파값도 바꿀 수 있고 바꾸는 법은 유니티 gradient API 구글링하면 나옴
        //그라데이션이라는 클래스가 유니티상에 있음

        //colorkey 설정 - 중간색이 파랑. (첫색은 0 끝색이 1, 구간별 색은 다양하게 넣을 수 있음)

        gradientColor = new GradientColorKey[1];
        gradientColor[0].color = NewGrad;
        gradientColor[0].time = 0.5F;

        /*
        // HDR을 켜면 hue값이 미세조정되는데 색깔이 개판으로 나오고, hdr를 끄면 색깔이 조정이 안됨
        float h, s, v, h1, h2;
        Color.RGBToHSV (NewGrad, out h, out s, out v);
        h1 = h+0.3f;
        h2 = h+0.3f;
        gradientColor[0].color = Color.HSVToRGB (h1, s, v, false);
        gradientColor[0].time = 0.0F;
        gradientColor[1].color = Color.HSVToRGB (h, s, v, false);
        gradientColor[1].time = 0.5F;
        gradientColor[2].color = Color.HSVToRGB (h2, s, v, false);
        gradientColor[2].time = 1.0F;
        */
        //밖에서 public으로 받아온 gradient의 정보를 내가 임의로 설정한 colorkey로 바꿔치기
        auroraColorMain.SetKeys (gradientColor, auroraColorMain.alphaKeys);

    }

    //Base Aurora Update
    private void Update () {
        if (IsMakingDone == true) {
            if (loop == false) {
                //선의 앞색깔과 뒷색깔을 점점 투명하게 만듬
                auroraTransparency -= 0.01f * FadeSpeed;
            } else { transform.Rotate (new Vector3 (0, 1, 0) * Time.deltaTime * 10); }

        }

        if (auroraTransparency < 0) {
            Destroy (gameObject);
        }

        if (vertexs.Count > 1) {
            for (int i = 0; i < vertexs.Count; i++) {

                float time = i / (float) (vertexs.Count - 1);

                float perlin = 0;

                perlin = Mathf.PerlinNoise (auroraSeed + Time.time * auroraAnimationFrequency, auroraSeed + time * auroraCurvature);
                float offset = perlin * 2f - 1f;

                Vector3 p_Position;
                // 써큘러가 아니면 포지션을 정하는 방식은 일단 다음과 같다.
                //if (IsMakingDone == true)
                p_Position = vertexs[i] + new Vector3 (offset / 4, 0, 0); //+ transform.position;
                //else p_Position = vertexs[i] + transform.position;
                //p_Position = new Vector3 (i+offset * auroraSizes.x, i+0, i+auroraSizes.z) + transform.position;
                // Quaternion.Euler(0, auroraRotation + angleOffset, 0) * new Vector3(offset * auroraSizes.x, 0, auroraSizes.z) + transform.position;

                float h, s, v;
                Color.RGBToHSV (auroraColorMain.Evaluate (time), out h, out s, out v);
                // 노이즈를 hue값에 끼워넣어 단색이 아니라 살짝 섞인 색이 나오게 만들기
                Color newColor = Color.HSVToRGB (h + (offset / 5000), s, v, true);
                //투명도 조절
                Color p_Color = new Color (newColor.r, newColor.g, newColor.b, auroraTransparency);

                //Color p_Color = auroraColorMain.Evaluate (time);

                float sizeY = auroraSizes.y;

                //if (vertexs.GetRange)
                p_Particles[i].position = p_Position;
                p_Particles[i].startSize3D = new Vector3 (auroraParticleThickness, sizeY, auroraParticleThickness);
                p_Particles[i].startColor = p_Color;

            }
            pSystem.SetParticles (p_Particles, vertexs.Count);
            //오로라 오브젝트만 블러처리가 되도록 카메라에 컴포넌트를 달아두었음. 그러려면 레이어8에 지정되어야함

        }

    }

}