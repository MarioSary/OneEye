using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, ICollisionHandler, IHitable
{
    public static Enemy instance;
    
    //this var are from RedBoss video
    public int health;
    public int damageAmount;
    //private float timeBtwDamage = 1.5f;

    //public Animator FatManAnimator;
    //public Animator cameraAnim;
    public Slider healthBar;
    
    
    //this var are from 2d platformer video
    [SerializeField] private Animator animator;
    //[SerializeField] private GameObject projectilePrefab;
    //[SerializeField] private Transform[] projectilePostions;
    //[SerializeField] private GameObject rocksAttackPrefab;
    //[SerializeField] private Transform[] rocksAttackPostions;
    //[SerializeField] private GameObject[] tearsPrefab;
    //[SerializeField] private Transform[] tearsPostions;
    private Transform target;
    [SerializeField] private float attackCoolDown;
    private bool canAttack = true;
    private float timeSinceAttack;
    [SerializeField] private GameObject fatMan;
    [SerializeField] private GameObject fatManHealth;
    public static bool alive;
    
    
    public bool isPlayerClose = false;
    private GameObject toDeactivate;
    public GameObject healthCanvas;
    public Animator angryManAnim;

    private Transform player;
    [SerializeField] private GameObject cave;
    [SerializeField] private GameObject caveEmpty;
    //public GameObject teleportHolder;
    private SpriteRenderer sprite;

    
    [SerializeField] private AudioSource fatManAnger;
    //[SerializeField] private AudioSource fatManFireBalls;
    [SerializeField] private AudioSource fatManRocks;
    [SerializeField] private AudioSource fatManPunching;
    [SerializeField] private AudioSource fatManCrying;
    [SerializeField] private AudioSource fatManDeath;
    [SerializeField] private AudioSource fatManScreem;

    [SerializeField] private GameObject fireBallSpawner;
    [SerializeField] private GameObject fireballsBoostWave;
    [SerializeField] private GameObject rockWave;
    [SerializeField] private GameObject tearsWave;
    [SerializeField] private GameObject tearsWaveBoost;
    [SerializeField] private GameObject darkHole = null;
    [SerializeField] private GameObject orb = null;



    public void Start()
    {
        alive = true;
        toDeactivate = transform.GetChild(0).gameObject;
        player = GameObject.FindWithTag("Player").transform;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //this lines are from RedBoss video
        // give the player some time to recover before taking more damage
        /*if (timeBtwDamage > 0)
        {
            timeBtwDamage -= Time.deltaTime;
        }*/
        
        healthBar.value = health;
        
        
        //2dPlatformer
        if (alive)
        {
            LookAtTarget();
            Attack();
        }

    }
    
    private void LookAtTarget()
    {
        if (target != null)
        {
            Vector3 scale = transform.localScale;
            scale.x = target.transform.position.x < transform.position.x ? -1 : 1;
            transform.localScale = scale;
        }
    }

    public void Damage()
    {
        if (RefGameObjectsEvent.instance.fatmanHealthCheck == null)
        {
            health -= damageAmount;
        }
        
    }

    public void TakeHit()
    {
        if (health > 0)
        {
            Damage();
            Attack();
        }
        if (health == 0)
        {
            Death();
        }
        
    }
    
    private void Attack()
    {
        if (!canAttack)
        {
            timeSinceAttack += Time.deltaTime;
        }

        if (timeSinceAttack >= attackCoolDown)
        {
            canAttack = true;
        }

        if (canAttack && target != null && Mathf.Abs(target.transform.position.y - transform.position.y) <=5)
        {
            canAttack = false;
            timeSinceAttack = 0;
            animator.SetBool("PinAttack", true);
        }
    }

    public void AngerStart()
    {
        fatManAnger.Play();
        DialogueManager.instance.dialogueBox.SetActive(false);
        DialogueManager.instance.inDialogue = false;
        toDeactivate.SetActive(false);
        healthCanvas.SetActive(true);
    }

    public void AngerHitSound()
    {
        fatManPunching.Play();
    }
    
    // public void PinAttack()
    // {
    //     int fireballSpawn = Random.Range(0, projectilePostions.Length);
    //      GameObject fireBall = Instantiate(projectilePrefab, projectilePostions[fireballSpawn].position, quaternion.identity);
    //      Vector3 direction = new Vector3(0, transform.localScale.y);
    //      fireBall.GetComponent<Projectile>().Setup(direction);
    //      fatManFireBalls.Play();
    // }

    public void GroundAttack()
    {
        fatManRocks.Play();
        // int rocksSpawn = Random.Range(2, rocksAttackPostions.Length);
        // GameObject rockS = Instantiate(rocksAttackPrefab, new Vector3(player.position.x, 23), quaternion.identity);
        // Vector3 direction = Vector3.down;
        // rockS.GetComponent<Projectile>().Setup(direction);
    }
    
    public void CryAttack()
    {
        fatManCrying.Play();
        // int tearsSpawn = Random.Range(0, tearsPrefab.Length);
        // int tearsPositions = Random.Range(0, tearsPostions.Length);
        // Instantiate(tearsPrefab[tearsSpawn], tearsPostions[tearsPositions].position, quaternion.identity);
        //Vector3 direction = Vector3.down;
        //tear.GetComponent<Projectile>().Setup(direction);
        //FindObjectOfType<Projectile>().Setup(direction);
    }


    public void StopAttack()
    {
        animator.SetBool("PinAttack", false);
        
        if (health > 75)
        {
            fireBallSpawner.SetActive(true);
        }

        if (health <= 75)
        {
            fireBallSpawner.SetActive(false);
            animator.SetTrigger("AngerBoost");
            fireballsBoostWave.SetActive(true);
        }

        if (health <= 50)
        {
            fireballsBoostWave.SetActive(false);
            animator.SetTrigger("RockS");
            rockWave.SetActive(true);
            //damageAmount = 3;
        }

        if (health <= 25)
        {
            rockWave.SetActive(false);
            animator.SetTrigger("Destruction");
            StartCoroutine(TearsCall());
            //damageAmount = 2;

        }

        if (health == 0)
        {
            fatManCrying.Stop();
            //AfterDeath.enemyDead = true;
            Death();
            
        }
    }

    IEnumerator TearsCall()
    {
        if (health > 1)
        {
            yield return new WaitForSeconds(5);
            tearsWave.SetActive(true);
        }
    }

    IEnumerator StopTearsCall()
    {
        yield return new WaitForSeconds(2);
        StopTears();
        caveEmpty.SetActive(true);
        cave.SetActive(false);
        darkHole.SetActive(true);
        yield return new WaitForSeconds(30);
        StopTearsBoost();
        orb.SetActive(true);
        yield return new WaitForSeconds(18);
        fatManDeath.volume--;
        fatManScreem.Stop();
        fatManCrying.volume--;
        yield return new WaitForSeconds(8);
        fatManCrying.Stop();
        fatManDeath.Stop();
        fatMan.SetActive(false);
        alive = false;
    }

    void StopTears()
    {
        tearsWave.SetActive(false);
        tearsWaveBoost.SetActive(true);
    }
    
    void StopTearsBoost()
    {
        tearsWaveBoost.SetActive(false);
    }


    private void Death()
    {
        //fatManExplosion.Play();
        animator.SetTrigger("Death");
    }

    public void StartDeathMusic()
    {
        fatManDeath.Play();
        fatManScreem.Play();
        
    }
    
    public void Cave()
    {
        StartCoroutine(StopTearsCall());
        fatManHealth.SetActive(false);
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadingOut());
    }
    
    IEnumerator FadingOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = sprite.material.color;
            c.a = f;
            sprite.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }

    }

    private void OnDisable()
    {
        //alive = false;
        
    }
    
    public void CollisionEnter(string colliderName, GameObject other)
    {
        if (colliderName == "DamageArea" && other.tag == "Player")
        {
            //Debug.Log("Damage Enter");
            other.GetComponent<Player>().Actions.TakeDamage();
        }
        if (colliderName == "Sight" && other.tag == "Player")
        {
            //Debug.Log("sight Enter");
            animator.SetTrigger("Notice");
            angryManAnim.SetTrigger("Enter");
            /*if (target == null)
            {
                this.target = other.transform;
            }*/
            
        }
        if (colliderName == "DialogueTrigger" && other.tag == "Player")
        {
            Debug.Log("dialogue Enter");
            //fatManSigh.Play();
            DialogueManager.instance.conversationButton.SetActive(true);
            isPlayerClose = true;
            angryManAnim.SetTrigger("NoticeDialogue");
        }
        if (colliderName == "Player" || colliderName == "GroundCollider" && other.tag == "RockProjectile")
        {
            Destroy(gameObject);
        }

    }

    public void CollisionExit(string colliderName, GameObject other)
    {
        if (colliderName == "Sight" && other.tag == "Player")
        {
            target = null;
        }
        if (colliderName == "DialogueTrigger" && other.tag == "Player")
        {
            Debug.Log("dialogue Exit");
            isPlayerClose = false;
            if (DialogueManager.instance.conversationButton != null)
            {
                DialogueManager.instance.conversationButton.SetActive(false);
            }
            
        }
    }
}
