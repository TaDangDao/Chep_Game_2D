using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAttackArea : MonoBehaviour
{
    private ParticleSystem ps;
    private Character owner;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        owner = GetComponentInParent<Character>();
    }
    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
        int num = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

        if (num > 0)
        {
            Player player = FindObjectOfType<Player>();
            player.OnHit(owner.Damage);
            //player.TakeDamage(10 * num); // Gây damage dựa trên số hạt
        }
    }
  
}
