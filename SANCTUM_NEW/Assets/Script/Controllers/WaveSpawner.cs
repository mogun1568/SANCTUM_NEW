using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class WaveSpawner : MonoBehaviour
{
    //public static int EnemiesAlive;

    //public GameObject[] monsters;
    //public Wave[] waves;
    int waveCount = 5;

    public Transform spawnPoint;    // 스폰할 위치

    public float timeBetweenWaves = 20f;
    private float countdown = 4f;
    private bool isFirstWave = true;

    //public TextMeshProUGUI waveCountdownText;

    //private int waveIndex = 0;

    [SerializeField] Map otherScriptInstance;

    //int monsterType;

    float bossTime = 60f;

    GameObject[] monsters;

    void Start()
    {
        //EnemiesAlive = 0;
        //otherScriptInstance = GameObject.Find("GameMaster").GetComponent<Map>();
        //otherScriptInstance = Managers.Game.map;
        //monsterType = GameManager.instance.pool.monsterPools.Length;
        SceneFader.isFading = false;
        Managers.Sound.Play("Bgms/old-story-from-scotland-147143", Define.Sound.Bgm);
        monsters = Resources.LoadAll<GameObject>("Prefabs/Monster");
    }

    void Update()
    {
        if (!Managers.Game.isLive)
        {
            return;
        }

        //if (EnemiesAlive > 0)
        //{
        //    return;
        //}

        if (countdown <= 0f)
        {
            if (isFirstWave)
            {
                isFirstWave = false;
            }
            else
            {
                otherScriptInstance.expand_map();
                waveCount = Mathf.RoundToInt(waveCount * 1.2f);
                Mathf.Clamp(waveCount, 0, 10);
                EnemyStat.AddHp += 20;
            }
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        // 1분에 한번 출현
        if (Managers.Game.gameTime >= bossTime)
        {
            Debug.Log("spawnBoss");
            GetComponent<WaveSpawner>().SpawnBossEnemy();
            bossTime *= 2f;
        }
    }

    IEnumerator SpawnWave()
    {
        Managers.Game.Rounds++;

        yield return new WaitForSeconds(6f);
        for (int i = 0; i < waveCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        //Wave wave = waves[waveIndex];

        //for (int i = 0; i < wave.count; i++)
        //{
        //    //SpawnEnemy(wave.enemy);
        //    //SpawnEnemy(monsters[Random.Range(0, monsters.Length)]);
        //    SpawnEnemy();
        //    yield return new WaitForSeconds(1f / wave.rate);
        //}

        //waveIndex++;

        //if (waveIndex == waves.Length)
        //{
        //    Debug.Log("LEVEL WON!");
        //    this.enabled = false;
        //}
    }

    void SpawnEnemy()
    {
        // 몬스터 원점이 발임
        int idx = Random.Range(0, monsters.Length);
        while (monsters[idx].name == "SalarymanDefault")
        {
            idx = Random.Range(0, monsters.Length);
        }
        Debug.Log(monsters[Random.Range(0, monsters.Length)].name);
        GameObject monster = Managers.Resource.Instantiate($"Monster/{monsters[idx].name}", spawnPoint.position, spawnPoint.rotation);
        //EnemiesAlive++;
    }

    public void SpawnBossEnemy()
    {
        Debug.Log("spawnBoss");
        Managers.Sound.Play("Bgms/battle-of-the-dragons-8037", Define.Sound.Bgm);
        GameObject monster = Managers.Resource.Instantiate("Monster/SalarymanDefault", spawnPoint.position, spawnPoint.rotation);
    }
}

