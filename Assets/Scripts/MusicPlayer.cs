using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private void Awake() {

        this.SetupSingleton();
    }

    private void SetupSingleton() {

        var thisType = this.GetType();

        if (FindObjectsOfType(thisType).Length > 1) {

            Destroy(this.gameObject);
        }
        else {

            DontDestroyOnLoad(this.gameObject);
        }
    }
}
