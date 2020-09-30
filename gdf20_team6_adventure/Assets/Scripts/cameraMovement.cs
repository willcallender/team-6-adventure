using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour {
    // camera object
    Camera cam;
    // player GameObject
    GameObject player;
    // will hold position of player, camera, and distance to player
    Vector3 playerPos, camPos, playerDistance, newPos;
    // properties of camera view
    float width, height;
    // max distance from center for player
    public Vector3 maxDist;

    // Start is called before the first frame update
    void Start() {
        // initialize player object
        player = GameObject.FindGameObjectWithTag("Player");

        // initialize camera object 
        cam = (Camera)FindObjectOfType(typeof(Camera));

        // calculate some basic properties of the camera view
        height = 2 * cam.orthographicSize;
        width = height * cam.aspect;
    }

    // Update is called once per frame
    void Update() {
        // update player positon
        playerPos = player.transform.position;

        // update camera position
        camPos = transform.transform.position;

        // calculate player distance from camera
        playerDistance = playerPos - camPos;

        // print(playerDistance);

        // reset newPos to 0
        newPos = transform.position;

        // check to see if player is too far from center and update newPos
        if (playerDistance.x > maxDist.x) {
            newPos.x = playerPos.x - maxDist.x;
        } else if (playerDistance.x < -maxDist.x) {
            newPos.x = playerPos.x + maxDist.x;
        }
        if (playerDistance.y > maxDist.y) {
            newPos.y = playerPos.y - maxDist.y;
        } else if (playerDistance.y < -maxDist.y) {
            newPos.y = playerPos.y + maxDist.y;
        }

        // update camera position
        transform.position = newPos;
    }
}
