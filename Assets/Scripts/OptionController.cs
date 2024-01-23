using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class OptionController : MonoBehaviour
{
    [SerializeField]
    private GameObject optionWindow;
    [SerializeField]
    private TMP_Text soundVolume, musicVolume;
    [SerializeField]
    private Slider soundSlider, musicSlider;
    private SoundController soundController;
    public bool optionIsOpen { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        optionIsOpen = false;
        optionWindow.SetActive(false);
        soundController = GetComponent<SoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (optionIsOpen)
        {
            soundController.ChangeMusicVolume(musicSlider.value);
            musicVolume.text = String.Format("{0:0.00}", soundController.audioSourceMusic.volume * 100) + " %";
            soundController.ChangSoundVolume(soundSlider.value);
            soundVolume.text = String.Format("{0:0.00}", soundController.audioSource.volume * 100) + " %";
        }
    }

    public void OpenOption()
    {
        optionWindow.SetActive(true);
        musicSlider.value = soundController.audioSourceMusic.volume;
        soundSlider.value = soundController.audioSource.volume;
        optionIsOpen = true;
    }
    public void CloseOption()
    {
        optionWindow.SetActive(false);
        optionIsOpen = false;
    }
}
