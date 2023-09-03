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
        // 1. 모든 아이템 비활성화
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // 2. 그 중에서 랜덤 3개 아이템 활성화
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length); // items.Length
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            // 중복 검사
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
            {
                break;
            }
        }

        for (int index = 0; index < ran.Length; index++)
        {
            Item ranItem = items[ran[index]];
            ranItem.gameObject.SetActive(true);

            // 동일 아이템의 개수를 제한시킬 코드(구현할까 생각 중)
        }
    }
}
