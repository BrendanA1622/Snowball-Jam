using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchingGround : MonoBehaviour
{
    [SerializeField] GameObject groundedDisplay;
    public bool isGrounded = false;
    public bool onSnow = false;
    MeshRenderer meshRenderer;

    private void Start() {
        meshRenderer = groundedDisplay.GetComponent<MeshRenderer>();
    }

    private void OnTriggerStay(Collider other) {

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
