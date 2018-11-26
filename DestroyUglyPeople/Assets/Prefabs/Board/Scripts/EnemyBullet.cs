using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    [SerializeField] float damage = 10f;
    [SerializeField] GameObject hitParticle;

    public float Damage()
    {
        return damage;
    }

    public void Hit()
    {
        GameObject hit = Instantiate(hitParticle, transform.position, Quaternion.identity) as GameObject;
        Destroy(hit, 1f);
        Destroy(gameObject);
    }
}
