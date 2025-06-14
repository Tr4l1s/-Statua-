using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudSys : MonoBehaviour
{
    public TextMeshProUGUI volumeAmount;
    public Slider slider;

    private void Start() 
    { 
        LoadAudio();
        slider.onValueChanged.AddListener(SetAudio);

    }

    public void SetAudio(float value)
    {
        AudioListener.volume = value;
        volumeAmount.text = ((int)(value * 100)).ToString();
        SaveAudio();
    }

    private void SaveAudio()
    {
        PlayerPrefs.SetFloat("audioVolume", AudioListener.volume);
    }

    private void LoadAudio()
    {
        if (PlayerPrefs.HasKey("audioVolume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("audioVolume");
            slider.value = PlayerPrefs.GetFloat("audioVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("audioVolume", 0.5f);
            AudioListener.volume = PlayerPrefs.GetFloat("audioVolume");
            slider.value = PlayerPrefs.GetFloat("audioVolume");
        }
    }
    public void GeriDon()
    {
        SceneManager.LoadScene("Menu"); // Menu sahnenin ismi
    }


}