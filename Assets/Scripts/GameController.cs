using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int coinScore;
    public Text coinScoreText;
    public GameObject life;
    public int playerLives;
    public Transform lives;
    public bool isPaused;
    public GameObject pauseMenu;
    public GameObject menuBG;
    public GameObject shop;
    public GameObject gameOverPanel;
    public static GameController current;
    
    // Start is called before the first frame update
    void Start()
    {
        current = this;
        AddLife(3);
    }

    // Update is called once per frame
    void Update()
    {
        coinScoreText.text = coinScore.ToString();
    }

    public void AddLife(int lifeValue)
    {
        if(playerLives < 3)
        {
            playerLives += lifeValue;
            for(int i = 0; i < lifeValue; i++)
            {
                Instantiate(life, lives.transform);
            }
        }
    }

    public void RemoveLife(int lifeValue)
    {
        if(lives.childCount > 0)
        {
            playerLives -= lifeValue;
            for(int i = 0; i < lifeValue; i++)
            {
                Destroy(lives.GetChild(i).gameObject);
            }
        }
    }

    public void AddScore(int coinValue)
    {
        coinScore += coinValue;
    }

    public void PauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        menuBG.SetActive(true);
        shop.SetActive(false);
    }

    public void OpenShopMenu()
    {
        menuBG.SetActive(false);
        shop.SetActive(true);
    }

    public void CloseShopMenu()
    {
        menuBG.SetActive(true);
        shop.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}
