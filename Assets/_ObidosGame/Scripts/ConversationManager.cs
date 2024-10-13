using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    public static ConversationManager Instance;
    
    [SerializeField] ConversationModule[] oparatorConversations;
    [SerializeField] ConversationModule[] wrongConversations;
    [SerializeField] ConversationModule[] correctConversations;

    [SerializeField] AudioSource mainAudioSource;
    [SerializeField] AudioSource secendoryAudioSource;

    [SerializeField] AudioClip gameSuccessSound;
    [SerializeField] AudioClip gameOverSound;

    [SerializeField][Range(5, 10)] float waitingForInputTime = 5;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        foreach (ConversationModule conversationModule in oparatorConversations)
        {
            conversationModule.duration = conversationModule.audioClip.length + 1.0f;
        }
        foreach (ConversationModule conversationModule in wrongConversations)
        {
            conversationModule.duration = conversationModule.audioClip.length + 1.0f;
        }
        foreach (ConversationModule conversationModule in correctConversations)
        {
            conversationModule.duration = conversationModule.audioClip.length + 1.0f;
        }

        GameManager.Instance.OnGameOver += OnGameOver;
    }

    //oparator
    public void PlayOparator(int index)
    {
        PlayAudio(oparatorConversations[index], mainAudioSource);
    }

    public float GetOparatorClipLength(int index)
    {
        return oparatorConversations[index].duration;
    }

    public string GetOparatorCorrectInput(int index)
    {
        return oparatorConversations[index].correctInput;
    }

    //wrong number
    public void PlayWrongNumber(int index)
    {
        PlayAudio(wrongConversations[index], mainAudioSource);
    }

    public float GetWrongNumberClipLength(int index)
    {
        return wrongConversations[index].duration;
    }

    public string GetWrongNumberCorrectInput(int index)
    {
        return wrongConversations[index].correctInput;
    }

    //right number
    public void PlayRightNumber(int index)
    {
        PlayAudio(correctConversations[index], mainAudioSource);
        StartCoroutine(ShowGameSuccess());
    }
    IEnumerator ShowGameSuccess()
    {
        yield return new WaitForSeconds(GetRightNumberClipLength(0) + 1);
        GameManager.Instance.GameSuccess();
    }
    public float GetRightNumberClipLength(int index)
    {
        return correctConversations[index].duration;
    }

    public string GetRightNumberCorrectInput(int index)
    {
        return correctConversations[index].correctInput;
    }


    private void PlayAudio(ConversationModule conversationModule, AudioSource audioSource)
    {
        if (conversationModule == null) { return; }
        audioSource.clip = conversationModule.audioClip;
        audioSource.Play();

    }

    private void OnGameOver(bool isSuccess)
    {
        if (mainAudioSource.isPlaying) mainAudioSource.Stop();
        if (secendoryAudioSource.isPlaying) secendoryAudioSource.Stop();

        secendoryAudioSource.PlayOneShot(isSuccess ? gameSuccessSound : gameOverSound);
    }
}

[Serializable]
public class ConversationModule
{
    //public string ID;
    public AudioClip audioClip;
    public string Text;
    public string correctInput;

    public float duration;
}
