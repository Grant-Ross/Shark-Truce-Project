using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    [SerializeField] public GameObject deathObject;
    
    private bool _broken = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile") && !_broken)
        {
            _broken = true;
            Break();
        }
    }

    private void Break()
    {
        Instantiate(deathObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
