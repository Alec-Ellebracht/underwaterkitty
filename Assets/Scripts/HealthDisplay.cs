using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    // state
    Text healthText;
    Player player;

    private void Start () {

        this.healthText = GetComponent<Text>();
        this.player = FindObjectOfType<Player>();
    }

    private void Update () {

        if (this.player) {

            this.healthText.text = 
                this.player.GetHealth().ToString();
        }
        else {
            this.healthText.text = "0";
        }
    }
}
