using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "wep", menuName = "Weapon", order = 2)]
public class ShootSO : ScriptableObject
{
    public GameObject shot;
    public Sprite GroundRepresentation;
    public Action<Rigidbody2D, Vector2> Action = new Action<Rigidbody2D, Vector2>((x, y) => x.AddForce(y)) ;

}
