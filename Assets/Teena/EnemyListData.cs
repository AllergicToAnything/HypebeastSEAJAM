using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyListData", menuName = "ScriptableObjects/EnemyListData", order = 1)]
public class EnemyListData : ScriptableObject
{
    public BaseEnemy[] enemyList;
    // Start is called before the first frame update
    public BaseEnemy GetRandomEnemy()
    {
        int enemyID = Random.Range(0, enemyList.Length);
        return enemyList[enemyID];
    }
}
