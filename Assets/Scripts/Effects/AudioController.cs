using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSourceBackMusic;
    public AudioSource audioSourceBackSound;
    public AudioSource audioSourceBlockSound;
    public AudioSource audioSourceStarSoundMenu;
    public AudioSource audioSourceStar;

    [Header("Фоновая музыка")]
    public AudioClip[] audioClipsBackMusic;

    [Header("Фоновые звуки")]
    public AudioClip[] audioClipsBackSound;

    [Header("Звуки удара блока")]
    public AudioClip[] audioClipsHitBlock;
    public AudioClip[] audioClipsDeleteBlock;
    public AudioClip[] audioClipsHitBlockBomb;
    public AudioClip[] audioClipsHitBlockLigtning;
    public AudioClip[] audioClipsHitBlockLine;

    [Header("Звуки победы на 3 звезды")]
    public AudioClip[] audioClipsThreeStarMenu;
    [Header("Звуки победы")]
    public AudioClip[] audioClipsStarMenu;

    [Header("Звуки получения звезды")]
    public AudioClip[] audioClipsStar;


    [Header("Звуки проигрыша")]
    public AudioClip[] audioClipsGameOver;
    private AudioClip hitBlock, hitBlockBomb, hitBlockLightning;


    private void Awake()
    {
        audioSourceBackMusic.clip = audioClipsBackMusic[Random.Range(0, audioClipsBackMusic.Length)];
        audioSourceBackSound.clip = audioClipsBackSound[Random.Range(0, audioClipsBackSound.Length)];

        //hitBlock = audioClipsHitBlock[Random.Range(0, audioClipsHitBlock.Length)]; ;

        audioSourceBackMusic.Play();
        audioSourceBackSound.Play();
    }

    public void HitBlock()
    {
        audioSourceBlockSound.clip = audioClipsHitBlock[Random.Range(0, audioClipsHitBlock.Length)];

        if(!audioSourceBlockSound.isPlaying)
            audioSourceBlockSound.Play();
    }

    public void DeleteBlock()
    {
        audioSourceBlockSound.clip = audioClipsDeleteBlock[Random.Range(0, audioClipsDeleteBlock.Length)];
        audioSourceBlockSound.Play();
    }

    public void HitBlockBomb()
    {
        audioSourceBlockSound.clip = audioClipsHitBlockBomb[Random.Range(0, audioClipsHitBlockBomb.Length)];
        audioSourceBlockSound.Play();
    }

    public void HitBlockLightning()
    {
        audioSourceBlockSound.clip = audioClipsHitBlockLigtning[Random.Range(0, audioClipsHitBlockLigtning.Length)];
        audioSourceBlockSound.Play();
    }

    public void HitBlockLine()
    {
        audioSourceBlockSound.clip = audioClipsHitBlockLine[Random.Range(0, audioClipsHitBlockLine.Length)];
        audioSourceBlockSound.Play();
    }
    public void AudioStar()
    {
        audioSourceStar.clip = audioClipsStar[Random.Range(0, audioClipsStar.Length)];
        audioSourceStar.Play();
    }

    public void GameOver()
    {
        audioSourceBlockSound.clip = audioClipsStar[Random.Range(0, audioClipsStar.Length)];
        audioSourceBlockSound.Play();
    }

    public void AudioStarMenu(bool isThreeStar)
    {
        if (isThreeStar)
        {
            audioSourceStarSoundMenu.clip = audioClipsThreeStarMenu[Random.Range(0, audioClipsThreeStarMenu.Length)];
        }
        else
        {
            audioSourceStarSoundMenu.clip = audioClipsStarMenu[Random.Range(0, audioClipsStarMenu.Length)];
        }
        audioSourceStarSoundMenu.Play();
    }
}

