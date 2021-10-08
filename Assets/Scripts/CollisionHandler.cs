using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")][SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab on player")][SerializeField] GameObject deathFX;
    void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        Invoke("ReloadScene", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        deathFX.SetActive(true);
        gameObject.SendMessage("OnPlayerDeath");
    }

    private void ReloadScene() // String referenced
    {
        SceneManager.LoadScene(1);
    }
}
