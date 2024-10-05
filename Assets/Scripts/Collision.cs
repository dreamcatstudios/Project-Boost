using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    float levelInvokeDelay = 1f;

    public bool isCollisionHandlingEnabled = true;

    [SerializeField]
    AudioClip success;

    [SerializeField]
    AudioClip crash;

    [SerializeField]
    ParticleSystem sucessParticles;

    [SerializeField]
    ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        // Initialize the AudioSource component
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on this GameObject.");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isCollisionHandlingEnabled)
            return; // Return early if collision handling is disabled
        if (!isTransitioning)
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("This thing is friendly");
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                case "Fuel":
                    Debug.Log("This thing is fuel");
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;
        sucessParticles.Play();

        if (audioSource != null)
        {
            audioSource.PlayOneShot(success);
        }

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", levelInvokeDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        crashParticles.Play();

        if (audioSource != null)
        {
            audioSource.PlayOneShot(crash);
        }

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelInvokeDelay);
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    // If you want to reload the current level, you can use this method
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
