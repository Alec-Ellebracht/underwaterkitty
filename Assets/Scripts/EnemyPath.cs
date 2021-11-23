using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {

    // configs
    [SerializeField] WaveConfig waveConfig;

    // props
    List<Transform> waypoints;

    // state
    int currentWaypoint = 0;

    /********************************************************************************************
    *
    * public methods
    *
    ********************************************************************************************/

    public void SetWaveConfig (WaveConfig waveConfig) {

        this.waveConfig = waveConfig;
    }

    /********************************************************************************************
    *
    * private methods
    *
    ********************************************************************************************/

    private void Start () {

        this.waypoints = 
            this.waveConfig.GetWaveWaypoints();

        var startingPos = 
            this.waypoints[this.currentWaypoint].transform.position;

        this.transform.position = startingPos;

        currentWaypoint++;
    }

    private void Update () {

        this.DoMove();
    }

    private void DoMove () {

        int totalWaypoints = this.waypoints.Count;

        var moveSpeed = 
            this.waveConfig.GetMoveSpeed();

        if (this.currentWaypoint + 1 <= totalWaypoints) {

            var currentPos = 
                this.transform.position;

            var targetPos = 
                this.waypoints[this.currentWaypoint].transform.position;

            var moveSpace = 
                (moveSpeed * Time.deltaTime);

            this.transform.position = 
                Vector2.MoveTowards( currentPos, targetPos, moveSpace );

            if (this.transform.position == targetPos) {
                currentWaypoint++;
            }
        }
        else {
            
            Destroy( gameObject );
        }
    }
}
