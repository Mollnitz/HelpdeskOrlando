using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.Events;

public class EnemyShootingManagement : MonoBehaviour
{
    [SerializeField]
    public ShootSO weapon;
    

    [SerializeField]
    [Range(10f, 35f)]
    float inaccuracy = 15;

    Rigidbody2D rb2d;

    internal UnityEvent aboutToFire;
    internal UnityEvent fired;

    IEnumerator shootRoutine;

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
                StopAllCoroutines();
                StartCoroutine(ShootWeapon());
            }
        });

        rb2d = GetComponent<Rigidbody2D>(); 

    }


    IEnumerator ShootWeapon()
    {
        while (true)
        {
            var ray = Physics2D.Raycast(transform.position, TowardsPlayer(), 10f, LayerMask.GetMask("Obstacles", "IgnorePlayer"));
            if (GameManager.IsVisible(GetComponent<Collider2D>()) && ray.transform != null && (ray).transform.CompareTag("Player"))
            {
                GameManager.StartCombat();
                yield return new WaitForSeconds(weapon.FireCooldown - 0.5f);
                aboutToFire.Invoke();
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < weapon.ShotAmount; i++)
                {
                    GameObject shot = GameObject.Instantiate(weapon.EnemyShot, transform.position + TowardsPlayer(), Quaternion.identity);
                    weapon.Shoot(shot.GetComponent<Rigidbody2D>(), Quaternion.Euler(0f, 0f, Random.Range(-inaccuracy, inaccuracy)) * TowardsPlayer());
                }
                fired.Invoke();
            }
            else
            {
                yield return new WaitForSeconds(weapon.FireCooldown);
            }
            
            
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
