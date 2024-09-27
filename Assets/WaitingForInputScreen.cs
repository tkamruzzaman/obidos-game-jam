using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaitingForInputScreen : MonoBehaviour
{
    [SerializeField][Range(5, 10)] float waitingForInputTime = 5.0f;
    float currentTime;
    [SerializeField] Image timerImage;
    [SerializeField] TMP_Text timerText;

    private void OnEnable()
    {
        waitingForInputTime = 5.0f + 1.0f;
        InputManager.Instance.IsWaitingForInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = waitingForInputTime -= Time.deltaTime;

        timerText.text = ((int)waitingForInputTime).ToString();
        timerImage.fillAmount = currentTime / waitingForInputTime;

        if (waitingForInputTime <= 0)
        {
            waitingForInputTime = 0;
            //waiting is over
            InputManager.Instance.IsWaitingForInput = false;
            //check inputs if its corect or not
            if (GameManager.Instance.isPhoneRang
                && !GameManager.Instance.isGameStarted
                && !GameManager.Instance.isGameEnded
                && InputManager.Instance.IsPlayerInputCorrect(ConversationManager.Instance.GetOparatorCorrectInput(0)))
            {
                //InputManager.Instance.ResetPlayerInput();
                //game start
                GameManager.Instance.StartGame();
                InputManager.Instance.InputCompleted();

            }
            else if (GameManager.Instance.isGameStarted
            && !GameManager.Instance.isGameEnded
            && InputManager.Instance.IsPlayerInputCorrect(ConversationManager.Instance.GetWrongNumberCorrectInput(GameManager.Instance.GetCurrentWrongNumberIndex() - 1)))
            {
                //go to next clip
                print("]]]]]]]]............RITGH...............[[[[[]]]]]");
                InputManager.Instance.InputCompleted();
                //gameObject.SetActive(false);
            }
            else
            {
                //game end
                GameManager.Instance.EndGame();
            }
        }
    }



}
