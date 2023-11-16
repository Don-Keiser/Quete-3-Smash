using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBGM : MonoBehaviour
{
    [SerializeField] AudioSource bgmManager;
    [SerializeField] AudioClip[] BGMs;
    private bool isFocused;

    private void Start()
    {
        bgmManager.clip = BGMs[Random.Range(0, BGMs.Length)];
        bgmManager.Play();
        Invoke("RandomMusicChange", bgmManager.clip.length);
    }

    private void OnApplicationFocus(bool focus)
    {
        isFocused = focus;
    }

    private void RandomMusicChange()
    {
        bgmManager.Stop();
        bgmManager.clip = BGMs[Random.Range(0, BGMs.Length)];
        bgmManager.Play();
        Invoke("RandomMusicChange", bgmManager.clip.length);
    }
}
