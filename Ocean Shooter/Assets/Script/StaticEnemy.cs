using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    [Header("Enemty Stats")]
    [SerializeField] float health = 1;
    [SerializeField] int scoreValue = 150;

    [Header("Death Explosion")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] GameObject shrapnelVFX;
    [SerializeField] float durationOfShrapnel = 1f;

    [Header("SpriteFlash - Lost Relic")]
    private Material matDefault;
    private Material matWhite;
    [SerializeField] float flashLength = 0.2f;
    [SerializeField] string flashMaterial;
    SpriteRenderer sr;

    [Header("SFX")]
    [SerializeField] AudioClip hitSFX;
    [SerializeField] [Range(0, 1)] float hitSFXVolume = 0.7f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.7f;

    [Header("Power-ups")]
    [SerializeField] GameObject powerUp1;
    [SerializeField] GameObject powerUp2;
    [SerializeField] GameObject powerUp3;
    [SerializeField] int minRandom = 1;
    [SerializeField] int maxRandom = 20;
    [SerializeField] float dropSpeed = 5f;


    private Shake shake;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load(flashMaterial, typeof(Material)) as Material;
        matDefault = sr.material;

        
        shake = GameObject.FindGameObjectWithTag("Screenshake").GetComponent<Shake>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
        AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, hitSFXVolume);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        //Lost Relic
        sr.material = matWhite;

        if (health <= 0)
        {
            Die();

            //Power up test
            DropUpgrade();


            shake.CamShake();
        }

        //Lost Relic
        else
        {
            Invoke("ResetMaterial", flashLength);
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        GameObject shrapnel = Instantiate(shrapnelVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        Destroy(shrapnel, durationOfShrapnel);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }

    //Lost Relic
    void ResetMaterial()
    {
        sr.material = matDefault;
    }

    private void DropUpgrade()
    {
        {
            int randomNum = UnityEngine.Random.Range(minRandom, maxRandom);
            if (randomNum == 1)
            {
                GameObject powerUp = Instantiate(
                    powerUp1, transform.position + new Vector3(0f, 0f, -1f),
                    Quaternion.identity)
                    as GameObject;
                powerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -dropSpeed);
            }
            else if (randomNum == 2)
            {
                GameObject powerUp = Instantiate(
                    powerUp2, transform.position + new Vector3(0f, 0f, -1f),
                    Quaternion.identity)
                    as GameObject;
                powerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -dropSpeed);
            }
            else if (randomNum == 3)
            {
                GameObject powerUp = Instantiate(
                    powerUp3, transform.position + new Vector3(0f, 0f, -1f),
                    Quaternion.identity)
                    as GameObject;
                powerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -dropSpeed);
            }
        }
    }
}
