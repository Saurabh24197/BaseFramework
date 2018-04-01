using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    private void Awake()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Settings.Volume", 0.5f);
    }

    private void OnEnable()
    {
        if ( GetComponent<Slider>() != null)
             GetComponent<Slider>().value = AudioListener.volume;
    }

    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Settings.Volume", value);
    }
}
