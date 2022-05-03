using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] AudioClip levelComplete;
    [SerializeField] AudioClip pickUp;
    [SerializeField] AudioClip crashSound;
    [SerializeField] float delay = 3f;
    [SerializeField] float finish = 2f;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem xplosionParticles;

    AudioSource audioSource;
    bool inTransition = false;
    bool collisionDisabled = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        cheatCodeActivation();
    }
    void cheatCodes()
    {
        LoadNextLevel();
    }


    void cheatCodeActivation()
    {
        if (Input.GetKey(KeyCode.L))
        {
            cheatCodes(); // load next level!
        }
        if (Input.GetKey(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggle collision!
        }
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        audioSource.Stop();
        audioSource.PlayOneShot(levelComplete);
        successParticles.Play();
        inTransition = true;
        GetComponent<Movement>().enabled = false;
        StartCoroutine(NextLevelAfterDelay(finish));
        IEnumerator NextLevelAfterDelay(float finish)
        {
            yield return new WaitForSeconds(finish);
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
    void LevelRestart()
    {
        // Created int for current scene to Scenemanager use loading multiple levels!
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Created a coroutine for 3 second delay after restarting level (after crashing)!!
        Debug.Log("You have crashed and died horribly! Restarting Level in 3 seconds!");
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        inTransition = true;
        audioSource.PlayOneShot(crashSound);
        xplosionParticles.Play();
        StartCoroutine(LoadLevelAfterDelay(delay));
        IEnumerator LoadLevelAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(currentSceneIndex);
        }

    }

    void OnCollisionEnter(Collision other)
    {
        // if inTransition is true then return !!
        // if inTransition is not true --> do switch cases!!
        if (inTransition || collisionDisabled) { return; }
        switch (other.gameObject.tag)
        {
            case "Start Line":
                break;

            case "Goal Line":
                LoadNextLevel();
                Debug.Log("Well done, you have reached your destination!");
                break;
            // Upon collision print text and deactivate pickup item!
            case "Package":
                // Working but need to disable collision upon picking up package and need to figure out how to implement my sound upon pickup!
                Debug.Log("You have collected the package! Now deliver it to the destination!");
                // if (other.gameObject.tag == "Package")
                // {
                //     audioSource.Stop();
                //     audioSource.PlayOneShot(pickUp);
                //     other.gameObject.SetActive(false);
                // }
                break;
            default:
                LevelRestart();
                break;
        }
    }
}

