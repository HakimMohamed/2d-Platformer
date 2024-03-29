using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyHealth : MonoBehaviour
{


    [Header("Properties")]
    [HideInInspector] public float Health;
    [SerializeField] float MaxHealth;


    [Header("UI Components")]
    [SerializeField] private Image HealthBar;

    [Header("Components")]
    private Animator anim;
    private SpriteRenderer sp;
    private Transform Blood;

    [Header("States")]
    private bool IsDead = false;

    Rigidbody2D rb;
    GameObject player;

    public float slowDownFactor = 0.01f;
    public float slowdownLength =.56f;
    private void Awake()
    {
        //Refrence

        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        //Set Values
        Health = MaxHealth;
        Blood = GameAssets.instance.BloodVFX;
    }

    private void Update()
    {

        if (IsDead)
        {
            EnemyDissolve();
            return;
        }

    }

    //Follow the player

    void AE_EnemyHurt()
    {
        AudioManager_PrototypeHero.instance.PlaySound("EnemyHurt");
    }

    public void EnemyReceiveDamage(float Damage)
    {
        if (IsDead)
            return;

        Health -= Damage;
        HealthBar.fillAmount = Health / MaxHealth;
        if (Health <= 0 && !IsDead)
        {
            //Dead
            Enemy.enemies_Number--;
            IsDead = true;
            player.GetComponent<Playermovement>().EnemiesKilled += 1;
            anim.SetTrigger("Death");

            player.GetComponent<Playermovement>().EnemiesKilled += 1;

            EnemyDeathBone();
            SpawnBones_xp();
            //anim.SetBool("IsDead", IsDead);
            
            rb.bodyType = RigidbodyType2D.Dynamic;
            if (Enemy.enemies_Number <= 0)
            {
                Debug.Log("last enemy");
                StartCoroutine(DoSlowMotion());
            }
                
            return;
        }
        
        anim.SetTrigger("Hit");
        //EnemyBlood();
        
    }
    public IEnumerator DoSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = .02f;

    }
    private void EnemyDissolve()
    {
        Vector4 RGBA = sp.color;
        RGBA -= new Vector4(0, 0, 0, Time.deltaTime);
        transform.GetComponent<SpriteRenderer>().color = RGBA;
        if (RGBA.w <= 0)
            Destroy(gameObject);
    }
    private void SpawnBones_xp()
    {
        int randomBones = Random.Range(1, 7);
        for(int i = 0; i < randomBones; i++)
        {
            Instantiate(GameAssets.instance.XpBones, transform.position, Quaternion.identity);

        }
    }
    private void EnemyDeathBone()
    {
        Instantiate(GameAssets.instance.DeathBone,transform.position, Quaternion.identity);

       
    }
    
}
