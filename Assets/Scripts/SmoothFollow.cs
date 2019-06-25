using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Makes the camera follow the player in a smooth fashion, also adapts to the players movement, by listening to the rigidbody2d velocity.
/// </summary>
public class SmoothFollow : MonoBehaviour
{
    Rigidbody2D player;

    [SerializeField][Range(7f, 15f)]
    float cameraDistance = 10f;

    [SerializeField][Range(2f, 8f)]
    float movementDampening = 3f;

    Vector3 speed = Vector3.zero;

    Vector3 offset; //Should be const / readonly

    private void Awake()
    {
        offset = Vector3.back * cameraDistance;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + (new Vector3(player.velocity.x, player.velocity.y, 0f) / movementDampening) + offset, ref speed, 0.1f);   
    }
}
