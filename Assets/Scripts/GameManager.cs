using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupEvent : UnityEvent<ShootSO> { };
public class EnemyPickupEvent : UnityEvent<GameObject, ShootSO> { };

public class GameManager : MonoBehaviour
{

    public static PickupEvent pickupEvent;
    public static EnemyPickupEvent enemyPickupEvent;
    public static GameManager instance;

    public Transform playerRef;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            //Panic
            DestroyImmediate(gameObject);
        }
        
        pickupEvent = new PickupEvent();
        enemyPickupEvent = new EnemyPickupEvent();

        //Listen for event
        pickupEvent.AddListener(x => Debug.Log("player picked up weapon"));

    }

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
