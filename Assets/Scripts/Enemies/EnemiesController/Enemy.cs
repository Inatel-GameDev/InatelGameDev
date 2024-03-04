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
        Move();
    }

    public void Move()
    {
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
        Debug.Log("atacando");
        attacking = true;
        animator.play_animation(animation_attacking);
        Invoke("stop_attacking", 0.5f);
        speed = 0;
    }

    public void hurt_player()
    {
        playerH.TakeDamage(dano);
        Debug.Log("deu dano no player");
    }

    public virtual void perdeVida(int n)
    {
        beingAttacked = true;
        vida -= n;
        animator.play_animation(animation_hit);
        speed = 0;
        Debug.Log("Tomou dano");

        if (vida <= 0)
        {
            Debug.Log("Mourreu");
            animator.play_animation(animation_dead);
        }
    }

    public void stop_attacking()
    {
        Debug.Log("saiu do ataque");
        attacking = false;
        animator.play_animation(animation_running);
        speed = speedInitial;
    }

    public void StopbeingAttacked()
    {
        beingAttacked=false;
        speed = speedInitial;
    }

    private void destroy()
    {
        Destroy(gameObject);
    }
}
