using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxLife = 4;

    public PlayerController player;
    public Enemy enemy;
    public PlayerAnimationController playerAnimationController;

    public Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;

    //public float knockbackForce = 5f; // For�a do knockback
    //public float knockbackDuration = 0.5f; // Dura��o do knockback

    private void Start()
    {
        player = GetComponent<PlayerController>();

        player.life = maxLife;
        Physics2D.IgnoreLayerCollision(8, 7, false);
    }

    void Update()
    {
        lifeBar();
    }

    void lifeBar()
    {
        for (int i = 0; i < coracao.Length; i++)
        {
            if (player.life > maxLife)
            {
                player.life = maxLife;
            }

            if (i < player.life)
            {
                coracao[i].sprite = cheio;
            }
            else
            {
                coracao[i].sprite = vazio;
            }

            if (i < maxLife)
            {
                coracao[i].enabled = true;
            }
            else
            {
                coracao[i].enabled = false;
            }

        }

    }

    public void TakeDamage(int dano)
    {
        Debug.Log("Player took damage: " + dano);
        player.life -= dano;
        //ApplyKnockback();
        playerAnimationController.PlayAnimation("hit");

        if (player.life <= 0)
        {
            // Reproduzir a anima��o de morte usando o PlayerAnimationController
            playerAnimationController.PlayAnimation("deadHit");

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
        SceneManager.LoadScene("SampleScene");
    }

    //private void ApplyKnockback()
    //{
    //    Obt�m a dire��o do knockback com base na orienta��o do inimigo
    //   Vector2 knockbackDirection = (enemy.transform.position - player.transform.position).normalized; // player - inimigo

    //    Aplica a for�a do knockback

    //   player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5f), ForceMode2D.Impulse); //knockback para cima apenas 

    //    Aguarda a dura��o do knockback antes de parar o movimento
    //    StartCoroutine(StopKnockback());
    //}

    //private IEnumerator StopKnockback()
    //{
    //    yield return new WaitForSeconds(knockbackDuration);

    //    Para o movimento do inimigo ap�s o knockback
    //    player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //}

}
