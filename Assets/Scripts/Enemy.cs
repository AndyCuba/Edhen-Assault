using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Tooltip("FX prefab on enemy")][SerializeField] GameObject deathFX;
    [Tooltip("Object to spawn runtime")][SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 12;
    [SerializeField] int hits = 10;

    ScoreBoard scoreBoard;
    void Start()
    {
        AddNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {
        //print("Particles collided with enemy" + gameObject.name);
        ProcessHit();

        if (hits <= 0)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        scoreBoard.AddScoreHit(scorePerHit);

        hits--;
    }

    private void KillEnemy()
    {
        // Quaternion.identity is a neutral rotation (we dont change it);
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;

        Destroy(gameObject);
    }
}
