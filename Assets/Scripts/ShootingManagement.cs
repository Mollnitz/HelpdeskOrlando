using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
public class ShootingManagement : MonoBehaviour
{
    [SerializeField]
    ShootSO carriedWep; //Current weapon

    [SerializeField]
    GameObject groundWep;

    // Start is called before the first frame update
    void Start()
    {
        //Equip weapon on pickup
        GameManager.pickupEvent.AddListener(x => carriedWep = x);

        //Throw weapon away on button input.
        GameManager.discardEvent.AddListener(x =>
        {
            GameObject wep = GameObject.Instantiate(groundWep, transform.position + (transform.up * 2f), Quaternion.identity);
            wep.GetComponent<GroundedWep>().so = x;
            carriedWep = null;
        });

        //Shoot weapon on button input
        GameManager.shootEvent.AddListener(x =>
        {
            GameObject shot = GameObject.Instantiate(x.Shot, transform.position + (transform.up * 1f), Quaternion.identity);
            x.Shoot(shot.GetComponent<Rigidbody2D>(), transform.up);
        });

        
    }

    // Update is called once per frame
    void Update()
    {
        if(carriedWep != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameManager.shootEvent.Invoke(carriedWep);
            }

            if (Input.GetButtonDown("Fire2"))
            {
                GameManager.discardEvent.Invoke(carriedWep);
            }
        }
        


    }



}
