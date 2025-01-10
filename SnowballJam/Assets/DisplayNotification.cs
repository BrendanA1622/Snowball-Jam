using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayNotification : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI displayText;
    private float alphaValue = 0.0f;
    private bool increasingAlpha = false;
    private bool decreasingAlpha = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (increasingAlpha) {
            // Debug.Log("Alpha: " + alphaValue);
            alphaValue += Time.deltaTime * 3.0f;
            if (alphaValue >= 1.0) {
                alphaValue = 1.0f;
                increasingAlpha = false;
                decreasingAlpha = true;
            }
        }
        if (decreasingAlpha) {
            // Debug.Log("Alpha: " + alphaValue);
            alphaValue -= Time.deltaTime * 0.5f;
            if (alphaValue <= 0.0) {
                alphaValue = 0.0f;
                increasingAlpha = false;
                decreasingAlpha = false;
            }
        }

        Color currentColor = displayText.color;
        currentColor.a = alphaValue;
        displayText.color = currentColor;
    }

    public void display(string message) {
        alphaValue = 0.0f;
        increasingAlpha = true;
        displayText.text = message;
    }
}
