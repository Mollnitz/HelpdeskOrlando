using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface IShootable
{
    GameObject Item { get; set; }
    GameObject Eject();
    GameObject Pickup();
}