using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrayController : MonoBehaviour
{

    public int currentEnemyCount = 0;
    private Vector3 arrayOrigin;
    [SerializeField]private GameObject enemyPrefab;
    [SerializeField]private GameplayManagerData gameplayManagerData;

    private int enemySpawnWidth = 4;
    private int enemySpawnHeight = 1;
    private int enemySpawnOffset = 1;

    public void StartArray(){

        currentEnemyCount = 0;

        arrayOrigin = gameObject.transform.position;

        if(gameplayManagerData.playerWave < 5)
        {
            enemySpawnWidth = 2;
        }
        if(gameplayManagerData.playerWave >= 5 && gameplayManagerData.playerWave < 10)
        {
            enemySpawnWidth = 3;
        }
        if(gameplayManagerData.playerWave >= 10 && gameplayManagerData.playerWave < 15)
        {
            enemySpawnWidth = 4;
        }

        for(int x = -enemySpawnWidth; x < enemySpawnWidth + 1; x++){

            for(int z = -enemySpawnHeight; z < enemySpawnHeight + 1; z++){

                currentEnemyCount += 1;

                Vector3 enemyPosition = new Vector3(arrayOrigin.x + (x * enemySpawnOffset), arrayOrigin.y + (z * enemySpawnOffset), 0);

                GameObject enemySpawn = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
                EnemyController enemyController = enemySpawn.GetComponent<EnemyController>();

                enemySpawn.name = currentEnemyCount.ToString();

                //if(z == 0 && gameplayManagerData.playerWave > 9){
                    //enemyController.enemyType = 1;
                //}

                if(z == 0 && x == 0 && gameplayManagerData.playerWave > 4){
                    enemyController.enemyName = 1;
                }

                if((x == -enemySpawnWidth || x == enemySpawnWidth) && z == 0 && (gameplayManagerData.playerWave == 10 || gameplayManagerData.playerWave == 15)){
                    enemyController.enemyName = 2;
                }

            }

        }

    }

}
