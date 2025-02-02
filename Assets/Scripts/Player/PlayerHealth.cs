using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{

    public PlayerController player;
    public PlayerAnimationController playerAnimationController;
    public Enemy enemy;
    public UI_manager UI;

    public float knockbackForce = 5f; // For�a do knockback
    public float knockbackDuration = 0.5f; // Dura��o do knockback
    private bool hasFallen = false;

    private void Start()
    {
        player = GetComponent<PlayerController>();

        Physics2D.IgnoreLayerCollision(8, 7, false);
    }

    private void Update()
    {
        Fall();
        enemy = FindObjectOfType<Enemy>();
    }


    public void TakeDamage(int dano)
    {
        // Se o jogador j� caiu, n�o faz nada
        if (hasFallen)
        {
            return;
        }

        player.life -= dano;
        UI.UpdateLifeBar();
        playerAnimationController.PlayAnimation("hit");
        AudioManager.Instance.PlaySFX("Hit1");

        if (enemy != null)
        {
            ApplyKnockback();
        }

        if (player.life <= 0)
        {
            // Reproduzir a anima��o de morte usando o PlayerAnimationController
            playerAnimationController.PlayAnimation("deadHit");
            AudioManager.Instance.PlaySFX("KO");

            // Parar o movimento e desabilitar o controle do jogador
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Physics2D.IgnoreLayerCollision(8, 7);
            player.enabled = false;

            // recarregar cena ap�s um segundo
            Invoke("LoadScene", 1f);
        }
    }

    void LoadScene()
    {
        // Tentar carregar o �ndice da cena salva, caso contr�rio, carrega a cena atual
        int cenaSalvaIndex = PlayerPrefs.GetInt("FaseAtual", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(cenaSalvaIndex);
    }


    void Fall()
    {
        // Se o jogador j� caiu, n�o faz nada
        if (hasFallen)
        {
            return;
        }

        if (player.transform.position.y < -10)
        {
            AudioManager.Instance.PlaySFX("KO_Far");
            hasFallen = true; // Define a vari�vel de controle como verdadeira para indicar que o jogador caiu
            Invoke("LoadScene", 1f);
        }
    }

    private void ApplyKnockback()
    {
        //Obt�m a dire��o do knockback com base na orienta��o do inimigo
        Vector2 knockbackDirection = (enemy.transform.position - player.transform.position).normalized; // player - inimigo

        //Aplica a for�a do knockback

        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse); //knockback para cima apenas 

        //Aguarda a dura��o do knockback antes de parar o movimento
        StartCoroutine(StopKnockback());
    }

    private IEnumerator StopKnockback()
    {
        yield return new WaitForSeconds(knockbackDuration);

        //Para o movimento do inimigo ap�s o knockback
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

}
