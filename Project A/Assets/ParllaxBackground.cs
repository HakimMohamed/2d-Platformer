using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParllaxBackground : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x*parallaxEffectMultiplier.x, deltaMovement.y *parallaxEffectMultiplier.y, deltaMovement.z);
        lastCameraPosition = cameraTransform.position;
    }
}
