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

[CreateAssetMenu(fileName = "wep", menuName = "Orlando/Weapon", order = 2)]
public class ShootSO : ScriptableObject
{

    public ShootSOSpeed speed = ShootSOSpeed.slow;
    public GameObject Shot;
    public GameObject EnemyShot;
    public Sprite GroundRepresentation;

    public AudioClip sound;

    [Range(0.8f, 5f)]
    public float FireCooldown = 1.5f;

    [Range(1f, 10f)]
    public float PointMultiplier = 2f;


    public void Shoot(Rigidbody2D rb2d, Vector2 dir)
    {
        float multiplier = (float)speed;
        rb2d.AddForce(dir * multiplier  * 8f, ForceMode2D.Impulse);
    }

}
