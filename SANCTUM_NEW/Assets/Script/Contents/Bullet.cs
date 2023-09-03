using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum InfoType { General, Explode, Dot, Slow, Massive }
    public InfoType type;

    private Transform target;

    [HideInInspector] public float speed;
    [HideInInspector] public float damage;
    [HideInInspector] public float explosionRadius = 8f;

    //public bool isDot;
    public GameObject imapctEffect;

    // 1��Ī ��� ����
    [HideInInspector] public bool isFPM;
    [HideInInspector] public Vector3 firePoint; // �ʱ� ���� ��ġ
    [HideInInspector] public float range;
    float distanceFromTower; // �ʱ� ��ġ�κ����� �Ÿ�

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void OnEnable()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        distanceFromTower = 0f;
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        if (isFPM)
        {
            distanceFromTower = Vector3.Distance(transform.position, firePoint);
            if (distanceFromTower >= range)
            {
                Managers.Resource.Destroy(gameObject);
                //gameObject.SetActive(false);
            }
            return;
        }

        if (target == null)
        {
            Managers.Resource.Destroy(gameObject);
            //gameObject.SetActive(false);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isFPM)
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            //Debug.Log("hit");
            target = other.gameObject.transform;
            HitTarget();
        }
        else
        {
            Managers.Resource.Destroy(gameObject);
            //gameObject.SetActive(false);
        }

    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(imapctEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        // explosionRadius��� ���� �ȿ� ������
        //if (explosionRadius > 0f)
        //{
        //    ExplodeDamage();
        //}
        //else if (isDot)
        //{
        //    DotDamage(target);
        //}
        //else
        //{
        //    Damage(target);
        //}

        switch (type)
        {
            case InfoType.General:
                Damage(target);
                break;
            case InfoType.Explode:
                ExplodeDamage();
                break;
            case InfoType.Dot:
                DotDamage(target);
                break;
            case InfoType.Slow:
                SlowSpeed(target);
                break;
            case InfoType.Massive:
                MassiveDamage(target);
                break;
        }

        Managers.Resource.Destroy(gameObject);
        //gameObject.SetActive(false);
    }

    // ������ Ÿ��
    void ExplodeDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    // ������ (������ �Ѵ������ �ٷ� ����)
    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }

    // ��Ʈ ������ �ڵ�
    void DotDamage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.DotDamage(damage);
        }
    }

    void SlowSpeed(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.Slow(damage, 0.5f);
        }
    }

    void MassiveDamage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage * 2);
        }
    }

    // �Ѿ� Ÿ�� ���� ǥ��(�������)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
