using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAttackArea : MonoBehaviour
{
    private ParticleSystem ps;//Tham chiếu của hệ thống particle
    private Character owner;//Tham chiếu của boss
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        owner = GetComponentInParent<Character>();
    }
    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();// Lấy list các hạt được bắn ra
        int num = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);// lấy số lượng hạt được kích hoạt bởi event enter

        if (num > 0)
        {
            Player player = FindObjectOfType<Player>();// Tìm player
            player.OnHit(owner.Damage);// Gây damage player
        }
    }
  
}
