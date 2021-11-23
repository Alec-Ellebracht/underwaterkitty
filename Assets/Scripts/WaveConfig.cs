using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemeyWaveConfig")]
public class WaveConfig : ScriptableObject {

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;

    [SerializeField] float spawnSpeed = 0.5f;
    [SerializeField] float spawnRandomizer = 0.3f;
    [SerializeField] int numEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab () { return enemyPrefab; }

    public List<Transform> GetWaveWaypoints () { 

        var waveWaypoints = new List<Transform>();

        foreach (Transform waypoint in this.pathPrefab.transform) {
            waveWaypoints.Add(waypoint);
        }

        return waveWaypoints; 
    }

    public float GetSpawnSpeed () { return this.spawnSpeed; }
    public float GetSpawnRandomizer () { return this.spawnRandomizer; }
    public int GetNumEnemies () { return this.numEnemies; }
    public float GetMoveSpeed () { return this.moveSpeed; }
}
