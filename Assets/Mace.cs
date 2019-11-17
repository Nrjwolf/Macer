using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.pa

public class Mace : MonoBehaviour
{
    [SerializeField] private ParticleSystem ParticleSystem;

    private float m_RateOverTime;

    private void Update()
    {
        // m_RateOverTime *= 0.7f;
        // var emission = ParticleSystem.emission;
        // emission.rateOverTime = m_RateOverTime;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        // m_RateOverTime = 20;
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.Kill();
        }

        
    }
}
