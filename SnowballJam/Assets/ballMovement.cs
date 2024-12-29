using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class ballMovement : MonoBehaviour
{
    [SerializeField] GameObject cameraObject;
    [SerializeField] GameObject ballObject;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject touchGObject;


    public float growthFactor = 0.01f;
    public float maxScale = 5.0f;
    public float minScale = 0.3f;
    public float camDistScale = 4.0f;

    public float forceMagnitude = 700.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if (touchGObject != null) {
            touchingGround tgScript = touchGObject.GetComponent<touchingGround>();
            cameraMovement camScript = cameraObject.GetComponent<cameraMovement>();
            if (tgScript != null) {
                camScript.camDistance = 5.0f + rb.transform.localScale.magnitude * camDistScale;
                camScript.cameraHeightOffset = camScript.camDistance * (4.553655f/10.0f);
                if (tgScript.isGrounded) {
                    float direction = camScript.currentRotation;

                    rb.AddForce((float)(Time.deltaTime * forceMagnitude * Input.GetAxis("Vertical") * Math.Sin((direction/360.0) * (2 * Math.PI))),0,(float)(Time.deltaTime * forceMagnitude * Input.GetAxis("Vertical") * Math.Cos((direction/360.0) * (2 * Math.PI))));


                    if (tgScript.onSnow) {
                        float speed = rb.velocity.magnitude;
                        float scaleIncrease = speed * growthFactor * Time.deltaTime;
                        Vector3 newScale = rb.transform.localScale + Vector3.one * scaleIncrease;
                        newScale = Vector3.Min(newScale, Vector3.one * maxScale);
                        rb.transform.localScale = newScale;
                    } else {
                        float speed = rb.velocity.magnitude;
                        float scaleIncrease = -speed * growthFactor * Time.deltaTime;
                        Vector3 newScale = rb.transform.localScale + Vector3.one * scaleIncrease;
                        newScale = Vector3.Max(newScale, Vector3.one * minScale);
                        rb.transform.localScale = newScale;
                    }
                    
                }
            }
        }
        if (rb.transform.localScale.magnitude <= (Vector3.one * minScale).magnitude) {
            Debug.Log("Game Over");
        }
    }
}
