using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    //Coroutine co;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        //Managers.UI.ShowSceneUI<UI_Inven>();

        //Dictionary<string, Data.Item> dict = Managers.Data.ItemDict;
        //Debug.Log("me");

        // Pool 관련 코드
        /*for (int i = 0; i < 5; i++)
        {
            Managers.Resource.Instantiate("UnityChan");
        }*/

        // 코루틴 관련 코드
        /*co = StartCoroutine("ExplodeAfterSeconds", 4.0f);
        StartCoroutine("CoStopExplode", 2.0f);*/
    }

    // 코루틴 관련 코드
    /*// 1. 함수의 상태를 저장/복원 가능!
    // -> 엄청 오래 걸리는 작업을 잠시 끊거나
    // -> 원하는 타이밍에 함수를 잠시 Stop/복원하는 경우
    // 2. return -> 우리가 원하는 타입으로 가능 (class도 가능)
    // yield return: 임시정지, yield break: 완전정지
    IEnumerator CoStopExplode(float seconds)
    {
        Debug.Log("Stop Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Stop Execute!!!!");
        if (co != null)
        {
            StopCoroutine(co);
            co = null;
        }
    }

    IEnumerator ExplodeAfterSeconds(float seconds)
    {
        Debug.Log("Explode Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Explode Execute!!!!");
        co = null;
    }*/


    public override void Clear()
    {
        
    }
}
