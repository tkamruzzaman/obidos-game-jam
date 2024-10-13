using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartScreen : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] RectTransform startRect;
    [SerializeField] VideoPlayer gameBackStoryVideo;
    [SerializeField] RectTransform phoneRingPanelRect;

    [SerializeField] TMP_Text introText01;
    [SerializeField] TMP_Text introText02;

    [SerializeField] TMP_Text startText;
    [SerializeField] TMP_Text phoneRingText;

    [SerializeField] float textWaitTime = 2.0f;
    [SerializeField] float text01Time = 3.0f;
    [SerializeField] float text02Time = 3.0f;

    [SerializeField][Range(0, 5)] float phoneRingMinDelay = 3.0f;
    [SerializeField][Range(5, 30)] float phoneRingMaxDelay = 20.0f;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayButtonAction);
        print(gameBackStoryVideo.clip.length);
        phoneRingPanelRect.gameObject.SetActive(false);
    }

    private void Start()
    {
        InputManager.Instance.OnPickupPhone += OnPickupPhone;
    }

    void Update()
    {
        if (GameManager.Instance.isIntroEnded) { return; }

        if (GameManager.Instance.isPhoneRang && InputManager.Instance.IsHungup)
        {
            GameManager.Instance.EndGame();
        }
    }

    private void PlayButtonAction()
    {
        startRect.gameObject.SetActive(false);
        gameBackStoryVideo.Play();
        StartCoroutine(IE_StartGame());
    }

    private IEnumerator IE_StartGame()
    {
        yield return new WaitForSeconds((float)(gameBackStoryVideo.length + 0.5f));
        phoneRingPanelRect.gameObject.SetActive(true);

        gameBackStoryVideo.gameObject.SetActive(false);

        GameManager.Instance.isIntroEnded = true;
        Invoke(nameof(RingPhone), Random.Range(phoneRingMinDelay, phoneRingMaxDelay));
    }

    private void RingPhone()
    {
        startText.gameObject.SetActive(false);
        phoneRingText.gameObject.SetActive(true);

        //ring the phone
        GameManager.Instance.isPhoneRang = true;

        InputManager.Instance.Ring();
    }

    private void OnPickupPhone()
    {
        phoneRingText.gameObject.SetActive(false);

    }

}
