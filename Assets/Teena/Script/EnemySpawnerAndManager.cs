using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerAndManager : MonoBehaviour
{
    BaseEnemy[] SpawnedEnemies;
    public IconSpriteListData iconSpriteList;
    public EnemyListData enemyList;
    public GameObject target;
    public float SpawnTimer;
    float defSpawnTimer;
    public bool AllowSpawn;
    public BoxCollider2D SpawnArea;
    public int EnemyPerCycle;

    // Start is called before the first frame update
    void Start()
    {
        defSpawnTimer = SpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer -= Time.deltaTime;
        if(SpawnTimer <= 0.0f)
        {
            for(int i = 0; i < EnemyPerCycle; i++)
            {
                SpawnEnemy();
            }
            SpawnTimer = defSpawnTimer;
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
}