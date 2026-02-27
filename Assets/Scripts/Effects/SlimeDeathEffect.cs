using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeathEffect : MonoBehaviour
{
    public GameObject _miniSlimePrefab; // Prefab for the mini slime to spawn
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(_miniSlimePrefab, transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        // Optional: Add any animation or effect logic here, then destroy the effect after a short duration
        Destroy(gameObject, 0.5f); // Destroy the effect after 0.5 seconds
    }


}
