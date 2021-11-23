using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    
    [SerializeField] float sceneLoadDelay = 3f;

    public void LoadNextScene () {

        int currentSceneIndex = 
            SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene () {

        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver () {

        StartCoroutine( this.WaitAndLoad() );
    }

    public void QuitGame () {

        Application.Quit();
    }

    private IEnumerator WaitAndLoad() {

        yield return new WaitForSeconds(this.sceneLoadDelay);
        SceneManager.LoadScene("GameOver");
    }
}
