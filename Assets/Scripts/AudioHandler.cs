using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

[RequireComponent(typeof(AudioSource))]
public class AudioHandler : MonoBehaviour
{
    public Events AudioEvent;
    private AudioSource aSource;
    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();

        //Hook this audiosource up to play
        switch (AudioEvent)
        {
            case Events.LevelClear:
                GameManager.levelClear.AddListener(PlayClip); break;
            case Events.Discard:
                GameManager.discardEvent.AddListener(x => PlayClip()); break;
            case Events.Pickup:
                GameManager.pickupEvent.AddListener(x => PlayClip()); break;
            case Events.EnemyPickup:
                GameManager.enemyPickupEvent.AddListener((x, y) => PlayClip()); break;
            case Events.Shoot:
                GameManager.shootEvent.AddListener(x => PlayClip()); break;
            case Events.PlayerDamage:
                GameManager.PlayerDamageEvent.AddListener(x => PlayClip()); break;
            case Events.PlayerHeal:
                GameManager.PlayerHealEvent.AddListener(x => PlayClip()); break;

            default:
                break;

        }

    }

    void PlayClip()
    {
        aSource.Play();
    }
    
}
