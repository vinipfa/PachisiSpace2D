using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuColor : MonoBehaviour
{
    public Text title, start;

    public static List<string> jogadorEscolheu = new List<string>();

    public Button verde, vermelho, azul, amarelo;

    [SerializeField] private string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        start.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //title.text = "Jogador " + (jogadorEscolheu.Count + 1).ToString();
    }

    private bool verificaEscolhaAcabou()
    {
       if(jogadorEscolheu.Count == 4)
        {
            start.enabled = true;
        }

        return true;
    }

    public void SelecionaPersonagemVerde()
    {
        verde.interactable = false;
        
        jogadorEscolheu.Add("verde");

        verificaEscolhaAcabou();
    }

    public void SelecionaPersonagemVermelho()
    {
        vermelho.interactable = false;

        jogadorEscolheu.Add("vermelho");

        verificaEscolhaAcabou();
    }

    public void SelecionaPersonagemAzul()
    {
        azul.interactable = false;

        jogadorEscolheu.Add("azul");

        verificaEscolhaAcabou();
    }

    public void SelecionaPersonagemAmarelo()
    {
        amarelo.interactable = false;

        jogadorEscolheu.Add("amarelo");

        verificaEscolhaAcabou();
    }

    public void startGame()
    {
        SoundManager.startPlayerAudioSource.Play();
        SceneManager.LoadScene(nextScene);
    }
}
