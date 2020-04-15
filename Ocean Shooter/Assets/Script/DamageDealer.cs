using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] int damage = 1;

    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;


    public int GetDamage()
    {
        return damage;
    }

    public void Hit()

    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
    }
}
