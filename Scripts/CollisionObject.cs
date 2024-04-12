using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionObject : MonoBehaviour
{

    [SerializeField] float tTime = 1.5f;
    [SerializeField] AudioClip patlamaClip;
    [SerializeField] AudioClip finishClip;

    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource clip;
    bool isTransitioning = false;

    void Start()
    {
        clip = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {

        if (isTransitioning) { return; }
        switch (collision.gameObject.tag){
            case "Start":
                break;
            case "Finish":
                clip.Stop();
                
                StartSuccessSequence();
                successParticle.Play();
                clip.PlayOneShot(finishClip);

                break;
            default:
                
                clip.Stop();
                StartCrushSequence();
                crashParticle.Play();
                clip.PlayOneShot(patlamaClip);

                break;
        }
    }


    void StartSuccessSequence()
    {
        isTransitioning = true;
        GetComponent<MoveRocket>().enabled = false;
        Invoke("LoadNextLevel", tTime);
    }


    void StartCrushSequence()
    {
        isTransitioning = true;
        GetComponent<MoveRocket>().enabled = false;
        Invoke("ReloadLevel", tTime);
    }


    void ReloadLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }


    void LoadNextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneIndex2 = sceneIndex + 1;

        if(sceneIndex2==SceneManager.sceneCountInBuildSettings)
        {
            sceneIndex2 = 0;
        }
        SceneManager.LoadScene(sceneIndex2);
    }
}
