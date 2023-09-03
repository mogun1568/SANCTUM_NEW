using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public static float mouseSensitivitiy = 100f;

    public Transform parentBody;

    float xRotation = 0f;

    Turret turretData;

    void OnEnable()
    {
        GameManager.instance.isFPM = true;
        GameManager.instance.MainCamera.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        turretData = GetComponentInParent<Turret>();
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        //Debug.Log(mouseSensitivitiy);

        if (Input.GetKeyDown(KeyCode.E))
        {
            // �ڷ�ƾ�� �ٸ� ��ũ��Ʈ���� ������ StartCoroutine() ����� ��
            GameManager.instance.StartCoroutine(GameManager.instance.WaitForItemSelection());
            ExitFirstPersonMode();
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivitiy * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivitiy * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // ���Ʒ� ���� ����

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        parentBody.Rotate(Vector3.up * mouseX);

        if (Input.GetMouseButtonDown(0))  // ���콺 ��Ŭ���� ����
        {
            FireBullet();
        }
    }


    void FireBullet()
    {
        Managers.Sound.Play("Effects/Arrow", Define.Sound.Effect);
        //GameManager.instance.soundManager.Play("Effects/Arrow", SoundManager.Sound.Effect);
        GameObject bulletGO = Managers.Resource.Instantiate("Tower/Prefab/Bullet/StandardBullet");
        bulletGO.transform.SetPositionAndRotation(transform.position, transform.rotation);
        //GameObject bulletGO = GameManager.instance.pool.GetBullet(turretData.bulletIndex, transform.position, transform.rotation);  // �Ѿ� ����
        bulletGO.transform.GetChild(0).gameObject.SetActive(false);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.isFPM = true;
        bullet.damage = turretData.bulletDamage * 1.5f;
        float bulletForce = turretData.bulletSpeed * 1.5f;

        bullet.firePoint = turretData.transform.position;
        bullet.range = turretData.range;

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        // �Ѿ˿� ���� ���� �߻�
        bulletRigidbody.velocity = transform.forward * bulletForce;
    }


    void ExitFirstPersonMode()
    {
        GameManager.instance.isFPM = false;
        turretData.isFPM = false;
        gameObject.SetActive(false);
        GameManager.instance.MainCamera.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
