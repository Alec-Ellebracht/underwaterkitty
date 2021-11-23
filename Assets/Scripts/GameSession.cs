using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    /*****************************************************************************
    *
    * State
    *
    ******************************************************************************/

    public int score = 0;

    /*****************************************************************************
    *
    * Lifecycle Hooks
    *
    ******************************************************************************/

    private void Awake() {
        
        this.SetupSingleton();
    }

    /*****************************************************************************
    *
    * Public Methods
    *
    ******************************************************************************/

    // getter for the score
    public int GetScore () {

        return this.score;
    }

    // increments/decrements the players score
    public void AddToScore (int scoreVal) {

        this.score += scoreVal;
    }
    
    // resets/destroys all session stats
    public void ResetGame () {

        Destroy( this.gameObject );
    }

    /*****************************************************************************
    *
    * Utils && such
    *
    ******************************************************************************/

    // ensures only a single instance of the game session
    private void SetupSingleton() {

        var sessionCount =  
            FindObjectsOfType<GameSession>().Length;

        if (sessionCount > 1) {

            Destroy(gameObject);
        }
        else {

            DontDestroyOnLoad(gameObject);
        }
    }
}
