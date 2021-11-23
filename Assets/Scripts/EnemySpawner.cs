using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] int startingWave = 0;
    [SerializeField] bool loop = false;
    [SerializeField] List<WaveConfig> waveConfigs;

    private IEnumerator Start () {
        do {

            yield return StartCoroutine( this.SpawnAllWaves() );
        }
        while (loop);
    }

    private IEnumerator SpawnAllWaves () {

        Debug.Log("spawning waves..");

        // go through all the waves and spawn the badies
        for (int i = this.startingWave; i < this.waveConfigs.Count; i++) {

            Debug.Log("wave "+i+"..");

            var wave = this.waveConfigs[i];

            yield return StartCoroutine( this.SpawnAllEnemies(wave) );
        }
    }

    private IEnumerator SpawnAllEnemies (WaveConfig wave) {

        int totalEnemies = wave.GetNumEnemies();
        var enemyPrefab = wave.GetEnemyPrefab();
        var waypoints = wave.GetWaveWaypoints();
        var waitTime = wave.GetSpawnSpeed();

        Debug.Log("Spawing " +totalEnemies+ " enemies, at a " +waitTime+ " speed");

        for (int i = 0; i < totalEnemies; i++) {

            var newEnemy = Instantiate(
                enemyPrefab,
                waypoints[0].position,
                Quaternion.identity // for rotation
            );

            var enemyPath = 
                newEnemy.GetComponent<EnemyPath>();
                
            enemyPath.SetWaveConfig( wave );

            yield return new WaitForSeconds( waitTime );
        }
    }
}
