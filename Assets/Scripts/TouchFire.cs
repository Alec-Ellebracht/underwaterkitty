using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchFire : MonoBehaviour {

    Player mainPlayer;

    // Start is called before the first frame update
    void Start() {

        this.mainPlayer = 
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
    }

    public void OnClick () {

        this.mainPlayer.DoTouchFire();
    }

    public void OffClick () {

        this.mainPlayer.DoTouchStopFire();
    }
}
