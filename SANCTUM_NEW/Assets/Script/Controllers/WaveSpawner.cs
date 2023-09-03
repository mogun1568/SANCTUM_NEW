using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class WaveSpawner : MonoBehaviour
{
    //public static int EnemiesAlive;

    //public GameObject[] monsters;
    public Wave[] waves;
    int waveCount = 5;

    public Transform spawnPoint;    // ������ ��ġ

    public float timeBetweenWaves = 20f;
    private float countdown = 4f;
    private bool isFirstWave = true;

    //public TextMeshProUGUI waveCountdownText;

    //private int waveIndex = 0;

    private Map otherScriptInstance;

    //int monsterType;

    float bossTime = 60f;

    void Start()
    {
        //EnemiesAlive = 0;
        //otherScriptInstance = GameObject.Find("GameMaster").GetComponent<Map>();
        otherScriptInstance = GameManager.instance.map;
        //monsterType = GameManager.instance.pool.monsterPools.Length;
        SceneFader.isFading = false;
        Managers.Sound.Play("Bgms/old-story-from-scotland-147143", Define.Sound.Bgm);
        //GameManager.instance.soundManager.Play("Bgms/old-story-from-scotland-147143", SoundManager.Sound.Bgm);
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
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
                Enemy.addHealth += 20;
            }
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        // 1�п� �ѹ� ����
        if (GameManager.instance.gameTime >= bossTime)
        {
            Debug.Log("spawnBoss");
            GetComponent<WaveSpawner>().SpawnBossEnemy();
            bossTime *= 2f;
        }

        //waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        GameManager.instance.Rounds++;


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
        // ���� ������ ����
        GameObject monster = Managers.Resource.Instantiate("Monster/AlienDefault");
        monster.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        //GameManager.instance.pool.GetMonster(Random.Range(0, monsterType - 1), spawnPoint.position, spawnPoint.rotation); //  * Quaternion.Euler(0f, 180f, 0f)
        //Instantiate(enemy, spawnPoint.position - Vector3.up * 1.5f, spawnPoint.rotation);
        //EnemiesAlive++;
    }

    public void SpawnBossEnemy()
    {
        //Debug.Log("spawnBoss");
        Managers.Sound.Play("Bgms/battle-of-the-dragons-8037", Define.Sound.Bgm);
        //GameManager.instance.soundManager.Play("Bgms/battle-of-the-dragons-8037", SoundManager.Sound.Bgm);
        // �̰� ���ο� pool ������� ���ľ� ��
        //GameManager.instance.pool.GetMonster(monsterType - 1, spawnPoint.position, spawnPoint.rotation);
    }
}

