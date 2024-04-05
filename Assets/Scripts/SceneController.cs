using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void NextLevel()
    {
        int proximoIndiceDeCena = SceneManager.GetActiveScene().buildIndex + 1;

        // Salva o �ndice da pr�xima cena que ser� carregada
        PlayerPrefs.SetInt("FaseAtual", proximoIndiceDeCena);

        // Agora, carrega a pr�xima cena
        SceneManager.LoadSceneAsync(proximoIndiceDeCena);
    }


    void OnApplicationQuit() {
        PlayerPrefs.DeleteKey("FaseAtual"); // Remove a fase salva ao fechar o jogo
    }

    // Adicione o restante do seu script aqui
}

