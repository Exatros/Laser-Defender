using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;
    [Header("Shooting")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.3f;
    [SerializeField] float maxTimeBetweenShots = 1f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    [Header("Special Attack")]
    [SerializeField] bool specialAttack = false;
    [SerializeField] float minReloadTimeForSpecialAttack = 2f;
    [SerializeField] float maxReloadTimeForSpecialAttack = 2f;
    [SerializeField] int numberOfSpecialProjetiles = 10;
    float currentSpecialProjetiles =0 ;
    [Header("VFX effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [Header("Audio")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.7f;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0.25f;


    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        currentSpecialProjetiles = numberOfSpecialProjetiles;
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.fixedDeltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            if (!specialAttack)
            {
                shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            }
            else
            {
                if (currentSpecialProjetiles > 0)
                {
                    shotCounter = 0.15f;
                    currentSpecialProjetiles--;
                }
                else
                {
                    shotCounter = Random.Range(minReloadTimeForSpecialAttack, maxReloadTimeForSpecialAttack);
                    currentSpecialProjetiles = numberOfSpecialProjetiles;
                }
            }

        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
            projectile,
            transform.position,
            Quaternion.identity
            ) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            DeathVFX();
            return;
        }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        damageDealer.Hit();
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        FindObjectOfType<GameSession>().DecraseEnemyCounter();
        Destroy(gameObject);
        DeathVFX();
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }

    private void DeathVFX()
    {
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion * explosion.GetComponent<ParticleSystem>().main.duration);
    }
}
