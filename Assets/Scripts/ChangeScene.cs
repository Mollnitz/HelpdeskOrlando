using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string target;

    public void Load()
    {
        SceneManager.LoadScene(target);
    }

}
