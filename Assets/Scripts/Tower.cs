using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Entity
{
    private Entity target;

    void Update()
    {
        if (IsDead) return;

        if (target == null || target.IsDead || Vector3.Distance(transform.position, target.transform.position) > attackRange)
        {
            FindTargetInRange();
        }

        if (target != null)
        {
            Attack();
        }
    }

    void FindTargetInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        float minDistance = float.MaxValue;
        Entity nearest = null;

        foreach (var col in colliders)
        {
            Entity e = col.GetComponent<Entity>();
            if (e != null && e.team != this.team && !e.IsDead)
            {
                float dist = Vector3.Distance(transform.position, e.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    nearest = e;
                }
            }
        }
        target = nearest;
    }

    void Attack()
    {
        if (Time.time >= lastAttackTime + attackRate)
        {
            target.TakeDamage(attackDamage);
            lastAttackTime = Time.time;
        }
    }
}
