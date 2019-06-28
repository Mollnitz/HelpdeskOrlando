using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.Events;

public class EnemyShootingManagement : MonoBehaviour
{
    [SerializeField]
    public ShootSO weapon;

    [SerializeField][Range(1f, 10f)]
    float chaseDistance = 4f;

    [SerializeField]
    [Range(10f, 35f)]
    float inaccuracy = 15;

    Rigidbody2D rb2d;

    internal UnityEvent aboutToFire;
    internal UnityEvent fired;

    private void Awake()
    {
        aboutToFire = new UnityEvent();
        fired = new UnityEvent();
    }

    // Start is called before the first frame update
    void Start()
    {

        

        GameManager.EnemySemaphor++;

        GameManager.enemyPickupEvent.AddListener( (obj, so) =>
        {
            if(obj == gameObject)
            {
                weapon = so;
                StartCoroutine(ShootWeapon());
            }
        });

        rb2d = GetComponent<Rigidbody2D>(); 

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Vector2.Distance(GameManager.instance.playerRef.position, transform.position) > chaseDistance)
        {
            rb2d.velocity =  TowardsPlayer();
            
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        } */
    }


    IEnumerator ShootWeapon()
    {
        while (true)
        {
            yield return new WaitForSeconds(weapon.FireCooldown - 0.5f);
            aboutToFire.Invoke();
            yield return new WaitForSeconds(0.5f);
            GameObject shot = GameObject.Instantiate(weapon.EnemyShot, transform.position + TowardsPlayer() , Quaternion.identity);
            weapon.Shoot(shot.GetComponent<Rigidbody2D>(), Quaternion.Euler(0f, 0f, Random.Range(-inaccuracy, inaccuracy)) * TowardsPlayer() );
            fired.Invoke();
            
        }
    }


    Vector3 TowardsPlayer(bool normalized = true)
    {
        if(normalized)
        {
            return -(transform.position - GameManager.instance.playerRef.position).normalized;
        }
        else
        {
            return -(transform.position - GameManager.instance.playerRef.position);
        }
    }

    


}
