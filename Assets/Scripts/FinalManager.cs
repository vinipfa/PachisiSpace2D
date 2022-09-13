using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FinalManager : MonoBehaviour
{

    public Text VencedorTxt;

    public Dictionary<string, int> ContagemDeJogadoresNaUltimaCasa = GameManager.Instance.ContagemNaUltimaCasa;

    private List<string> CoresJogadores = MenuColor.jogadorEscolheu;

    // Start is called before the first frame update
    void Start()
    {
        VerificaFinalJogo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void VerificaFinalJogo()
    {
        foreach (KeyValuePair<string, int> jogador in ContagemDeJogadoresNaUltimaCasa)
        {
            if (jogador.Value == 4)
            {   
                IdentificaJogadorVencedor(jogador.Key);   
            }
        }
    }

    private void IdentificaJogadorVencedor(string Cor)
    {
        for (int i = 0; i < CoresJogadores.Count; i++)
        {
            if(Cor == CoresJogadores[i])
            {
                VencedorTxt.text = "Jogador " + i + " " + Cor;
            }
        }
    }
}
