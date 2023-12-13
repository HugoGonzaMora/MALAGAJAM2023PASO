using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType
    {
        Scorpion,
        Slime
    }
    //Enemy Type
    public EnemyType enemyType = EnemyType.Scorpion;

    //Components
    private Transform groundDetection;
    private SpriteRenderer spriteRenderer;

    private GameObject[] tag;

    //Ground check
    private LayerMask layer;

    //Movement
    public Vector2 direction = Vector2.right;
    public float moveSpeed = 1;

    private void Awake()
    {
        groundDetection = transform.GetChild(0);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        layer = LayerMask.GetMask("Suelo");
    }
    /// <summary>
    /// Detect if the enemy touch the floor 
    /// </summary>
    /// <returns></returns>
    private bool GroundDetection()
    {
        return Physics2D.Raycast(groundDetection.position, Vector2.down, 1f, layer);
    }

    /// <summary>
    /// Detect if the enemy touch the ceiling
    /// </summary>
    /// <returns></returns>
    private bool CeilingDetection()
    {
        return Physics2D.Raycast(groundDetection.position, Vector2.up, 1f, layer);
    }

    private bool WallDetection()
    {
        Vector2 direction = Vector2.zero;

        if (spriteRenderer.flipX)
        {
            direction = Vector2.right;
        }
        else if (!spriteRenderer.flipX)
        {
            direction = Vector2.left;
        }

        

        return Physics2D.Raycast(groundDetection.position, direction, 0.1f, layer);
    }

    private void Update()
    {
        EnemyMove();
    }

    private void EnemyMove()
    {
        //CRAB Detection and change direction
        if (enemyType.Equals(EnemyType.Scorpion))
        {
            //If not detect ground or detect wall change direction
            if (!GroundDetection() || WallDetection())
            {
                direction = -direction;
                spriteRenderer.flipX = !spriteRenderer.flipX;
                //Invert the groundDetection X point
                groundDetection.transform.localPosition = new Vector3(-groundDetection.transform.localPosition.x, groundDetection.transform.localPosition.y, groundDetection.localPosition.z);
            }
        }
        if (enemyType.Equals(EnemyType.Slime))
        {
            //If not detect ground or detect wall change direction
            if (!GroundDetection() || WallDetection())
            {
                direction = -direction;
                spriteRenderer.flipX = !spriteRenderer.flipX;
                //Invert the groundDetection X point
                groundDetection.transform.localPosition = new Vector3(-groundDetection.transform.localPosition.x, groundDetection.transform.localPosition.y, groundDetection.localPosition.z);
            }
        }
    }//EnemyMove

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //TO DO, call the Player TakeDamage method
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(); ;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        tag = GameObject.FindGameObjectsWithTag("Debil");
        print("aaaaaaaaaaaaaaaaaa");
        if (other.gameObject.CompareTag("Debil"))
        {
            Destroy(gameObject);
        }
    }
}
