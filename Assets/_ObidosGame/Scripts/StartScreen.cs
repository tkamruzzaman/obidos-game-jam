using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] TMP_Text introText01;
    [SerializeField] TMP_Text introText02;

    [SerializeField] TMP_Text startText;

    [SerializeField] float textWaitTime = 2.0f;
    [SerializeField] float text01Time = 3.0f;
    [SerializeField] float text02Time = 3.0f;


    [SerializeField] bool isPhoneRang;
    [SerializeField] bool isIntroEnded;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isIntroEnded) { return; }
        textWaitTime -= Time.deltaTime;

        if (textWaitTime <= 0.0f)
        {
            introText01.gameObject.SetActive(true);
            text01Time -= Time.deltaTime;
            if (text01Time <= 0.0f)
            {
                introText01.gameObject.SetActive(false);
                introText02.gameObject.SetActive(true);
                text02Time -= Time.deltaTime;
                if (text02Time <= 0)
                {
                    introText02.gameObject.SetActive(false);

                    startText.gameObject.SetActive(true);
                    Invoke(nameof(GameStart), Random.Range(5, 6));
                    isIntroEnded = true;
                }
            }
        }

        //if(isPhoneRang && )
    }

    private void GameStart()
    {
        startText.gameObject.SetActive(false);
        //ring the phone
        isPhoneRang = true;

        InputManager.Instance.Ring();

    }

}
