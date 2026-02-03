using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum Team { Blue, Red }

    [Header("Stats")]
    public Team team;
    public float hp;
    public float maxHp;
    public float attackDamage;
    public float attackRange;
    public float attackRate; // 공격 속도 (초 단위)

    protected float lastAttackTime;

    public bool IsDead => hp <= 0;

    public virtual void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0) Die();
    }

    protected virtual void Die()
    {
        // 사망 애니메이션 또는 이펙트 처리
        Destroy(gameObject);
    }
}
