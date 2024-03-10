using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private Cannon cannon;
    public PlayerHealth player;

    private void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
        cannon = FindObjectOfType<Cannon>();

        // Verifica a escala do canh�o para determinar a dire��o da bola
        if (cannon.transform.localScale.x < 0)  // Canh�o est� virado para a direita
        {
            // Acelera a bola para a direita
            rb.velocity = Vector2.right * speed;
        }
        else  // Canh�o est� virado para a esquerda
        {
            // Acelera a bola para a esquerda
            rb.velocity = Vector2.left * speed;
        }    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
