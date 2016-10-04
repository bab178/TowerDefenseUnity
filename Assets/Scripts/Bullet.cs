using UnityEngine;
using System.Collections;
using System;

public class Bullet : MonoBehaviour {

    private int Damage = 1;
    public float Speed = 15f;
    public Transform Target;

    void Update()
    {
        ProjectileMovement();
    }

    void ProjectileMovement()
    {
        float distance = Speed * Time.deltaTime;

        if(Target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = Target.position - transform.localPosition;

        if (direction.magnitude > distance)
        {
            transform.Translate(direction.normalized * distance, Space.World);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5);
        }
        else
        {
            HitEnemy();
        }
    }

    void HitEnemy()
    {
        var enemy = Target.gameObject.GetComponent<Enemy>();
        enemy.TakeDamage(Damage);
        Destroy(gameObject);
    }
}
