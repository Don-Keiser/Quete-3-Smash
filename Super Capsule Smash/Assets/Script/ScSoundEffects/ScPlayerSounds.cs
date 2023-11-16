using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScPlayerSounds : MonoBehaviour
{
    [SerializeField] AudioSource audioManager;
    [SerializeField] AudioClip[] punchSounds;
    [SerializeField] AudioClip fatPunchSound;
    [SerializeField] AudioClip shieldHitSound;
    [SerializeField] AudioClip jumpSound;

    public void RandomPunchSound()
    {
        audioManager.clip = punchSounds[Random.Range(0, punchSounds.Length)];
        audioManager.Play();
    }

    public void ShieldHitSound()
    {
        audioManager.clip = shieldHitSound;
        audioManager.Play();
    }

    public void JumpSound()
    {
        audioManager.clip = jumpSound;
        audioManager.Play();
    }
}
