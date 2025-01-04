using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] float towerRange = 15f;
    Transform target;


    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistace = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistace < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistace;
            }
        }

        target = closestTarget;

    }

    void AimWeapon()
    {
        float targetDistace = Vector3.Distance(transform.position, target.position);

        weapon.LookAt(target);

        if (targetDistace < towerRange)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        var emmisionModule = projectileParticles.emission;
        emmisionModule.enabled = isActive;
    }
}
