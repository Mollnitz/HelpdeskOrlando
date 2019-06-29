using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    Transform left;

    //Used for closing door;
    Vector3 lorg, rorg;
    Quaternion lorgq, rorgq;

    [SerializeField]
    Transform right;

    bool open = false;
    bool animRunning = false;

    TriggerDoor doorTrigger;

    private void Start()
    {
        lorg = left.position;
        lorgq = left.rotation;
        rorg = right.position;
        rorgq = right.rotation;

        doorTrigger = GetComponentInChildren<TriggerDoor>();
    }

    public void OpenDoor()
    {
        if(!open && !animRunning)
        {
            open = true;
            animRunning = true;
            StartCoroutine(OpenRotation());
            left.GetComponent<Collider2D>().enabled = false;
            right.GetComponent<Collider2D>().enabled = false;
        }
    }
    public void CloseDoor()
    {
        if (open && !animRunning)
        {
            open = false;
            animRunning = true;
            StartCoroutine(CloseRotation());
        }
    }

    private IEnumerator CloseRotation()
    {
        for (int i = 0; i < 200; i++)
        {

            yield return new WaitForEndOfFrame();
            left.position = Vector3.Slerp(left.position, lorg, 0.01f);
            right.position = Vector3.Slerp(right.position, rorg, 0.01f);
            left.rotation = Quaternion.Slerp(left.rotation, lorgq, 0.01f);
            right.rotation = Quaternion.Slerp(right.rotation, rorgq, 0.01f);
            
        }

        left.position = lorg;
        right.position = rorg;
        left.rotation = lorgq;
        right.rotation = rorgq;

        left.GetComponent<Collider2D>().enabled = true;
        right.GetComponent<Collider2D>().enabled = true;
        animRunning = false;

        if (!doorTrigger.TriggerCheck())
        {
            OpenDoor();
        }

    }

    private IEnumerator OpenRotation()
    {
        int i = 0;
        while(i < 90)
        {
            i++;
            yield return new WaitForEndOfFrame();
            left.RotateAround(left.position - left.right * 0.6f, left.forward, -1f);
            right.RotateAround(right.position + right.right * 0.6f, right.forward, 1f);
        }
        animRunning = false;
        if(doorTrigger.TriggerCheck())
        {
            CloseDoor();
        }
       
    }
}
