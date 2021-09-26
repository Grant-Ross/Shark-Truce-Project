using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    [SerializeField] private GameObject deathObject;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Break();
        }
    }

    private void Break()
    {
        Instantiate(deathObject);
        Destroy(gameObject);
    }
}
