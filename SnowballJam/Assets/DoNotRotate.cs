using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoNotRotate : MonoBehaviour
{
    [SerializeField] private GameObject ownNoRotObject;
    void Update()
    {
        
        gameObject.transform.rotation = Quaternion.identity;

    }
}
