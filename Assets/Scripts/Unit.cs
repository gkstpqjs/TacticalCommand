using UnityEngine;
using System.Linq;

public class Unit : Entity
{
    public float moveSpeed = 2f;
    private Entity target;

    void Update()
    {
        if (IsDead) return;

        // 1. 타겟이 없거나 죽었다면 새로운 타겟 찾기
        if (target == null || target.IsDead)
        {
            FindNearestEnemy();
        }

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            // 2. 사거리 안에 있는지 확인
            if (distance <= attackRange)
            {
                Attack();
            }
            else
            {
                MoveToTarget();
            }
        }
    }

    void FindNearestEnemy()
    {
        // 씬 내의 모든 Entity 중 적 팀인 것들만 필터링
        var enemies = FindObjectsOfType<Entity>()
            .Where(e => e.team != this.team && !e.IsDead)
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position));

        target = enemies.FirstOrDefault();
    }

    void MoveToTarget()
    {
        // 타겟 방향으로 이동
        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // 이동 방향 바라보기
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    void Attack()
    {
        if (Time.time >= lastAttackTime + attackRate)
        {
            Debug.Log($"{gameObject.name}이(가) {target.name}을 공격합니다!");
            target.TakeDamage(attackDamage);
            lastAttackTime = Time.time;
        }
    }
}
