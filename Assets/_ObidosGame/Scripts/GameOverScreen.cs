using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TMP_Text gameoverText;
    [SerializeField] Image gameOverImage;

    [SerializeField] Button restartButton;

    [SerializeField] Sprite gameSuccessSprite;
    [SerializeField] Sprite gameFailedSprite;

    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    public void GameOverSet(bool isSuccess)
    {
        if (isSuccess)
        {
            gameoverText.text = "Congratulation!\nYou have WON!";
            gameOverImage.sprite = gameSuccessSprite;
        }
        else
        {
            gameoverText.text = "GAME OVER!";
            gameOverImage.sprite = gameFailedSprite;
        }
    }
}
