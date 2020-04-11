using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("object entered");
        if (collision.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            Debug.Log("bullet destroyed");
        }
    }

}
