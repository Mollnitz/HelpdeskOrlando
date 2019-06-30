using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class LocationOfInterest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.LocationOfInterestEvent.Invoke(transform);
    }


}
