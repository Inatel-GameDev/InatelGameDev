using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    //Animator
    public TrapsAnimations anim;

    // A posi��o de onde as bolas de canh�o ser�o lan�adas
    public Transform firepoint;

    // Prefab da bola de canh�o
    public Ball ball;

    // Tempo entre cada disparo
    public float timeBetween;

    // Tempo inicial entre disparos
    public float StartTimeBetween;

    //Vida
    public int vida;

    // sinaliza que esta sendo atacado
    public bool beingAttacked = false;

    //sinaliza que esta destruido
    private bool destroyed = false;

    public string animation_idle;
    public string animation_attacking;
    public string animation_hit;
    public string animation_dead;

    void Start()
    {
        // Inicializa o tempo entre disparos com o valor definido no Inspector
        timeBetween = StartTimeBetween;
        anim = GetComponent<TrapsAnimations>();
    }

    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        // Verifica se o tempo entre disparos chegou a zero
        if (timeBetween <= 0 && !destroyed)
        {
            anim.PlayAnimation(animation_attacking);

            // Reinicia o tempo entre disparos
            timeBetween = StartTimeBetween;
        }
        else
        {
            // Reduz o tempo entre disparos com base no tempo decorrido
            timeBetween -= Time.deltaTime;
        }
    }

    public void StopAttacking()
    {
        anim.PlayAnimation(animation_idle);
    }

    public void StopbeingAttacked()
    {
        beingAttacked = false;
        anim.PlayAnimation(animation_idle);
    }

    public void NewBall()
    {

        // Instancia uma nova bola de canh�o no firepoint (posi��o de lan�amento)
        Ball newBall = Instantiate(ball, firepoint.position, firepoint.rotation);

        // Verifica a escala do canh�o para determinar a dire��o da bola
        if (transform.localScale.x < 0)
        {
            // Se o canh�o estiver virado para a esquerda, inverte a escala da bola
            newBall.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void PerdeVida(int dano)
    {
        beingAttacked = true;
        vida -= dano;
        anim.PlayAnimation(animation_hit);
        StopAttacking();
        if (vida <= 0)
        {
            destroyed = true;
            anim.PlayAnimation(animation_dead);
            Destroy();
        }
    }

    public void Destroy()
    {
        Destroy(gameObject, 0.1f);
    }
}