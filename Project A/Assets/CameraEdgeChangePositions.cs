using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraEdgeChangePositions : MonoBehaviour
{
    [Header("P1")]
    [SerializeField] private Transform P1;
    [SerializeField] private Transform P1_NewPosition;

    [Header("P2")]
    [SerializeField] private Transform P2;
    [SerializeField] private Transform P2_NewPosition;

    [Header("P3")]
    [SerializeField] private Transform P3;
    [SerializeField] private Transform P3_NewPosition;

    Transform player;
    [SerializeField] PolygonCollider2D CameraEdge2;
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y < P1.position.y)
        {
           transform.position =  Vector2.Lerp(transform.position, P1_NewPosition.position,Time.deltaTime*10f);

            
            Invoke("ChangeConfiner", .4f);
        }
    }
    private void ChangeConfiner()
    {
        GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = CameraEdge2;
    }
}
