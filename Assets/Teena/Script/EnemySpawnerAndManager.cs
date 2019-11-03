using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnWave
{
    public EnemyListData enemyList;
    public float SpawnTimer;
    public int EnemyPerCycle;
    public int NumOfCycles;
}

public class EnemySpawnerAndManager : MonoBehaviour
{
    BaseEnemy[] SpawnedEnemies;
    public SpawnWave[] spawnWaves;
    public IconSpriteListData iconSpriteList;
    public EnemyListData enemyList;
    public GameObject target;
    public float SpawnTimer;
    float defSpawnTimer;
    bool AllowSpawn;
    public BoxCollider2D SpawnArea;
    public int EnemyPerCycle;
    public int NumOfCycles;
    int CycleCounter;
    public EnemySpawnerAndManager[] managersToObserve;
    public bool SpawnBasedOnObservedFinished;
    public bool FinishedCycle = false;
    public bool FinishedLocalCycle = false;
    public int FinalWaveInt = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetWave(0);
    }

    void SetWave(int waveNum)
    {
        if(waveNum < spawnWaves.Length)
        {
            defSpawnTimer = spawnWaves[waveNum].SpawnTimer;
            EnemyPerCycle = spawnWaves[waveNum].EnemyPerCycle;
            NumOfCycles = spawnWaves[waveNum].NumOfCycles;
            enemyList = spawnWaves[waveNum].enemyList;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(FinishedCycle == false)
        {
            if (SpawnBasedOnObservedFinished == true)
            {
                if(CheckObserved() == true)
                {
                    AllowSpawn = true;
                }
            }
            else
            {
                AllowSpawn = true;
            }

            if(AllowSpawn == true)
            {
                SpawnTimer -= Time.deltaTime;
                if (SpawnTimer <= 0.0f)
                {
                    for (int i = 0; i < EnemyPerCycle; i++)
                    {
                        SpawnEnemy();
                    }
                    SpawnTimer = defSpawnTimer;
                    CycleCounter++;
                    if (CycleCounter >= NumOfCycles)
                    {
                        FinishedCycle = true;
                    }
                }
            }
        }
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3
        (
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    void SpawnEnemy()
    {
        Object enemyObject = enemyList.GetRandomEnemy();
        Vector3 spawnPosition = RandomPointInBounds(SpawnArea.bounds);
        Sprite spawnedSprite = iconSpriteList.GetRandomSprite();
        BaseEnemy spawnedEnemy = (BaseEnemy)Instantiate(enemyObject, spawnPosition, Quaternion.identity);
        spawnedEnemy.sprite = spawnedSprite;
    }

    bool CheckObserved()
    {
        for(int i = 0; i < managersToObserve.Length; i++)
        {
            if(managersToObserve[i].FinishedCycle == false)
            {
                return false;
            }
        }

        return true;
    }
}