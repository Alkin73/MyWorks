using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public LayerMask projectileLayer;
    private new ParticleSystem particleSystem;

    private void OnParticleCollision(GameObject other)
    {
        if (projectileLayer.Contains(other.layer))
        {
            Debug.Log("boom headshot");
            GameManager.instance.ReduceTimerForBonus(5);
            particleSystem = GetComponentInChildren<ParticleSystem>();
            GetHit();
        }
    }

    public void GetHit()
    {
        particleSystem.Play();
        gameObject.SetActive(false);
    }
}
