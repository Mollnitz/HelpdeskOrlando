using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootSOSpeed
{
    slow = 1,
    medium,
    fast
}

[CreateAssetMenu(fileName = "wep", menuName = "Weapon", order = 2)]
public class ShootSO : ScriptableObject
{

    public ShootSOSpeed speed = ShootSOSpeed.slow;
    public GameObject Shot;
    public GameObject EnemyShot;
    public Sprite GroundRepresentation;

    public void Shoot(Rigidbody2D rb2d, Vector2 dir)
    {
        float multiplier = (float)speed;
        rb2d.AddForce(dir * multiplier  * 8f, ForceMode2D.Impulse);
    }

}
