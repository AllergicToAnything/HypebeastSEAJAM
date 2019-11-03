using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.SoundManagerNamespace;

public class BaseEnemy : MonoBehaviour
{
    public Vector3 enemyPos;
    public EnemyData enemyData;
    public GameObject target;
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;
    public AudioClip[] clips;
    public AudioClip deathClip;
    public AudioSource source;
    public float DeathTimer = 12.0f;
    protected bool IsDying = false;
    bool PlayedDeathAudio = false;
    bool Hitted = false;
    
    public void RandomizeClip()
    {
        if(Hitted == false)
        {
            int clipID = Random.Range(0, clips.Length);
            SoundManager.PlayOneShotSound(source, clips[clipID]);
            Hitted = true;
        }
    }

    protected void DoDead()
    {
        spriteRenderer.sprite = null;
        DeathTimer -= Time.deltaTime;
        if(PlayedDeathAudio == false)
        {
            SoundManager.PlayOneShotSound(source, deathClip);
            PlayedDeathAudio = true;
        }
        if(DeathTimer <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
