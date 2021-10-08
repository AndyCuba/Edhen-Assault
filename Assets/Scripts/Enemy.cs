using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Tooltip("FX prefab on enemy")][SerializeField] GameObject deathFX;
    [Tooltip("Object to spawn runtime")][SerializeField] Transform parent;
    void Start()
    {
        AddNonTriggerBoxCollider();
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {
        //print("Particles collided with enemy" + gameObject.name);

        // Quaternion.identity is a neutral rotation (we dont change it);

        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;

        Destroy(gameObject);
    }
}
