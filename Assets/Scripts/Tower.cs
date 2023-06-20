using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange = 10f;
    [SerializeField] ParticleSystem projectile;

    Transform targetEnemy;
    public BlockNeutral baseBlock;

    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy) 
        {
            objectToPan.LookAt(targetEnemy);
            FireAtEnemy();
        }
        else 
        {
            Shoot(false);
        }
    }

    private void SetTargetEnemy()
    {
        var allEnemies = FindObjectsOfType<EnemyDamage>();
        if (allEnemies.Length == 0) 
        {
            return;
        }

        Transform closetEnemy = allEnemies[0].transform;

        foreach(EnemyDamage enemy in allEnemies) 
        {
            closetEnemy = GetCloset(closetEnemy, enemy.transform);
        }

        targetEnemy = closetEnemy;
    }

    private Transform GetCloset(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformB.position);

        if (distToA < distToB) 
        {
            return transformA;
        }

        return transformB;
    }

    private void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if (distanceToEnemy <= attackRange) 
        {
            Shoot(true);
        }
        else 
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        var emissionModule = projectile.emission;
        emissionModule.enabled = isActive;
    }
}
