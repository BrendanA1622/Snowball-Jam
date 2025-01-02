using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gainingParticles : MonoBehaviour
{

    [SerializeField] GameObject upParticleObject;
    [SerializeField] GameObject actualParticlesUpObject;
    [SerializeField] ParticleSystem particlesUp;
    [SerializeField] ParticleSystem particlesDown;
    [SerializeField] GameObject ballObject;
    [SerializeField] Rigidbody ballRigidBody;
    [SerializeField] GameObject cameraObject;
    [SerializeField] GameObject touchGObject;

    public float emissionRate = 0.4f;
    public float emissionRateDown = 0.5f;
    private float sphereRadius = 1.0f;
    // Start is called before the first frame update

    private void FixedUpdate() {
        upParticleObject.transform.position = ballObject.transform.position;
        if (touchGObject != null) {
            touchingGround tgScript = touchGObject.GetComponent<touchingGround>();
            cameraMovement camScript = cameraObject.GetComponent<cameraMovement>();
            ballMovement ballScript = ballObject.GetComponent<ballMovement>();
            var emissionModule = particlesUp.emission;
            var rateOverTime = emissionModule.rateOverTime;

            var emissionModuleDown = particlesDown.emission;
            var rateOverTimeDown = emissionModuleDown.rateOverTime;
            if (tgScript != null) {
                if (tgScript.isGrounded) {

                    SphereCollider sphereCollider = ballObject.GetComponent<SphereCollider>();
                    if (sphereCollider != null) {
                        sphereRadius = sphereCollider.radius * ballObject.transform.localScale.x;
                    }
                    Vector3 bottomPoint = ballObject.transform.position - new Vector3(0, sphereRadius, 0);

                    if (tgScript.onSnow || ballScript.terrainLayerIndex == 1 || ballScript.terrainLayerIndex == 2) {
                        float speed = ballRigidBody.velocity.magnitude;
                        
                        rateOverTime.constant = emissionRate * speed;
                        emissionModule.rateOverTime = rateOverTime;
                        actualParticlesUpObject.transform.localScale = ballRigidBody.transform.localScale;
                        emissionModule.enabled = true;

                        emissionModuleDown.enabled = false;
                    } else {
                        float speed = ballRigidBody.velocity.magnitude;

                        rateOverTimeDown.constant = emissionRateDown * speed;
                        emissionModuleDown.rateOverTime = rateOverTimeDown;
                        emissionModuleDown.enabled = true;

                        emissionModule.enabled = false;
                    }
                } else {
                    emissionModule.enabled = false;
                    emissionModuleDown.enabled = false;
                }
            }
        }
    }
}
