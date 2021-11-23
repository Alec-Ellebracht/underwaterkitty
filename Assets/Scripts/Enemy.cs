using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // configs
    [Header("Enemy Config")]
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;

    [Header("Projectile Config")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 2f;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;

    [Header("Death Config")]
    [SerializeField] AudioClip[] deathSounds;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float explosionDuration = 2f;

    [Header("Score Config")]
    [SerializeField] int pointsForDeath = 10;

    // cached refs


    private void Start () {

        this.cachedGameSession = 
            FindObjectOfType<GameSession>();

        this.shotCounter = 
            Random.Range(this.minTimeBetweenShots, this.maxTimeBetweenShots);
    }

    private void Update () {

        this.CountAndShoot();
    }

    private void CountAndShoot() {

        this.shotCounter -= Time.deltaTime;

        if (this.shotCounter <= 0) {

            this.DoFire();

            this.shotCounter = 
                Random.Range(this.minTimeBetweenShots, this.maxTimeBetweenShots);
        }
    }

    // fires the weapon
    private void DoFire () {

        GameObject projectile =
            Instantiate(
                projectilePrefab, 
                transform.position, 
                Quaternion.identity
            ) as GameObject;

        var projectileBody = 
            projectile.GetComponent<Rigidbody2D>();

        projectileBody.velocity = 
            new Vector2(-projectileSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) {

        DamageDealer damageDealer = 
            other.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) { return; }

        this.ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer) {

        this.health -= damageDealer.GetDamage();

        if (this.health <= 0) {
            this.OnDeath();
        }
    }

    private void OnDeath() {

        Destroy(this.gameObject);

        AudioClip deathSound = 
            this.SelectRandomSound( this.deathSounds );

        AudioSource.PlayClipAtPoint(
            deathSound, 
            Camera.main.transform.position
        );

        GameObject explosion = 
            Instantiate (
                deathVFX, 
                transform.position, 
                transform.rotation
            );

        Destroy(explosion, explosionDuration);

        this.gameSession.AddToScore( this.pointsForDeath );
    }

    private AudioClip SelectRandomSound(AudioClip[] sounds) {

        // choose a random sound
        var range = UnityEngine.Random.Range(0, sounds.Length);
        return sounds[range];
    }

    GameSession cachedGameSession;
    private GameSession gameSession {
        get {
            if (!this.cachedGameSession) {

                this.cachedGameSession = 
                    FindObjectOfType<GameSession>();
            }
            return this.cachedGameSession;
        }
    }
}

