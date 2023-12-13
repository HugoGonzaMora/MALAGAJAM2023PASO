using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
  //Level Timer
  private float timer = 0;
  private float minutes = 0;
  private float seconds = 0;

    //Component references
    [Header("HUD Text")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI pointsText;

    private void Update()
    {
        timer += Time.deltaTime;
        seconds = (int)timer % 60;
        minutes = (int)timer / 60;
    }


    private void OnGUI()
    {
        timeText.text = "Timer " + string.Format("{0:00}:{1:00}", minutes, seconds);
        healthText.text = "Health " + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health.ToString("00");
        pointsText.text = "Points " + (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().powerUps*100).ToString("0000");
    }
}
