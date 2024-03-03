using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int vidaMaxima = 3;
    [SerializeField] private int vidaAtual;

    public EnemyController enemy;
    public EnemyAnimationController enemyAnimationController;

    public float knockbackForce = 5f; // For�a do knockback
    public float knockbackDuration = 0.5f; // Dura��o do knockback

    private void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void TakeDamage(int dano)
    {
        if (vidaAtual > 0)
        {
            vidaAtual -= dano;
            ApplyKnockback();
            enemyAnimationController.PlayEnemyAnimation("FierceHit");

            if (vidaAtual <= 0)
            {
                enemyAnimationController.PlayEnemyAnimation("FierceDeadHit");
                StartCoroutine(DieWithDelay());
            }
        }
    }

    private IEnumerator DieWithDelay()
    {
        enemy.enabled = false;
        yield return new WaitForSeconds(0.3f); // Aguarda um tempo antes de destruir o inimigo (ajuste conforme necess�rio)

        // Implemente o que acontece quando o inimigo morre (por exemplo, anima��o, efeitos, etc.)
        Destroy(gameObject);
    }

    private void ApplyKnockback()
    {
        // Obt�m a dire��o do knockback com base na orienta��o do inimigo
        Vector2 knockbackDirection = enemy.turnRight ? Vector2.left : Vector2.right;// (se isso for vdd)? vaz esse : se n faz esse 

        // Aplica a for�a do knockback
        enemy.GetComponent<Rigidbody2D>().velocity = knockbackDirection * knockbackForce;

        // Aguarda a dura��o do knockback antes de parar o movimento
        StartCoroutine(StopKnockback());
    }

    private IEnumerator StopKnockback()
    {
        yield return new WaitForSeconds(knockbackDuration);

        // Para o movimento do inimigo ap�s o knockback
        enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}


