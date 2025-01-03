using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [SerializeField] GameObject cameraObject;
    [SerializeField] GameObject ballObject;
    [SerializeField] Rigidbody ballRigidBody;
    public float camDistance;
    public float cameraHeightOffset;
    public float rotationSpeed = 100.0f;

    public float currentRotation = 180.0f;


    private Vector3 travelDirection = new Vector3(0,0,1);
    public bool inMovingGame = true;

    private void Start() {
        travelDirection = new Vector3(0,0,1);
    }

    private void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");
        currentRotation += horizontalInput * rotationSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0, currentRotation, 0);
        Vector3 direction = rotation * Vector3.back;


        Vector3 desiredPosition = ballObject.transform.position + direction * camDistance;
        desiredPosition.y += cameraHeightOffset;
        
        if (inMovingGame) {
            cameraObject.transform.position = desiredPosition;
            cameraObject.transform.LookAt(ballObject.transform);
        }
        
    }
}
