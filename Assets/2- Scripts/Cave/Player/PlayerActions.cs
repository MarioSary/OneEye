using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActions
{
    private Player player;
    public static PlayerActions instance;


    public PlayerActions(Player player)
    {
        this.player = player;
        
    }

    public void Move(Transform transform)
    {
        if (DialogueManager.instance.InDialogue)
        {
            player.References.StepWalk.Stop();
            return;
        }
        else
        {
            player.Components.Rigidbody.velocity = new Vector2(
                player.Stats.Direction.x * player.Stats.Speed * Time.deltaTime,
                player.Components.Rigidbody.velocity.y);
        
            if (player.Stats.Direction.x != 0)
            {
                transform.localScale = new Vector3(player.Stats.Direction.x < 0 ? -1 : 1, 1, 1);
                player.Components.Animator.TryPlayAnimation("Body_Walk");
                player.Components.Animator.TryPlayAnimation("Legs_Walk");
                

            }
            else if (player.Components.Rigidbody.velocity == Vector2.zero)
            {
                player.References.StepWalk.Play();
                player.Components.Animator.TryPlayAnimation("Body_Idle");
                player.Components.Animator.TryPlayAnimation("Legs_Idle");
            }
        }
            
    }
    
    
    private IEnumerator Dash()
    {
        player.Stats.canDash = false;
        player.Stats.isDashing = true;
        float originalGravity = player.Components.Rigidbody.gravityScale;
        player.Components.Rigidbody.gravityScale = 0f;
        player.Components.Rigidbody.velocity = new Vector2(player.transform.localScale.x * player.Stats.dashingPower, 0f);
        player.Stats.trailRenderer.emitting = true;
        yield return new WaitForSeconds(player.Stats.dashingTime);
        player.Stats.trailRenderer.emitting = false;
        player.Components.Rigidbody.gravityScale = originalGravity;
        player.Stats.isDashing = false;
        yield return new WaitForSeconds(player.Stats.dashingCollDown);
        player.Stats.canDash = true;

    }

    public void DashCall()
    {
        if (player.Stats.canDash)
        {
            if (DialogueManager.instance.InDialogue)
            {
                return;
            }
            else
            {
                player.StartCoroutine(Dash());
            }
            
        }
    }
    
    public void PickUpWeapon(WEAPON weapon)
    {
        player.Stats.Weapons[weapon] = true;
        UIManager.Instance.weaponText.SetActive(true);
        

    }

    // public IEnumerator WeaponTextDisable()
    // {
    //     yield return new WaitForSeconds(2);
    //     UIManager.Instance.weaponText.SetActive(false);
    //     UIManager.Instance.attackText.SetActive(true);
    //     yield return new WaitForSeconds(2);
    //     UIManager.Instance.attackText.SetActive(false);
    //     UIManager.Instance.dashText.SetActive(true);
    //     yield return new WaitForSeconds(2);
    //     UIManager.Instance.dashText.SetActive(false);
    // }

    public void Jump()
    {
        if (DialogueManager.instance.InDialogue)
        {
            return;
        }
        else
        {
            if (player.Utilities.IsGrounded())
            {
                player.Components.Rigidbody.AddForce(new Vector2(0, player.Stats.JumpForce), ForceMode2D.Impulse);
                player.Components.Animator.TryPlayAnimation("Legs_Jump");
                player.Components.Animator.TryPlayAnimation("Body_Jump");
            }
        }

    }

    public void Attack()
    {
        if (DialogueManager.instance.InDialogue)
        {
            return;
        }
        else
        {
            player.Components.Animator.TryPlayAnimation("Legs_Attack");
            player.Components.Animator.TryPlayAnimation("Body_Attack");
        }
        
    }

    public void TrySwapWeapon(WEAPON weapon)
    {
        if (DialogueManager.instance.InDialogue)
        {
            return;
        }
        else
        {
            if (player.Stats.Weapons[weapon] == true)
            {
                player.Stats.Weapon = weapon;
                player.Components.Animator.SetWeapon((int)player.Stats.Weapon);
                SwapWeapon();
            }
        }
        
    }

    public void SwapWeapon()
    {
        if (DialogueManager.instance.InDialogue)
        {
            return;
        }
        else
        {
            for (int i = 1; i < player.References.WeaponObjects.Length; i++)
            {
                player.References.WeaponObjects[i].SetActive(false);

            }

            if (player.Stats.Weapon > 0)
            {
                player.References.WeaponObjects[(int)player.Stats.Weapon].SetActive(true);

            }
        }
        
    }
    
    

    public void Shoot(string animation)
    {
        if (DialogueManager.instance.InDialogue)
        {
            return;
        }
        else
        {
            if (animation == "Shoot")
            {
                GameObject go = GameObject.Instantiate(player.References.BarrelPrefab, player.References.GunHandBarrel.position, Quaternion.identity);

                Vector3 direction = new Vector3(player.transform.localScale.x, 0);
            
                go.GetComponent<Projectile>().Setup(direction);
            }
        }
        
    }

    public void TakeDamage()
    {
        if (DialogueManager.instance.InDialogue)
        {
            return;
        }
        else
        {
            if (!player.Stats.IsImmortal)
            {
                if (player.Stats.Lives > 0)
                {
                    UIManager.Instance.RemoveLife();
                    player.Stats.Lives--;
                    player.Components.Animator.TryPlayAnimation("Body_Hurt");
                }
                if (player.Stats.Alive)
                {
                    player.StartCoroutine(Immortality());
                }

                if (!player.Stats.Alive)
                {
                    player.Stats.alive = false;
                    player.References.Death.Play();
                    player.Components.Animator.TryPlayAnimation("Body_Death");
                    player.Components.Animator.TryPlayAnimation("Legs_Death");
                    

                }
            }
        }
        
    }

    private IEnumerator Blink()
    {
        while (player.Stats.IsImmortal)
        {
            for (int i = 0; i < player.Components.SpriteRenderers.Length; i++)
            {
                player.Components.SpriteRenderers[i].enabled = false;
            }

            yield return new WaitForSeconds(.15f);
            
            for (int i = 0; i < player.Components.SpriteRenderers.Length; i++)
            {
                player.Components.SpriteRenderers[i].enabled = true;
            }
            
            yield return new WaitForSeconds(.15f);
        }
    }

    private IEnumerator Immortality()
    {
        player.Stats.IsImmortal = true;
        player.StartCoroutine(Blink());
        yield return new WaitForSeconds(player.Stats.ImmortalityTime);
        player.Stats.IsImmortal = false;
    }
    
    public IEnumerator BeforeDeath()
    {
        yield return new WaitForSeconds(2);
        player.References.GameOverUI.SetActive(true);
        Time.timeScale = 0;
        AudioListener.volume = 0;
    }

    public void Collide(Collider2D Collision)
    {
        if (Collision.tag == "Collectable")
        {
            Collision.GetComponent<ICollectable>().Collect();
        }
    }

    
}
