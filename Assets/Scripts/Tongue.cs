using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<InsectMover>())
        {
            FindObjectOfType<GameCanvas>().score += 100;
            FindObjectOfType<GameCanvas>().Hit(collision.transform.position);

            collision.GetComponent<InsectMover>().moving = false;
            collision.transform.parent = this.transform;
            Destroy(collision.gameObject, 1f);
        }
    }
}
