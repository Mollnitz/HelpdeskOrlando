using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using System;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip ambient;
    public AudioClip combat;
    public AudioClip clear;

    AudioSource aSource;
    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();

        SelectTrack(GameManager.gameState);

        GameManager.gameStateChangeEvent.AddListener(SelectTrack);
    }

    private void SelectTrack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Ambient:
                aSource.clip = ambient;
                aSource.Play(); break;
            case GameState.Combat:
                aSource.clip = combat;
                aSource.Play(); break;
            case GameState.Clear:
                aSource.clip = clear;
                aSource.Play(); break;
            default:
                break;
        }
    }

}
