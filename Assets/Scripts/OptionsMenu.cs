using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer gameAudio;
    
    public void SetSfxVolume(float volume)
    {
        gameAudio.SetFloat("soundVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        gameAudio.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
    }
}
