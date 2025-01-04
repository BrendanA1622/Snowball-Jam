using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.Audio;
using Unity.VisualScripting;
using System;

public class changeVolume : MonoBehaviour
{
    
    [SerializeField] Slider volumeSlider;
    
    void Start() {
        if(!PlayerPrefs.HasKey("musicVolume")) {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        } else {
            Load();
        }
    }

    public void ChangeVolume() {
        AudioListener.volume = volumeSlider.value;
    }

    private void Load() {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void Save() {
        PlayerPrefs.SetFloat("musicVolume",volumeSlider.value);
    }

}
