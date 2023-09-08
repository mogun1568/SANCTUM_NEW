using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;

    /*[SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);*/

    [SerializeField]
    Vector3 pos, move, dragOrigin;  // dragOrigin: 드래그 시작 위치 저장 변수

    [SerializeField]
    float moveSpeed = 30f, scrollSpeed = 5f, minY = 15f, maxY = 80f;

    public static float mouseSensitivitiy = 100f;
    public Transform parentBody;
    float xRotation = 0f;

    Camera _towerCamera;
    Turret _towerData;

    void Start()
    {

    }

    void LateUpdate()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (!GameManager.instance.isLive)
        {
            return;
        }

        if (_mode == Define.CameraMode.QuarterView)
        {
            /*RaycastHit hit;
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else
            {
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }*/


            // 마우스 왼쪽 버튼을 누르는 순간 드래그 시작 위치를 저장합니다.
            if (Input.GetMouseButtonDown(1))
            {
                dragOrigin = Input.mousePosition;
            }

            // 마우스 왼쪽 버튼을 누르고 있는 동안 드래그한 거리만큼 카메라를 이동합니다.
            if (Input.GetMouseButton(1))
            {
                pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
                move = Quaternion.Euler(45f, 45f, 0f) * new Vector3(pos.x * moveSpeed, 0, pos.y * moveSpeed);

                // 카메라의 y 위치를 고정시키기 위해 yPosition 변수를 사용합니다.
                move.y = 0;

                transform.Translate(move, Space.World);
                dragOrigin = Input.mousePosition;
            }

            pos = transform.position;

            float scroll = Input.GetAxis("Mouse ScrollWheel");

            // 먼가 코드가 더러움
            pos += Camera.main.transform.forward * (scroll * 300 * scrollSpeed * Time.deltaTime);

            if (pos.y <= minY && scroll > 0)
            {
                while (pos.y <= minY)
                {
                    pos -= Camera.main.transform.forward * (scroll * Time.deltaTime);
                }
            }
            if (pos.y >= maxY && scroll < 0)
            {
                while (pos.y >= maxY)
                {
                    pos -= Camera.main.transform.forward * (scroll * Time.deltaTime);
                }
            }

            //pos.x = Mathf.Clamp(pos.x, 0, 100);
            //pos.z = Mathf.Clamp(pos.z, -100, 0);

            transform.position = pos;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // 코루틴을 다른 스크립트에서 쓸때도 StartCoroutine() 써줘야 함
                GameManager.instance.StartCoroutine(GameManager.instance.WaitForItemSelection());
                SetQuarterView();
            }

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivitiy * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivitiy * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // 위아래 각도 제한

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            parentBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void SetQuarterView()   // Vector3 delta
    {
        _mode = Define.CameraMode.QuarterView;

        /*GameManager.instance.isFPM = false;
        _towerData.isFPM = false;
        _towerCamera.enabled = false;
        GameManager.instance.MainCamera.SetActive(true);
        Cursor.lockState = CursorLockMode.None;*/

        //_delta = delta;
    }

    public void SetFirstPersonView()  // Vector3 delta
    {
        _mode = Define.CameraMode.FirstPersonView;

        /*GameManager.instance.isFPM = true;
        GameManager.instance.MainCamera.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;*/

        //_delta = delta;
    }

    public void GetTower(GameObject tower)
    {
        _towerCamera = tower.GetComponentInChildren<Camera>();
        _towerData = tower.GetComponent<Turret>();
    }
}