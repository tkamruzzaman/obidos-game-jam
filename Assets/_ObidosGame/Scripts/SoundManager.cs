using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] AudioSource phoneRingAudioSource;
    [SerializeField] AudioSource dialToneAudioSource;

    [SerializeField] AudioClip ringAudioClip;

    [SerializeField] AudioClip[] dialToneAudioClips;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InputManager.Instance.OnPickupPhone += OnPickupPhone;
        InputManager.Instance.OnDialNumber += OnDialNumber;
        InputManager.Instance.OnDialSpecialCharacter += OnDialSpecialCharacter;
    }

    public void PlayPhoneRingSound()
    {
        phoneRingAudioSource.Play();
    }

    public void StopPhoneRingSound()
    {
        if (phoneRingAudioSource.isPlaying)
        {
            phoneRingAudioSource.Stop();
        }
    }

    public void PlayKeyDialTone(int keyNumber)
    {
        dialToneAudioSource.PlayOneShot(dialToneAudioClips[keyNumber]);
    }

    private void OnPickupPhone()
    {
        StopPhoneRingSound();
    }

    private void OnDialNumber(int number)
    {
        PlayKeyDialTone(number);
    }

    private void OnDialSpecialCharacter(string specialCharacter)
    {
        if (specialCharacter == "*")
        {
            PlayKeyDialTone(10);
        }
        else if (specialCharacter == "#")
        {
            PlayKeyDialTone(11);
        }
    }
}
