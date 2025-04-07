using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    public void ChangeMusicVolume(float value)
    {
        float mixerVolume = Mathf.Lerp(-80, 0, value);
        _audioMixer.SetFloat("Music", mixerVolume);
    }

    public void ChangeEffectsVolume(float value)
    {
        float mixerVolume = Mathf.Lerp(-80, 0, value);
        _audioMixer.SetFloat("Effects", mixerVolume);
    }

    public void ToogleMasterVolume(bool value)
    {
        if (value == true)
        {
            _audioMixer.SetFloat("Master", 0);
        }
        else
        {
            _audioMixer.SetFloat("Master", -80);
        }
    }
}
