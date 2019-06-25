using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "wep", menuName = "Weapon", order = 2)]
public class ShootSO : ScriptableObject
{
    public GameObject Shot;
    public GameObject EnemyShot;
    public Sprite GroundRepresentation;
    public Action<Rigidbody2D, Vector2> shootAction = shoot;


    static void shoot(Rigidbody2D rb2d, Vector2 dir)
    {
        rb2d.AddForce(dir * 12f, ForceMode2D.Impulse);
    }

}
