using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "charspriteset", menuName = "Orlando/SpriteSet", order = 2)]
public class CharacterSpriteSetSO : ScriptableObject
{
    public Sprite Standard;
    public Sprite Armed;
    public Sprite Firing;
}
