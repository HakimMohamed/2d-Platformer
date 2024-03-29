using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class xpBones : MonoBehaviour
{
    Transform Player;
    Animator anim;
    [SerializeField]Vector2 randonSpeed = Vector2.zero;
    Animator xpBonesAnimatorText;
    int speed;
    float TimeTostart = 2f;
    bool isCollected = false;
    public static event EventHandler OnnewBone;
    void Start()
    {
        speed = (int)UnityEngine.Random.Range(randonSpeed.x, randonSpeed.y);
        Player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        xpBonesAnimatorText = GameObject.FindGameObjectWithTag("numberOfBones").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeTostart -= Time.deltaTime;

        if (TimeTostart <= 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, Time.deltaTime * speed);
        }
        if (Vector2.Distance(transform.position, Player.position) < 1f && !isCollected && TimeTostart <= 0)
        {
            if (xpBonesAnimatorText != null)
            {
                OnnewBone?.Invoke(this, EventArgs.Empty);
                isCollected = true;
                anim.SetTrigger("Vanish");
                xpBonesAnimatorText.SetTrigger("newBone");
                Player.GetComponent<PlayerExp>().numOfbones += 1;
                Player.GetComponent<PlayerExp>().StartCoroutine(Player.GetComponent<PlayerExp>().changePlayersColor());
            }
        }

        if (transform.position.x > Player.transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);

        }
        else if(transform.position.x < Player.transform.position.x)
        {
            transform.localScale = new Vector2(1, 1);

        }
    }
    
   
}
