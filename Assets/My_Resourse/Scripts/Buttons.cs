using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static AudioBar;

public class Buttons : MonoBehaviour
{

    public void OpenScene(int id)
    {
        SceneManager.LoadSceneAsync(id);
    }

    public void ChangeValueSlider(AudioBar audioBar)
    {
        if (audioBar.slider.value == 1)
            audioBar.slider.value = 0;
        else
            audioBar.slider.value = 1;

        audioBar.Value = audioBar.slider.value;

        if (audioBar.sliderType == SliderType.Music)
        {
            SoundManager.Instance.SetBgmVolume();
        }
        else
        {
            SoundManager.Instance.SetEffectVolume();
        }

        SoundManager.Instance.PlayEffect(SoundList.sound_common_btn_in);
        audioBar.SetIocn();
    }
}
