using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHitable
{
    public static Player instance;
    
    [SerializeField] private PlayerStats stats;
    [SerializeField] private PlayerComponents components;
    [SerializeField] private PlayerRefrences references;
    [SerializeField] private PlayerUtilities utilities;
    private PlayerActions actions;
    
    //for camera smooth movement
    private float fallingSpeedYDampingChangeThreshold;


    public PlayerComponents Components
    {
        get
        {
            return components;
        }
    }

    public PlayerStats Stats
    {
        get
        {
            return stats;
        }
    }

    public PlayerActions Actions
    {
        get
        {
            return actions;
        }
    }
    
    public PlayerUtilities Utilities
    {
        get
        {
            return utilities;
        }
    }
    public PlayerRefrences References
    {
        get
        {
            return references;
        }
    }

    private void Awake()
    {
    }


    private void Start()
    {
        Time.timeScale = 1;
        actions = new PlayerActions(this);
        utilities = new PlayerUtilities(this);
        stats.Speed = stats.WalkSpeed;
        stats.alive = true;
        stats.IsImmortal = false;

        AnyStateAnimation[] animations = new AnyStateAnimation[]
        {
            new AnyStateAnimation(RIG.BODY, "Body_Idle", "Body_Attack", "Body_Hurt","Body_Dash"),
            new AnyStateAnimation(RIG.BODY, "Body_Walk", "Body_Attack", "Body_Jump", "Body_Hurt"),
            new AnyStateAnimation(RIG.BODY, "Body_Jump", "Body_Hurt"),
            new AnyStateAnimation(RIG.BODY, "Body_Fall", "Body_Hurt"),
            new AnyStateAnimation(RIG.BODY, "Body_Attack", "Body_Hurt"),
            new AnyStateAnimation(RIG.BODY, "Body_Hurt", "Body_Death"),
            new AnyStateAnimation(RIG.BODY, "Body_Death"),
            new AnyStateAnimation(RIG.BODY, "Body_Dash"),
            
            new AnyStateAnimation(RIG.LEGS, "Legs_Idle", "Legs_Attack","Legs_Dash"),
            new AnyStateAnimation(RIG.LEGS, "Legs_Walk", "Legs_Jump","Legs_Dash"),
            new AnyStateAnimation(RIG.LEGS, "Legs_Jump"),
            new AnyStateAnimation(RIG.LEGS, "Legs_Fall"),
            new AnyStateAnimation(RIG.LEGS, "Legs_Attack"),
            new AnyStateAnimation(RIG.LEGS, "Legs_Death"),
            new AnyStateAnimation(RIG.BODY, "Legs_Dash"),
        };

        Components.Animator.AnimationTriggerEvent += Actions.Shoot;
        
        stats.Weapons.Add(WEAPON.ROCK, true);
        stats.Weapons.Add(WEAPON.GUN, false);
        stats.Weapons.Add(WEAPON.SWORD, false);
        
        UIManager.Instance.AddLife(stats.Lives);
        
        components.Animator.AddAnimations(animations);

        fallingSpeedYDampingChangeThreshold = CameraManager.instance.fallSpeedYDampingChangeThreshold;

    }

   
    private void Update()
    {
        if (stats.isDashing)
        {
            return;
        }
        if (stats.Alive)
        {
            utilities.HandleInput();
            utilities.HandleAir();
        }

        if (!Stats.Alive)
        {
            StartCoroutine(actions.BeforeDeath());
        }

        if (Stats.pickedUp)
        {
            //StartCoroutine(actions.WeaponTextDisable());
            if (UIManager.Instance.weaponText != null)
            {
                UIManager.Instance.weaponText.SetActive(true);
            }

            
            if (Input.GetKeyDown(KeyCode.Mouse2) && UIManager.Instance.attackText != null)
            {
                Destroy(UIManager.Instance.weaponText);
                UIManager.Instance.attackText.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && UIManager.Instance.weaponText == null && UIManager.Instance.dashText != null)
            {
                Destroy(UIManager.Instance.attackText);
                UIManager.Instance.dashText.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && UIManager.Instance.attackText == null)
            {
                Destroy(UIManager.Instance.dashText);
                Destroy(UIManager.Instance.tutorialLimit);
            }
        }
        
        //if falling past a certain speed threshold
        if (components.Rigidbody.velocity.y < fallingSpeedYDampingChangeThreshold && !CameraManager.instance.isLerpingYDamping && !CameraManager.instance.lerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }
        //if standing still or moving up
        if (components.Rigidbody.velocity.y >= 0f && !CameraManager.instance.isLerpingYDamping && CameraManager.instance.lerpedFromPlayerFalling)
        {
            //reset so it can be called again
            CameraManager.instance.lerpedFromPlayerFalling = false;
            CameraManager.instance.LerpYDamping(false);
        }

    }

    private void FixedUpdate()
    {
        if (stats.isDashing)
        {
            return;
        }
        
        if (stats.Alive)
        {
            actions.Move(transform);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        actions.Collide(collision);
    }
    

    public void TakeHit()
    {
        actions.TakeDamage();
    }
}
