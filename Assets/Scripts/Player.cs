using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    // configs
    [Header("Player Config")]
    [SerializeField] int health = 500;
    [SerializeField] Weapon weapon;

    [Header("Movement Config")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float movePadding = 5f;
    [SerializeField] FixedJoystick fixedJoystick;

    [Header("Sound Config")]
    [SerializeField] AudioClip[] deathSounds;
    [SerializeField] AudioClip[] hitSounds;
    
    // scene boundry vars
    float yMin = 10f;
    float yMax = 10f;
    float xMin = 10f;
    float xMax = 10f;

    // cached refs
    AudioSource audioSource;

    private void Start () {

        this.SetMoveBoundries();

        this.weapon.BindPlayer(this);

        this.audioSource = 
            GetComponent<AudioSource>();
    }

    private void Update () {

        this.DoMove();
        // this.DoFire();
    }

    // public

    public void DoTouchFire () {
        this.weapon.DoFire();
    }

    public void DoTouchStopFire () {
        this.weapon.DoStopFire();
    }

    public int GetHealth () {
        return this.health;
    }

    // private

    // fires the weapon
    private void DoFire () {

        if (Input.GetButtonDown("Fire1")) {

            this.weapon.DoFire();
        }

        else if (Input.GetButtonUp("Fire1")) {

            this.weapon.DoStopFire();
        }
    }

    // moves the player
    private void DoMove () {

        var spaceTime = (Time.deltaTime * moveSpeed);

        var deltaY = 0f;
        var deltaX = 0f;

        if (!this.fixedJoystick) {

            deltaY = Input.GetAxis("Vertical") * spaceTime;
            deltaX = Input.GetAxis("Horizontal") * spaceTime;
        }
        else {

            deltaY = this.fixedJoystick.Vertical * spaceTime;
            deltaX = this.fixedJoystick.Horizontal * spaceTime;
        }

        var nextYPos = Mathf.Clamp(transform.position.y + deltaY, this.yMin, this.yMax);
        var nextXPos = Mathf.Clamp(transform.position.x + deltaX, this.xMin, this.xMax);

        transform.position = new Vector2(nextXPos, nextYPos);
    }

    private void SetMoveBoundries () {

        Camera gameCam = Camera.main;

        this.yMin = 
            gameCam.ViewportToWorldPoint(
                new Vector3(0, 0, 0)).y + this.movePadding;
        this.yMax = 
            gameCam.ViewportToWorldPoint(
                new Vector3(0, 1, 0)).y - this.movePadding;
        this.xMin = 
            gameCam.ViewportToWorldPoint(
                new Vector3(0, 0, 0)).x + this.movePadding;
        this.xMax = 
            gameCam.ViewportToWorldPoint(
                new Vector3(1, 0, 0)).x - this.movePadding;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        DamageDealer damageDealer = 
            other.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) { return; }

        damageDealer.OnHit();

        this.ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer) {

        this.health -= damageDealer.GetDamage();

        if (this.health <= 0) {

            this.OnDeath();
        }
        else {

            AudioClip hitSound = 
                this.SelectRandomSound( this.hitSounds );

            this.audioSource.PlayOneShot( hitSound );
        }
    }

    private void OnDeath () {

        this.weapon.OnDestroy();

        Destroy(this.gameObject);

        AudioClip deathSound = 
            this.SelectRandomSound( this.deathSounds );

        AudioSource.PlayClipAtPoint(
            deathSound, 
            Camera.main.transform.position
        );

        FindObjectOfType<SceneLoader>().LoadGameOver();
    }

    // utils

    private AudioClip SelectRandomSound(AudioClip[] sounds) {

        // choose a random sound
        var range = UnityEngine.Random.Range(0, sounds.Length);
        return sounds[range];
    }
}