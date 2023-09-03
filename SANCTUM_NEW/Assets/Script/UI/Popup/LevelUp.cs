using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        GameManager.instance.Stop();
        rect.localScale = Vector3.one;
        Managers.Sound.Play("Effects/LevelUpLong", Define.Sound.Effect);
        //GameManager.instance.soundManager.Play("Effects/LevelUpLong", SoundManager.Sound.Effect);
    }

    public void Hide()
    {
        GameManager.instance.Resume();
        //Time.timeScale = 1f;
        rect.localScale = Vector3.zero;
        GameManager.instance.isHide = true;
    }

    //public void Select(int index)
    //{
    //    items[index].Onclick();
    //}

    void Next()
    {
        // 1. ��� ������ ��Ȱ��ȭ
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // 2. �� �߿��� ���� 3�� ������ Ȱ��ȭ
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length); // items.Length
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            // �ߺ� �˻�
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
            {
                break;
            }
        }

        for (int index = 0; index < ran.Length; index++)
        {
            Item ranItem = items[ran[index]];
            ranItem.gameObject.SetActive(true);

            // ���� �������� ������ ���ѽ�ų �ڵ�(�����ұ� ���� ��)
        }
    }
}
