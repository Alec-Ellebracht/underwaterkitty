using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    // configs
    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float fireRate = 1f;

    [Header("Sounds")]
    [SerializeField] AudioClip[] firingSounds;

    // state
    Player attachedPlayer;
    Vector3 playerToWeaponVector;
    GameObject projectileSpawn;

    // cached refs
    AudioSource audioSource;
    Coroutine firing;

    private void Start () {

        this.audioSource = 
            GetComponent<AudioSource>();

        this.projectileSpawn = 
            this.gameObject.transform.Find("ProjectileSpawn").gameObject;
    }

    private void Update () {

        if (this.attachedPlayer) {
            this.transform.position = 
                (this.attachedPlayer.transform.position + this.playerToWeaponVector);
        }
    }

    public void BindPlayer (Player attachedPlayer) {

        this.attachedPlayer = attachedPlayer;

        this.playerToWeaponVector = 
            (this.transform.position - attachedPlayer.transform.position);
    }

    public void DoFire () {

        this.firing = StartCoroutine( this.DoContinuousFire() );
    }

    private IEnumerator DoContinuousFire () {
        while (true) {

            GameObject projectile =
                Instantiate(
                    this.projectilePrefab, 
                    this.projectileSpawn.transform.position, 
                    Quaternion.identity
                ) as GameObject;

            var projectileBody = 
                projectile.GetComponent<Rigidbody2D>();

            projectileBody.velocity = 
                new Vector2(projectileSpeed, 0);

            this.audioSource.PlayOneShot( this.firingSounds[0] );

            yield return new WaitForSeconds(this.fireRate);
        }
    }

    public void DoStopFire () {

        StopCoroutine( this.firing );
    }

    public void OnDestroy () {
        
        Destroy(this.gameObject);
    }
}
