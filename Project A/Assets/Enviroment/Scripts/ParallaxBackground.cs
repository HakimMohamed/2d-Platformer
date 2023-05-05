using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCameraPositoin;
    [SerializeField]float ParallaxEffect = 0;
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPositoin = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPositoin;
        transform.position += new Vector3( deltaMovement.x,0,0)*ParallaxEffect;
        lastCameraPositoin = new Vector3(cameraTransform.position.x, 0, 0);

    }


}
