using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    [SerializeField] GameObject boundary;

    [SerializeField] Slider hp;
    [SerializeField] TextMeshProUGUI damage;
    [SerializeField] TextMeshProUGUI fireRate;
    [SerializeField] Transform sphere;
    [SerializeField] Transform sphere1;

    [SerializeField] GameObject fPMButton;


    private Node target;
    //private Turret tower;
    TowerControl towerControl;
    //public TextMeshProUGUI retrunExp; // 추가 예정


    //[SerializeField] Camera cam;
    //[SerializeField] RectTransform height;
    //[SerializeField] RectTransform uiElementRectTransform;
    //[SerializeField] RectTransform a;
    //[SerializeField] RectTransform b;

    private void Start()
    {
        ui.SetActive(false);
        boundary.SetActive(false);
    }
    private void Update()
    {
        //if(target != null)
        //{
        //    Vector3 vec = cam.transform.position - ui.transform.position;
        //    float mag = vec.magnitude;
        //    vec.Normalize();
        //    Quaternion Rot = Quaternion.LookRotation(vec);
        //    // Debug.Log(mag);


        //    if (mag >= 24f)
        //    {
        //        ui.transform.localScale = new Vector3(.15f - 2 / mag + mag * 0.0005f, .15f - 2 / mag + mag * 0.0005f, .15f - 2 / mag + mag * 0.0005f);
        //        height.anchoredPosition = new Vector2(0, 10 - mag / 50 + mag * 0.05f);
        //        // Debug.Log(height.anchoredPosition);
        //    }
        //    else
        //    {
        //        ui.transform.localScale = new Vector3(.11f, .11f, .11f);
        //    }
        //    // ui.transform.position = new Vector3(0f, 10f-mag/1000, 0f);
        //    //ui.transform.rotation = Rot * Quaternion.Euler(180f, 0f, 180f);

        //    //// 카메라가 보는 방향을 가져옵니다.
        //    //Vector3 cameraForward = mainCamera.transform.forward;
        //    //cameraForward.y = 0f; // y 축 방향을 0으로 설정하여 수평을 유지합니다.

        //    //// 월드 스페이스 UI를 회전시킵니다.
        //    //uiRectTransform.rotation = Quaternion.LookRotation(cameraForward, Vector3.up);
        //}

        if (ui.activeSelf)
        {
            RectTransform uiElementRectTransform = GetComponentInChildren<RectTransform>();  // UI 요소의 RectTransform 컴포넌트 가져오기

            Vector3 anchoredPosition3D = uiElementRectTransform.anchoredPosition3D;  // UI 요소의 앵커드 위치 가져오기
            Vector3 screenPosition = uiElementRectTransform.TransformPoint(anchoredPosition3D);  // 화면 좌표계로 변환

            // screenPosition을 사용하여 UI 요소의 화면 좌표계 위치 활용


            Vector3 uiScreenPoint = screenPosition;  // UI의 화면 좌표계 위치

            Vector3 uiNormal = Camera.main.transform.forward;  // UI 평면의 법선 벡터
            Vector3 cameraPlaneNormal = -Camera.main.transform.forward;  // 카메라 평면의 법선 벡터

            Plane uiPlane = new Plane(uiNormal, uiScreenPoint);  // UI 평면 생성
            Plane cameraPlane = new Plane(cameraPlaneNormal, Camera.main.transform.position);  // 카메라 평면 생성

            float distance = Mathf.Abs(cameraPlane.GetDistanceToPoint(uiScreenPoint));
            //Debug.Log(distance);

            uiElementRectTransform.localScale = new Vector3(distance * 0.003f, distance * 0.003f, 0f);  // 크기 조정

            float x = distance * 0.2f;
            x = Mathf.Clamp(x, 6f, 15f);
            float y = x * 0.5f;
            uiElementRectTransform.anchoredPosition3D = new Vector3(0f, x, y);


            ChangeInfo();


            //Vector2 towerViewportPosition = cam.WorldToViewportPoint(target.turret.transform.position);
            ////RectTransform uiCanvasRectTransform = ui.GetComponent<RectTransform>();

            //float screenWidth = Screen.width;
            //float screenHeight = Screen.height;

            //float canvasWidth = uiElementRectTransform.sizeDelta.x;
            //float canvasHeight = uiElementRectTransform.sizeDelta.y;

            //float uiPosX = (towerViewportPosition.x * screenWidth);// - (canvasWidth * 0.5f);
            //float uiPosY = (towerViewportPosition.y * screenHeight);// - (canvasHeight * 0.5f);
            ////Debug.Log((canvasWidth, canvasHeight));

            //uiElementRectTransform.anchoredPosition = new Vector2(uiPosX, uiPosY);

            //float distance = Vector3.Distance(cam.transform.position, target.turret.transform.position); // 카메라와 오브젝트 사이의 거리
            //Debug.Log("Distance: " + distance);

            //a.anchoredPosition = new Vector2(a.anchoredPosition.x, a.anchoredPosition.y + (distance2 - distance));
            //b.anchoredPosition = new Vector2(b.anchoredPosition.x + (distance2 - distance), b.anchoredPosition.y);

            //distance2 = distance;



            if (!target.turret.activeSelf)
            {
                Hide();
            }
        }
    }

    public void SetTarget(Node _target)
    {
        if (GameManager.instance.isFPM)
        {
            return;
        }

        target = _target;

        transform.position = target.GetBuildPosition();

        //tower = target.turret.GetComponent<Turret>();
        towerControl = target.turret.GetComponent<TowerControl>();

        //retrunExp.text = 경헙치 + "Exp"  // 추가 예정

        ui.SetActive(true);
        boundary.SetActive(true);
        if (towerControl.itemData.itemName == "Water")
        {
            fPMButton.SetActive(false);
        }
        else
        {
            fPMButton.SetActive(true);
        }

    }

    void ChangeInfo()
    {
        float curHP = towerControl._stat.HP;
        float maxHP = 100f;
        hp.value = curHP / maxHP;

        damage.text = towerControl._stat.BulletDamage.ToString("F0");
        fireRate.text = towerControl._stat.FireRate.ToString("F1");

        sphere.localScale = new Vector3(towerControl._stat.Range * 2, sphere.localScale.y, towerControl._stat.Range * 2);
        sphere1.localScale = new Vector3(towerControl._stat.Range * 2 - 0.5f, sphere1.localScale.y, towerControl._stat.Range * 2 - 0.5f);
    }


    public void Hide()
    {
        ui.SetActive(false);
        boundary.SetActive(false);
    }

    public void FirstPersonMode()
    {
        target.FirstPersonMode();
        Managers.Select.DeselectNode();
        //BuildManager.instance.DeselectNode();
        FPSUI.getTower(towerControl);
    }

    public void Demolite()
    {
        target.DemoliteTower();
        Managers.Select.DeselectNode();
        //BuildManager.instance.DeselectNode();
    }
}
