
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
        if (canseeplayer(agroRange)) {
            chasePlayer();

        }
        else
        {
            stopchasingplayer();
        }

    }
    bool canseeplayer(float distance)
    {
        bool val = false;
        float castdist = distance;
        if (isFacingLeft)
        {
            castdist = -distance;
        }
        Vector2 endpos = castpoint.position + Vector3.right * castdist;
        RaycastHit2D hit = Physics2D.Linecast(castpoint.position, endpos, 1 << LayerMask.NameToLayer("Action"));
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            
        }
        else
        {
            Debug.DrawLine(castpoint.position, endpos, Color.blue);
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



  
   












