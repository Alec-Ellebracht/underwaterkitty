using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    // state
    Text scoreText;
    GameSession gameSession;

    private void Start () {

        this.scoreText = GetComponent<Text>();
        this.gameSession = FindObjectOfType<GameSession>();
    }

    private void Update () {

        if (this.gameSession) {

            this.scoreText.text = 
                this.gameSession.GetScore().ToString();
        }
    }
}
