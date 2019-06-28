using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Manager { 

    public enum Events
    {
        LevelClear,
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
        public static UnityEvent levelClear;

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
                    levelClear.Invoke();
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
            levelClear = new UnityEvent();

            playerRef = GameObject.FindGameObjectWithTag("Player").transform;

            //Listen for event
            //pickupEvent.AddListener(x => Debug.Log("player picked up weapon"));

        }


    }


}