using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperMode : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // ��� ������
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("a");
            //int remainExp = GameManager.instance.nextExp - GameManager.instance.exp;
            //for (int i = 0; i < remainExp; i++)
            //{
            //    GameManager.instance.GetExp();
            //}
            GameManager.instance.uiLevelUp.Show();
        }

        // ����
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("s");
            if (GameManager.instance.isLive)
            {
                GameManager.instance.Stop();
            }
            else
            {
                GameManager.instance.Resume();
            }

        }

        // �� Ȯ��
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("d");
            GameManager.instance.map.expand_map();
        }

        // ������ ����
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("f");
            Managers.Sound.Play("Effects/Hit3", Define.Sound.Effect);
            //GameManager.instance.soundManager.Play("Effects/Hit3", SoundManager.Sound.Effect);
            GameManager.instance.Lives--;
        }
    }
}
