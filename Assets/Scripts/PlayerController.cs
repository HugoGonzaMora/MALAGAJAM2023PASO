using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Health")]
    public float health = 10;
    private bool takeDamage = true;
    [HideInInspector] public int powerUps = 0;

    [Header("Blink")]
    public float blinkSpeed = 0.15f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = transform.Find("CharacterPlayer").GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Manage player damage 
    /// </summary>
    public void TakeDamage()
    {

        if (takeDamage)
        {

            //play sound
            // AudioManager.instance.PlayDamageSound();

            //reduce health
            health--;


            StartCoroutine(BlinkSprite(4));

            if (health <= 0)
            {
                //To do, GameManager loseGame
                GameManager.gameManager.LooseGame();
            }
        }
    }

    private IEnumerator BlinkSprite(int blinkTimes)
    {
        takeDamage = false;
        do
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(blinkSpeed);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(blinkSpeed);
            blinkTimes--;
        }
        while (blinkTimes > 0);

        takeDamage = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       /* if (collision.CompareTag("PowerUp"))
        {
            //delete gameobject PowerUp from the hierarchy game
            Destroy(collision.gameObject);

            //play pwrUp sound
            //AudioManager.instance.PlayPWRUPSound();

            //Increment number of powerups collected
            powerUps++;

            if (powerUps >= GameManager.gameManager.powerUpsCount)
            {
                GameManager.gameManager.WinGame();
            }
        }*/
    }

}
