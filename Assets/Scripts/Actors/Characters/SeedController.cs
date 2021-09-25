using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float speed;

    private const float DeathTime = .8f;
    private float timer;
    
    private void Start()
    {
        rb2D.velocity = transform.up * speed;
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= DeathTime) Kill();
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
