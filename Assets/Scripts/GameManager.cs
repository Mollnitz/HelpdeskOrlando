using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using System.Text;

namespace Manager { 

    [DataContract]
    internal class HighScore
    {
        [DataMember]
        internal string name;
        [DataMember]
        internal int score;

        public HighScore(string key, int value)
        {
            name = key;
            score = value;
        }
    }
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
    public class PickupEvent : UnityEvent<GroundedWep> { };
    public class ShootEvent : UnityEvent<ShootSO> { };
    public class DiscardEvent : UnityEvent<ShootSO> { };
    public class EnemyPickupEvent : UnityEvent<GameObject, ShootSO> { };

    public class LOIEvent : UnityEvent<Transform> { };



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

        public static LOIEvent LocationOfInterestEvent;
        List<Transform> LOIList;

        public static FloatEvent PointEvent;

        public static GameManager instance;

        public static GameState gameState = GameState.Ambient;

        public Dictionary<string, int> highscores;

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

            PointEvent = new FloatEvent();

            LocationOfInterestEvent = new LOIEvent();
            LOIList = new List<Transform>();

            LocationOfInterestEvent.AddListener(x => {
                LOIList.Add(x);
                });

            playerRef = GameObject.FindGameObjectWithTag("Player").transform;

            highscores = new Dictionary<string, int>();
            //Listen for event
            pickupEvent.AddListener(x => LOIList.Remove(x.transform));

            levelClear.AddListener(() => {
                gameState = GameState.Clear;
                gameStateChangeEvent.Invoke(gameState);
            }
            );

        }

        public static Transform GetTargetFromPool(Transform requester)
        {
           
            if (instance.LOIList.Count > 1) {
                var target = instance.LOIList.OrderBy(x => Vector3.Distance(x.position, requester.position)).First();

                instance.LOIList.Remove(target);
                return target;
            }
            else if (instance.LOIList.Count == 1)
            {
                var target = instance.LOIList.First();
                instance.LOIList.RemoveAt(0);
                return target;
            }
            else
            {
                return instance.playerRef;
            }
        }


        private void Update()
        {
            planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            
        }

    }


}