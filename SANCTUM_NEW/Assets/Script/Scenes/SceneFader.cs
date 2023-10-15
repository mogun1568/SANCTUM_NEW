using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.GraphView;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;

    public Setting setting;

    public static bool isFading;

    public void Init()
    {
        //img = GetComponentInChildren<Image>();
        img = Util.FindChild<Image>(gameObject, "Black", true);

        //setting.IntialSetting();
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        isFading = true;
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        //GameManager.instance.soundManager.Clear();
        //setting.ChangeScene();
        SceneManager.LoadScene(scene);
        Cursor.lockState = CursorLockMode.None;
    }
}
