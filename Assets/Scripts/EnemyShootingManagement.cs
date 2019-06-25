using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingManagement : MonoBehaviour
{
    [SerializeField]
    ShootSO weapon;

    [SerializeField][Range(1, 10)]
    float chaseDistance = 4f;

    Rigidbody2D rb2d;


    // Start is called before the first frame update
    void Start()
    {
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
        if(Vector2.Distance(GameManager.instance.playerRef.position, transform.position) > chaseDistance)
        {
            rb2d.velocity =  TowardsPlayer();
            
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }


    IEnumerator ShootWeapon()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            GameObject shot = GameObject.Instantiate(weapon.EnemyShot, transform.position + TowardsPlayer() , Quaternion.identity);
            weapon.shootAction(shot.GetComponent<Rigidbody2D>(), Quaternion.Euler(0f, 0f, Random.Range(-15, 15)) * TowardsPlayer() );

            
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
