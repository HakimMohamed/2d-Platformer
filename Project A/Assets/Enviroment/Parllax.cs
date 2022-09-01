using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parllax : MonoBehaviour
{
    private float Length, startPos;
    public GameObject cam;
    public float parllaxEffect;

    void Start()
    {
        startPos = transform.position.x;
        Length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = (cam.transform.position.x * parllaxEffect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);


    }
}
