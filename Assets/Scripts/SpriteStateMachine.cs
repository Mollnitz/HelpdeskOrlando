using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class SpriteStateMachine : MonoBehaviour
{
    SpriteRenderer sr;
    public CharacterSpriteSetSO spriteSet;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        sr.sprite = SelectSprite();

        GameManager.enemyPickupEvent.AddListener((obj, so) =>
        {
            if (obj == transform.parent.gameObject)
            {
                sr.sprite = spriteSet.Armed;
            }
        });

        GetComponentInParent<EnemyShootingManagement>().aboutToFire.AddListener(() =>
        {
            sr.sprite = spriteSet.Firing;
        });

        GetComponentInParent<EnemyShootingManagement>().fired.AddListener(() =>
        {
            sr.sprite = spriteSet.Armed;
        });


    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(GameManager.instance.playerRef.position - transform.position, Vector3.forward) * Quaternion.Euler(-90f, -90f, 0f);
    }


    private Sprite SelectSprite()
    {
        var EnemyM = GetComponentInParent<EnemyShootingManagement>();
        if (EnemyM == null)
        {
            //Panic
            Debug.LogError("Something is wrong here");

        }
        else if(EnemyM.weapon != null)
        {
            return spriteSet.Armed;
        }

        return spriteSet.Standard;
    }
}
