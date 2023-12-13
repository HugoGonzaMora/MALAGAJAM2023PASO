using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDMG : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //TO DO, call the Player TakeDamage method
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(); ;
        }
    }
}
