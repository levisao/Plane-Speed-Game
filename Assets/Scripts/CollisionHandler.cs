using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadSceneDelay = 1f;

    //[SerializeField] GameObject particleCrash;
    [SerializeField] ParticleSystem particleCrash;
    [SerializeField] Collider[] shipColliders; //fazendo uma lista com todos os colliders da nave, cotém diferentes tipos de collider

    private void Start()
    {
        particleCrash.GetComponent<ParticleSystem>().Stop();
    }
    //PlayerControls playerControls;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(this.name + "Collided with Trigger " + other.gameObject.name); //this.name é o nome do objeto o qual esta o script
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        //particleCrash.GetComponent<ParticleSystem>().Play(); //triggering the explosion
        particleCrash.Play(); //pode ser do jeito de cima tbma
        GetComponent<MeshRenderer>().enabled = false;
        DisableAllColliders();
        GetComponent<PlayerControls>().enabled = false; //simpler way
        //playerControls.enabled = false; // fazer dessa forma requer arrastar o objet no inspector
        Debug.Log("GOT HIT");
        Invoke("ReloadLevel", loadSceneDelay);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void DisableAllColliders()
    {
        foreach (Collider collider in shipColliders) //correndo pela lista e desabilitando os colliders
        {
        collider.enabled = false;
        }

    }
}
