using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControlle : MonoBehaviour
{
    private void Update()
    {
        GetComponent<AudioSource>().volume = Data.VolumeMusic;
    }
}
