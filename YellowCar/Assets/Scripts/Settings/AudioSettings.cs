using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private float _speedAnimationPanel;
    private Vector3 _vanishPosition;


    public void EnablePanel()
    {
        _vanishPosition = transform.GetComponent<RectTransform>().anchoredPosition;
        transform.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, _speedAnimationPanel).SetEase(Ease.InOutSine);
    }

    public void DisablePanel()
    {
        transform.GetComponent<RectTransform>().DOAnchorPos(_vanishPosition, _speedAnimationPanel).SetEase(Ease.InOutSine);
    }


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
