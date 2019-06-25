using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitLifespan : MonoBehaviour
{

    [SerializeField][Range(4f, 10f)]
    float time = 5;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
