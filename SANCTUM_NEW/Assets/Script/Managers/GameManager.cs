using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    // �̰� ����� ������ ��� ��ũ��Ʈ Update �Լ��� isLive ���� ���� ���ư��� �ؾ��� - ����
    public bool isLive;
    public bool isFPM;
    public float gameTime;
    public static bool GameIsOver;

    [Header("# Player Info")]
    public int Lives;
    public int startLives = 10;
    public int Rounds;
    public int exp;
    public int nextExp = 3;
    public int countLevelUp;
    [HideInInspector] public bool isHide;

    [Header("# Game Object")]
    public PoolManager pool;
    public Map map;
    public GameObject gameOverUI;
    public LevelUp uiLevelUp;
    public GameObject MainCamera;
    //public SoundManager soundManager;

    void Start()
    {
        isLive = true;
        isFPM = false;
        GameIsOver = false;
        Lives = startLives;
        Rounds = 0;
        countLevelUp = 0;
    }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (GameIsOver)
        {
            return;
        }

        if (!isLive)
        {
            return;
        }

        if (Lives <= 0)
        {
            EndGame();
        }

        gameTime += Time.deltaTime;
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
        Stop();
    }

    public void GetExp(int _exp)
    {
        exp += _exp;

        while (exp >= nextExp)
        {
            //nextExp *= 1;
            exp -= nextExp;
            nextExp = Mathf.RoundToInt(nextExp * 1.5f);
            Mathf.Clamp(nextExp, 0, 10);
            countLevelUp++;
            //Debug.Log(countLevelUp);
        }

        if (countLevelUp > 0 && !isFPM)
        {
            StartCoroutine(WaitForItemSelection());
        }
    }

    public IEnumerator WaitForItemSelection()
    {
        while (countLevelUp > 0)
        {
            uiLevelUp.Show();
            countLevelUp--;
            // Hide() �Լ��� ����Ǹ� �Ѿ���� �ؾߵ�
            isHide = false;
            yield return new WaitUntil(() => isHide);
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;

        if (isFPM)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}