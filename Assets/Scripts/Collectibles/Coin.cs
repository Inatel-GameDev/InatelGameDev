using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    private PlayerController playerController;
    public Text coinText;  // Refer�ncia ao objeto Text que mostrar� a quantidade de moedas
    Animator animator;

    private void Start()
    {
        // Encontrar o objeto PlayerController na cena
        playerController = FindObjectOfType<PlayerController>();

        // Verificar se a refer�ncia foi obtida com sucesso
        if (playerController == null)
        {
            Debug.LogError("PlayerController n�o encontrado. Certifique-se de que existe um objeto PlayerController na cena.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.AddCoins();

            // Verificar se a refer�ncia ao PlayerController � nula
            if (playerController != null)
            {
                // Obt�m a quantidade de moedas do PlayerController
                int coinCount = playerController.GetCoins();

                // Atualiza o texto com a quantidade de moedas
                coinText.text = coinCount.ToString();
            }
            else
            {
                Debug.LogError("Refer�ncia ao PlayerController � nula. Certifique-se de que foi corretamente atribu�da no Editor Unity.");
            }

            Destroy(this.gameObject);
        }
    }
}
