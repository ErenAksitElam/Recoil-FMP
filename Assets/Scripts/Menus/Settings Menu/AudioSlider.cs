using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer Master;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TMP_Text volumeText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }

        // Add listener to update volume when slider value changes
        musicSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;

        if (volume <= 0.0001f)
        {
            Master.SetFloat("Volume", -80f); // Minimum volume (mute)
        }
        else
        {
            Master.SetFloat("Volume", Mathf.Log10(volume) * 20); // Logarithmic volume scaling
        }

        PlayerPrefs.SetFloat("musicVolume", volume);

        float value = musicSlider.value * 100;
        volumeText.SetText($"{value.ToString("N0")}");
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");

        SetMusicVolume();
    }
}
