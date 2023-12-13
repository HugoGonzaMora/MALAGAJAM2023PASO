using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager gameManager;

    //PowerUps in the game
    [HideInInspector] public int powerUpsCount;


    //Panels
    [Header("Panels")]
    public GameObject endPanel;

    private void Awake()
    {
        //Singleton
        gameManager = this;

        //Disable panel on begining
        endPanel.SetActive(false);

        //Restart TimeScale
        Time.timeScale = 1;
    }

    private IEnumerator Start()
    {
        //The start will start after 0.05f
        yield return new WaitForSeconds(0.05F);
        //get all powerups
        powerUpsCount = GameObject.FindGameObjectsWithTag("PowerUp").Length;
    }

    /// <summary>
    /// Make us loose
    /// </summary>
    public void LooseGame()
    {
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "GAME OVER!";

        //Freeze Time
        Time.timeScale = 0;
    }

    /// <summary>
    /// Make us win
    /// </summary>
    public void WinGame()
    {
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "YOU WIN!";

        //Freeze Time
        Time.timeScale = 0;
    }

    /// <summary>
    /// Change to the Scene name
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
