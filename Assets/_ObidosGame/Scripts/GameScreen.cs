using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class GameScreen : MonoBehaviour
{
    [SerializeField] TMP_Text gameText;

    public void Play(int index)
    {
        StartCoroutine(PlayAudio(index));
    }

    private IEnumerator PlayAudio(int index)
    {
        //wait for connecting
        yield return new WaitForSeconds(0.2f);
        //play wrong number
        ConversationManager.Instance.PlayWrongNumber(index);

        yield return new WaitForSeconds(ConversationManager.Instance.GetWrongNumberClipLength(index));

        yield return new WaitForSeconds(0.2f);

        //Done
        GameManager.Instance.doneClipNo++;
        //wait for input
        ScreenManager.Instance.EnableWaitingForInputScreen();

    }




}
