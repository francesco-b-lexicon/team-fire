using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour

{
    public float attackRange = 10f;
    public bool attacking = false;
    public LayerMask targetLayers;
    void Update()
    {
        if (attacking)
        {
            Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, attackRange, targetLayers);
            foreach (Collider2D target in hitObjects)
            {
                IHittable targetHitBehaviour = target.GetComponent<IHittable>();

                if (targetHitBehaviour != null && targetHitBehaviour.CanBeHit)
                {
                    targetHitBehaviour.Hit();
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
