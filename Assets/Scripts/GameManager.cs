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
        PlayerDamage,
        PlayerHeal,
        EnemyPickup,
        Shoot
    }

    public enum GameState
    {
        Ambient,
        Combat,
        Clear
    }

    public class FloatEvent : UnityEvent<float> { };

    public class GameStateEvent : UnityEvent<GameState> { };
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

        public static FloatEvent PlayerDamageEvent;
        public static FloatEvent PlayerHealEvent;

        public static GameStateEvent gameStateChangeEvent;

        public static GameManager instance;

        public static GameState gameState = GameState.Ambient;

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

        static Plane[] planes;
        
        public static void StartCombat()
        {
            if(gameState == GameState.Ambient)
            {
                
                gameState = GameState.Combat;
                gameStateChangeEvent.Invoke(gameState);
            }

        }

        public static bool IsVisible(Collider2D col)
        {
            return GeometryUtility.TestPlanesAABB(planes, col.bounds);
        }

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

            PlayerDamageEvent = new FloatEvent();
            PlayerHealEvent = new FloatEvent();

            gameStateChangeEvent = new GameStateEvent();

            playerRef = GameObject.FindGameObjectWithTag("Player").transform;

            //Listen for event
            //pickupEvent.AddListener(x => Debug.Log("player picked up weapon"));

            levelClear.AddListener(() => {
                gameState = GameState.Clear;
                gameStateChangeEvent.Invoke(gameState);
            }
            );

        }

        private void Update()
        {
            planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        }

    }


}