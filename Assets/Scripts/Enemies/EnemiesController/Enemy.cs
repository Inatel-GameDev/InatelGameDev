using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int direction = 1;
    public float speed;
    public float speedInitial;
    public int vida;
    public int dano;
    public PlayerHealth playerH;
    public Rigidbody2D rig;
    public enemy_animation animator;
    public bool attacking = false;
    public bool beingAttacked = false;

    public string animation_idle;
    public string animation_running;
    public string animation_attacking;
    public string animation_hit;
    public string animation_dead;

    void Update()
    {
        // Verifica se o inimigo est� vivo antes de permitir movimento
        if (vida > 0)
        {
            Move();
        }
    }

    public void Move()
    {
        // Evitar que o inimigo se mova se estiver morto
        if (vida <= 0) return;

        rig.velocity = new Vector2(direction * speed, rig.velocity.y);

        if (!attacking && !beingAttacked)
        {
            if (rig.velocity.x != 0)
            {
                animator.play_animation(animation_running);
            }
            else
            {
                animator.play_animation(animation_idle);
            }
        }
    }

    public void Flip()
    {
        direction *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public virtual void attack()
    {
        // Evitar que o inimigo ataque se estiver morto
        if (vida <= 0) return;

        Debug.Log("atacando");
        attacking = true;
        animator.play_animation(animation_attacking);
        Invoke("stop_attacking", 0.5f);
        speed = 0;
    }

    public void hurt_player()
    {
        // Aplica dano ao jogador
        playerH.TakeDamage(dano);
    }

    public virtual void perdeVida(int dano)
    {
        beingAttacked = true;
        speed = 0;
        vida -= dano;
        animator.play_animation(animation_hit);

        if (vida <= 0)
        {
            animator.play_animation(animation_dead);
            // Garante que o inimigo seja destru�do ap�s a anima��o de morte
            Invoke(nameof(destroy), 0.6f); // Ajuste o tempo conforme necess�rio
        }
    }

    public void stop_attacking()
    {
        attacking = false;
        if (vida > 0)
        {
            animator.play_animation(animation_running);
            speed = speedInitial;
        }
    }

    public void StopbeingAttacked()
    {
        beingAttacked = false;
        if (vida > 0)
        {
            speed = speedInitial;
        }
    }

    private void destroy()
    {
        // Destroi o objeto do inimigo
        Destroy(gameObject);
    }

}
