using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public Vector3 enemyPos;
    public EnemyData enemyData;
    public GameObject target;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    public void SetTarget(GameObject tar)
    {
        target = tar;
    }

    public void Initialize(Sprite sprite, GameObject tar)
    {
        spriteRenderer.sprite = sprite;
        target = tar;
    }
}
