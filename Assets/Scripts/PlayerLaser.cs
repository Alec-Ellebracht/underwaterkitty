using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour {

    // configs
    [SerializeField] float moveSpeed = 10f;

    // cached refs
    AudioSource laserAudioSource;

    void Start () {


    }

    // Update is called once per frame
    void Update () {


    }
    
    // private void OnCollisionEnter2D (Collision2D collision) {

    //     // choose a random sound
    //     var range = UnityEngine.Random.Range(0, panSounds.Length);
    //     AudioClip sound = panSounds[range];

    //     this.panAudioSource.PlayOneShot( sound );
    // }
}


