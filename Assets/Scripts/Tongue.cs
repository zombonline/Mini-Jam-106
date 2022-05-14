using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<InsectMover>())
        {
            Debug.Log("Tongue punched " + collision.name);
            Destroy(collision.gameObject);
        }
    }
}
