using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ColisionHandle : MonoBehaviour
{
    [SerializeField] float delayTime = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            NextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollidable) {return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Starting the level!");
                break;

            case "Fuel":
                Debug.Log("Fuel recharged!");
                break;

            case "Finish":                
                StartLandingSequence();
                break;

            default:                
                StartCrashSequence();
                break;

        }
    }

    void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayTime);
    }

    void StartLandingSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayTime);
    }
    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void NextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }
    
}
