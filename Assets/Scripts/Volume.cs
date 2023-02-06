using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public GameObject slider;
    public Slider volumeSlider;
    public JSONData jsonData;

    private bool _isVolumeSliderActive;

    private void Start()
    {
        volumeSlider.value = jsonData.gameData.Volume;
        AudioListener.volume = volumeSlider.value;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        jsonData.gameData.Volume = volumeSlider.value;
        jsonData.SaveJSON();
    }

    public void ShowVolumeSlider()
    {
        if (!_isVolumeSliderActive)
        {
            slider.SetActive(true);
            _isVolumeSliderActive = true;
        }
        else
        {
            slider.SetActive(false);
            _isVolumeSliderActive = false;
        }
    }
}
