using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length; //stores in an Array
        if (numMusicPlayers > 1) //if there's already one instance in the scene. desroy
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject); // para a msica n reiniciar toda vez q morremos
        }
    }
}
