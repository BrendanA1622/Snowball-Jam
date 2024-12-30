using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchingGround : MonoBehaviour
{
    [SerializeField] GameObject groundedDisplay;
    [SerializeField] GameObject ballObject;
    public bool isGrounded = false;
    public bool onSnow = false;
    MeshRenderer meshRenderer;

    private void Start() {
        meshRenderer = groundedDisplay.GetComponent<MeshRenderer>();
    }

    private void OnTriggerStay(Collider other) {

        if (other.tag == "Enemy") {
            if (!(other.transform.localScale.magnitude >= 1.749370 && other.transform.localScale.magnitude <= 1.749372)) {
                // Debug.Log(other.transform.localScale.magnitude);
                if (other.transform.localScale.magnitude - 0.5f >= ballObject.transform.localScale.magnitude) {
                    ballMovement ballScript = ballObject.GetComponent<ballMovement>();
                    ballScript.KillPlayer();
                } else if (other.transform.localScale.magnitude + 0.5f <= ballObject.transform.localScale.magnitude) {
                    if (other.GetComponent<EnemySimpleMovement>()) {
                        EnemySimpleMovement enemyScript = other.GetComponent<EnemySimpleMovement>();
                        enemyScript.KillEnemy();
                    }
                }
            }
        }

        if (other.tag == "Finish") {
            Debug.Log("Nice Job!");
        }


        if (other.tag == "Snow") {
            onSnow = true;
        } else {
            onSnow = false;
        }


        isGrounded = true;
        if (meshRenderer) {
            meshRenderer.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        isGrounded = false;
        if (meshRenderer) {
            meshRenderer.enabled = true;
        }
    }
}
