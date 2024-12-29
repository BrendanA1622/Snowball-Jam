using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class followBallPos : MonoBehaviour
{
    [SerializeField] GameObject ballObject;
    [SerializeField] GameObject followingObject;

    // Update is called once per frame
    void Update()
    {
        followingObject.transform.position = ballObject.transform.position;
    }
}
