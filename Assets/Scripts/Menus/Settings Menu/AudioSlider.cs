using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private AudioMixMode mixMode;

    private void Start()
    {
        mixer.SetFloat("Volume", Mathf.Log10(PlayerPrefs.GetFloat("Volume", 1) * 20));
    }

    public void OnChangeSlider(float value)
    {
        value *= 100;
        //valueText.SetText($"{value.ToString("N4")}");
        valueText.SetText($"{value.ToString("N0")}");

        switch (mixMode)
        {
            case AudioMixMode.linearAudioSourceVolume:
                audioSource.volume = value;
                break;
            case AudioMixMode.LinearMixerVolume:
                mixer.SetFloat("Volume", (-80 + value * 100));
                break;
            case AudioMixMode.LogrithmicMixerVolume:
                mixer.SetFloat("Volume", Mathf.Log10(value) * 20);
                break;
        }

        float a = Mathf.Log10(value) * 20;

        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }

    public enum AudioMixMode
    {
        linearAudioSourceVolume,
        LinearMixerVolume,
        LogrithmicMixerVolume
    }
}
