using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Events
{
    Win,
    Discard,
    Pickup,
    EnemyPickup,
    Shoot
}

public class PickupEvent : UnityEvent<ShootSO> { };

public class ShootEvent : UnityEvent<ShootSO> { };
public class DiscardEvent : UnityEvent<ShootSO> { };
public class EnemyPickupEvent : UnityEvent<GameObject, ShootSO> { };

public class GameManager : MonoBehaviour
{
    public static UnityEvent winEvent;

    public static ShootEvent shootEvent;

    public static DiscardEvent discardEvent;
    public static PickupEvent pickupEvent;
    public static EnemyPickupEvent enemyPickupEvent;

    public static GameManager instance;

    private static int enemySemaphor = 0;
    public static int EnemySemaphor
    {
        set
        {
            enemySemaphor = value;
            if(EnemySemaphor == 0)
            {
                winEvent.Invoke();
            }
        }

        get
        {
            return enemySemaphor;
        }
    }

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

        shootEvent = new ShootEvent();
        discardEvent = new DiscardEvent();
        pickupEvent = new PickupEvent();
        enemyPickupEvent = new EnemyPickupEvent();
        winEvent = new UnityEvent();

        //Listen for event
        //pickupEvent.AddListener(x => Debug.Log("player picked up weapon"));

    }

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").transform;
    }

}
