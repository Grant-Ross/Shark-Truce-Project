using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBuns : MonoBehaviour
{
    [SerializeField] private Collider2D endingZone;

    private void OnTriggerEnter(Collider other)
    {
        print("hi");
    }
}
