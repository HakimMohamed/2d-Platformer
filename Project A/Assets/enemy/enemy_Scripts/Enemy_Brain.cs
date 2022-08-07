
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Brain : MonoBehaviour
{

    [Header("raycast tools")]
    [SerializeField] Transform castpoint;
    [SerializeField] float agroRange;
    [SerializeField] float movespeed;
    Rigidbody2D rb2d;
    bool isFacingLeft;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        if (canseePlayer(agroRange)) {
            chasePlayer();

        }
        else
        {
            stopchasingplayer();
        }

    }
    bool canseePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;
        if (isFacingLeft)
        {
            castDist = -distance;
        }
        Vector2 endPos = castpoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castpoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }






        else
            {
                Debug.DrawLine(castpoint.position, endPos, Color.blue);
            }
           
        }
        return val;
    } 
    void chasePlayer()
    {
        if(transform.position.x < castpoint.position.x)
        {
            rb2d.velocity = new Vector2(movespeed, 0);
            transform.localScale = new Vector3(1, 1);
            isFacingLeft = false;

        }
        else
        {
            rb2d.velocity = new Vector2(-movespeed, 0);
            transform.localScale = new Vector2(-1, 1);
            isFacingLeft = true;
        }

    }
    void stopchasingplayer()
    {
        rb2d.velocity = new Vector2(0, 0);

    }

}



  
   












