using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;

    //Room Camera
    // [SerializeField] private float speed;
    // private float currentPosX;
    // private Vector3 velocity = Vector3.zero;

    //Follow Player
    //[SerializeField] private Transform player;
    // [SerializeField] private float aheadDistance;
    //[SerializeField] private float upDistance;
    //[SerializeField] private float cameraSpeed;


    //Follow Player 2
    //public Transform player;
    //public float cameraDistance = 30.0f;


    //Clamp Camera

    private void Awake()
    {
        //Follow Player 2
        // GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
    }


    private void Update()
    {
        //Room Camera
        //gradually changes a vector towards a desired goal
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), 
        // ref velocity, speed);

        //Follow Player
        //transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        //lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * transform.localScale.x), Time.deltaTime + cameraSpeed);

        //Follow Player 2
        // transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);


        //Clamp Camera

        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, -6.8f, 100f),
            Mathf.Clamp(targetToFollow.position.y, 0f, 9f),
            transform.position.z);
         }
    public void MoveToNewRoom(Transform _newRoom)
    {
        // currentPosX = _newRoom.position.x;
    }


}









