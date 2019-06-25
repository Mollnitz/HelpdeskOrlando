using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<HealthScript>().deathEvent.AddListener(() => SceneManager.LoadScene("DeathScreen"));
    }

}
