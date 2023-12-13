using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 1f;
    private float destroyDelay = 2f;
    private int numberShakes = 3;
    [SerializeField] private Rigidbody2D rb;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }
    private IEnumerator Fall()
    {
        //bucle para agitar los bloques antes de caer
        for (int i = 0; i < numberShakes; i++)
        {
            transform.position = new Vector2(0.1f + transform.position.x, 0);
            yield return new WaitForSeconds(0.1f);

            transform.position = new Vector2(transform.position.x -0.1f, 0);
            yield return new WaitForSeconds(0.1f);

            transform.position = new Vector2(transform.position.x - 0.1f, 0);
            yield return new WaitForSeconds(0.1f);

            transform.position = new Vector2(0.1f + transform.position.x, 0);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }
}
