using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider musicSlider;
    public Slider mouseSlider;

    static float soundValue = 0.7f;
    static float mouseValue = 100;

    public void IntialSetting()
    {
        musicSlider.value = soundValue;
        mouseSlider.value = mouseValue;

        audioMixer.SetFloat("Master", Mathf.Log10(musicSlider.value) * 20);
        FirstPersonCamera.mouseSensitivitiy = mouseSlider.value;
    }


    public void Sound()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(musicSlider.value) * 20);
    }

    public void Mouse()
    {
        FirstPersonCamera.mouseSensitivitiy = mouseSlider.value;
    }

    public void Close()
    {
        Managers.Sound.Play("Effects/UiClickLow", Define.Sound.Effect);
        //GameManager.instance.soundManager.Play("Effects/UiClickLow", SoundManager.Sound.Effect);
        gameObject.SetActive(false);
    }

    public void ChangeScene()
    {
        soundValue = musicSlider.value;
        mouseValue = mouseSlider.value;
    }
}
