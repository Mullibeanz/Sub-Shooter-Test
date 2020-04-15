using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //config perameters
    [Header("Player Stats")]
    [SerializeField] int health = 3;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float playerPaddingX = 0.665f;
    [SerializeField] float playerPaddingY = 1.28f;

    [Header("Projectile")]
    [SerializeField] GameObject playerMissile;
    [SerializeField] float missileSpeed = 10f;
    [SerializeField] float missileFiringPeriod = 0.5f;
    [SerializeField] GameObject muzzleFlash;

    [Header("Moab")]
    [SerializeField] GameObject playerMoab;
    [SerializeField] float moabSpeed = 10f;
    [SerializeField] float moabFiringPeriod = 1f;
    [SerializeField] int numOfMoabs = 3;

    [Header("Death")]
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
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0.7f;
    [SerializeField] AudioClip hitSFX;
    [SerializeField] [Range(0, 1)] float hitSFXVolume = 0.7f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.7f;
    

    Coroutine firingCoroutine;
    private Shake shake;

    float xMin;
    float xMax;
    float yMin;
    float yMax;


    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();

        //Lost Relic
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load(flashMaterial, typeof(Material)) as Material;
        matDefault = sr.material;

        shake = GameObject.FindGameObjectWithTag("Screenshake").GetComponent<Shake>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        Launch();
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
        }

        //Lost Relic
        else
        {
            Invoke("ResetMaterial", flashLength);
            shake.CamShake();
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            //Missile
            GameObject leftmissile = Instantiate(
            playerMissile,
                transform.position + new Vector3(-0.35f, 1f, 0f),
                Quaternion.identity) as GameObject;
            leftmissile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, missileSpeed);

             GameObject rightmissile = Instantiate(
               playerMissile,
               transform.position + new Vector3(0.35f, 1f, 0f),
               Quaternion.identity) as GameObject;
            rightmissile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, missileSpeed);

            //Muzzle Flash
            GameObject leftFlash = Instantiate(
               muzzleFlash,
               transform.position + new Vector3(-0.35f, 1f, -1f),
               Quaternion.identity) as GameObject;
            leftFlash.GetComponent<Rigidbody2D>().velocity = new Vector2(0, missileSpeed / 2);
            Destroy(leftFlash, missileFiringPeriod);

            GameObject rightFlash = Instantiate(
               muzzleFlash,
               transform.position + new Vector3(0.35f, 1f, -1f),
               Quaternion.identity) as GameObject;
            rightFlash.GetComponent<Rigidbody2D>().velocity = new Vector2(0, missileSpeed / 2);
            Destroy(rightFlash, missileFiringPeriod);

            //ShootSFX
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);

            yield return new WaitForSeconds(missileFiringPeriod);
        }
    }

    //Moab Experiment
    private void Launch()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (numOfMoabs >= 1)
            {
                StartCoroutine(LaunchMoab());
                numOfMoabs--;
            }
        }
    }

    IEnumerator LaunchMoab()
    {
        GameObject moab = Instantiate(playerMoab,
                transform.position + new Vector3(0f, 1f, 0f),
                Quaternion.identity) as GameObject;
        moab.GetComponent<Rigidbody2D>().velocity = new Vector2(0, moabSpeed);

        yield return new WaitForSeconds(moabFiringPeriod);
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position + new Vector3(0f, 0f, 2f), transform.rotation);
        GameObject shrapnel = Instantiate(shrapnelVFX, transform.position + new Vector3(0f, 0f, 1f), transform.rotation);
        Destroy(explosion, durationOfExplosion);
        Destroy(shrapnel, durationOfShrapnel);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        FindObjectOfType<SceneLoader>().LoadGameOverScene();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + playerPaddingX;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - playerPaddingX;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + playerPaddingY;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - playerPaddingY;
    }

    //Lost Relic
    void ResetMaterial()
    {
        sr.material = matDefault;
    }

    public int GetHealth()
    {
        return health;
    }

    public void ResetHealth()
    {
        health = 3;
    }

    public int GetnumberOfMoabs()
    {
        return numOfMoabs;
    }

    public void AddExtraMoab()
    {
        numOfMoabs++;
    }
}

