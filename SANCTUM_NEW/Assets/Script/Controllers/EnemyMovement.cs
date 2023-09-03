using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    /*private Transform target;
    private int wavepointIndex = 0;

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();

        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWatpoint();
        }

        enemy.speed = enemy.monster.startSpeed;
    }

    void GetNextWatpoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }*/


    private Vector3 target;
    private LinkedListNode<Vector3> Mnode;
    private Enemy enemy;

    Animator anim;

    public enum MonsterState
    {
        Moving,
        Attacking,
        Die,
    }

    MonsterState _state;

    void UpdateMoving()
    {
        if (enemy.health <= 0)
        {
            _state = MonsterState.Die;
        }

        if (enemy.isAttack)
        {
            _state = MonsterState.Attacking;
        }

        if (Mnode == null)
        {
            //Debug.Log("ERROR");
            return;
        }
        Vector3 dir = target - transform.position;
        transform.Translate(enemy.speed * Time.deltaTime * dir.normalized, Space.World);

        if (Vector3.Distance(transform.position, target) <= 0.4f)
        {
            GetNextWatpoint();
            dir = target - transform.position;
        }

        // 부드럽게 변경
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);

        // 애니메이션
        anim.SetBool("isAttack", enemy.isAttack);
        anim.SetFloat("health", enemy.health);

        //enemy.speed = enemy.monster.startSpeed;
    }

    void UpdateAttacking()
    {
        if (enemy.health <= 0)
        {
            _state = MonsterState.Die;
        }

        enemy.LockOnTarget();
        anim.SetBool("isAttack", true);
    }

    void UpdateDie()
    {
        anim.SetFloat("health", 0);
    }

    void OnEnable() // pool 때문에 Start에서 OnEnable로 바꿈
    {
        Mnode = Map.points.First;
        target = Mnode.Value;
        enemy = GetComponent<Enemy>();

        _state = MonsterState.Moving;
        anim = GetComponent<Animator>();
        anim.SetBool("isAttack", false);
        anim.SetFloat("health", 100);
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        if (enemy.health > 0 && !enemy.isAttack)
        {
            _state = MonsterState.Moving;
        }

        switch (_state)
        {
            case MonsterState.Moving:
                UpdateMoving();
                break;
            case MonsterState.Attacking:
                UpdateAttacking();
                break;
            case MonsterState.Die:
                UpdateDie();
                break;
        }
    }

    void GetNextWatpoint()
    {
        if (Mnode.Next == null)
        {
            EndPath();
            return;
        }
        Mnode = Mnode.Next;
        target = Mnode.Value;
    }

    void EndPath()
    {
        Managers.Sound.Play("Effects/Hit3", Define.Sound.Effect);
        //GameManager.instance.soundManager.Play("Effects/Hit3", SoundManager.Sound.Effect);
        GameManager.instance.Lives--;
        //WaveSpawner.EnemiesAlive--;
        Managers.Resource.Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}