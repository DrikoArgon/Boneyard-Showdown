using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : AgressiveEnemy
{
    public GameObject projectilePrefab;
    public float attackRange = 1.5f;

    public Transform projectileSpawnPointUp;
    public Transform projectileSpawnPointRight;
    public Transform projectileSpawnPointDown;

    protected virtual void FireProjectile(Transform target) {


        if (Mathf.Abs(target.position.x - transform.position.x) < 0.2 && Mathf.Abs(target.position.y - transform.position.y) < attackRange) {
            attacking = true;

            animator.Play("Attack");

            if (target.position.y > transform.position.y) {
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPointUp.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                EnemyProjectile projectileVariables = projectile.GetComponent<EnemyProjectile>();
                projectileVariables.direction = 1;
            } else {
                GameObject knife = Instantiate(projectilePrefab, projectileSpawnPointDown.position, Quaternion.identity);
                EnemyProjectile projectileVariables = knife.GetComponent<EnemyProjectile>();
                projectileVariables.direction = 3;
            }

        }

        if (Mathf.Abs(target.position.x - transform.position.x) < attackRange && Mathf.Abs(target.position.y - transform.position.y) < 0.2) {
            attacking = true;
            animator.Play("Attack");


            if (target.position.x > transform.position.x) {
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPointRight.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                EnemyProjectile knifeVariables = projectile.GetComponent<EnemyProjectile>();
                knifeVariables.direction = 2;
            } else {
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPointRight.position, Quaternion.Euler(new Vector3(0, 0, 270)));
                EnemyProjectile projectileVariables = projectile.GetComponent<EnemyProjectile>();
                projectileVariables.direction = 4;
            }

        }

    }

}
