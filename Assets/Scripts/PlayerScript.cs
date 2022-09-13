using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public Transform[] caminho;

    [SerializeField]
    private float moveSpeed = 50f;

    [HideInInspector]
    public int caminhoIndex = 0;

    public bool moveAllowed = false;

    public bool Escolhido = false;

    public string Cor;

    public bool EmJogo = false;

    public Transform CasaInicial;

    // Use this for initialization
    private void Start()
    {
    
    }

    // Update is called once per frame
    private void Update()
    {
        if (moveAllowed)
            Move();
    }

    private void Move()
    {
        if (caminhoIndex <= caminho.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
            caminho[caminhoIndex].transform.position,
            moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, caminho[caminhoIndex].transform.position) < 0.1 )
            {
                caminhoIndex += 1;
            }

            /*Debug.Log("Movendo Personagem");
            Debug.Log("caminho" + caminhoIndex);
            Debug.Log("caminho.length" + caminho.Length);
            Debug.Log("caminho transform" + caminho[caminhoIndex].transform.position);
            Debug.Log("caminho transform" + transform.position);*/
        }
    }

    public void LiberaPersonagem()
    {
        Escolhido = true;
        moveAllowed = true;

        //Debug.Log ("Liberando Personagem");
    }

    public void FoiClicado()
    {
        if (GameManager.Instance.CorJogadorVez() == Cor 
            && GameManager.Instance.VerificaSeDadoFoiJogado()) 
        {
            if (Escolhido)
            {
                bool TemEspacoParaAndar = GameManager.Instance.selectDadoAnimacao <= caminho.Length - caminhoIndex;

                if (TemEspacoParaAndar)
                {
                    LiberaPersonagem();
                }
                else
                {
                    GameManager.Instance.ButtonDado.interactable = true;
                    GameManager.Instance.AtualizaJogador(true);
                }
            }
            else if (!Escolhido && GameManager.Instance.selectDadoAnimacao == 6)
            {
                //Debug.Log("Conseguiu sair");
                LiberaPersonagem();
            }
        }
    }

    public void IniciaMovimento()
    {
        if(Escolhido == true)
        {
            moveAllowed = true;
        }
    }

    public bool JogadorNaMesmaCasa(PlayerScript otherPlayer)
    {
        if (Cor == otherPlayer.Cor) return false;
        return (Vector2.Distance(transform.position, otherPlayer.transform.position) < 0.1);
    }

    public void ResetPlayer()
    {
        transform.position = CasaInicial.position;
        caminhoIndex = 0;
        moveAllowed = false;
        Escolhido = false;
        EmJogo = false;
    }

    public bool EstaNaUltimaCasa()
    {
        return caminhoIndex == caminho.Length;
    }


}
