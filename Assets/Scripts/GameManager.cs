using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
	[SerializeField] private string nextScene;

	// identifica o jogador que deve jogar
	public Text JogadorText;

	// identifica qual � o jogador da vez ("que est� jogando agora") 
	public int JogadorVez=0;

	// list com a cor de cada jogador
	private List<string> jogadorEscolheu = MenuColor.jogadorEscolheu;
	//private List<string> jogadorEscolheu = gambiList();

	// Armazena a posição de cada personagem dos jogadores
	public int PlayerVermelho1Index = 0, PlayerVermelho2Index = 0, PlayerVermelho3Index = 0, PlayerVermelho4Index = 0;
	public int PlayerAmarelo1Index = 0, PlayerAmarelo2Index = 0, PlayerAmarelo3Index = 0, PlayerAmarelo4Index = 0;
	public int PlayerVerde1Index = 0, PlayerVerde2Index = 0, PlayerVerde3Index = 0, PlayerVerde4Index = 0;
	public int PlayerAzul1Index = 0, PlayerAzul2Index = 0, PlayerAzul3Index = 0, PlayerAzul4Index = 0;

	// Controle do movimento dos jogadores
	public static GameObject PlayerVermelho1, PlayerVermelho2, PlayerVermelho3, PlayerVermelho4;
    public static GameObject PlayerVerde1, PlayerVerde2, PlayerVerde3, PlayerVerde4;
    public static GameObject PlayerAzul1, PlayerAzul2, PlayerAzul3, PlayerAzul4;
    public static GameObject PlayerAmarelo1, PlayerAmarelo2, PlayerAmarelo3, PlayerAmarelo4;

	public Transform dado;

    public Button ButtonDado;

    public int selectDadoAnimacao=0;

	public GameObject DadosJogarTxt;
	public GameObject Dados1Animacao;
	public GameObject Dados2Animacao;
	public GameObject Dados3Animacao;
	public GameObject Dados4Animacao;
	public GameObject Dados5Animacao;
	public GameObject Dados6Animacao;

	// Random generation of dice numbers...
	private System.Random randomNo;

	private static GameManager instance = null;

	// flag para validar se pode ou não passar a vez do jogador
	// caso true representa que o jogador esta em uma condição de selecionar um personagem
	// e mover porem não vai ser trocado o jogador pois ele não selecionou o personagem ainda
	private bool flagAtualizaJogador = false;

	public List<GameObject> ListaDePlayers = new List<GameObject>();

	public Dictionary<string, int> ContagemNaUltimaCasa = new Dictionary<string, int>();

	public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
				instance = GameObject.Find("GameManager").GetComponent<GameManager>();
            }
			return instance;
        }
    }

	public static List<string> gambiList()
    {
		List<string> list = new List<string>();
		list.Add("vermelho");
		list.Add("verde");
		list.Add("amarelo");
		list.Add("azul");
		return list;
    }

	//Verifica se o dado foi jogado.
	//Um exemplo de aplica��o � permitir escolher o jogador apenas depois de rodar o dado.
	public bool VerificaSeDadoFoiJogado()
    {
		return !ButtonDado.interactable;
	}

	// Click on Roll Button on Dice UI
	public void DiceRoll()
	{
		// Desativa o bot�o do dado
		ButtonDado.interactable = false;

		//Debug.Log("Rodando o dado");
		selectDadoAnimacao = randomNo.Next(1, 12);
		if(selectDadoAnimacao > 6 ) selectDadoAnimacao = 6;

		switch (selectDadoAnimacao)
		{
			case 1:
				Dados1Animacao.SetActive(true);
				Dados2Animacao.SetActive(false);
				Dados3Animacao.SetActive(false);
				Dados4Animacao.SetActive(false);
				Dados5Animacao.SetActive(false);
				Dados6Animacao.SetActive(false);
				break;

			case 2:
				Dados1Animacao.SetActive(false);
				Dados2Animacao.SetActive(true);
				Dados3Animacao.SetActive(false);
				Dados4Animacao.SetActive(false);
				Dados5Animacao.SetActive(false);
				Dados6Animacao.SetActive(false);
				break;

			case 3:
				Dados1Animacao.SetActive(false);
				Dados2Animacao.SetActive(false);
				Dados3Animacao.SetActive(true);
				Dados4Animacao.SetActive(false);
				Dados5Animacao.SetActive(false);
				Dados6Animacao.SetActive(false);
				break;

			case 4:
				Dados1Animacao.SetActive(false);
				Dados2Animacao.SetActive(false);
				Dados3Animacao.SetActive(false);
				Dados4Animacao.SetActive(true);
				Dados5Animacao.SetActive(false);
				Dados6Animacao.SetActive(false);
				break;

			case 5:
				Dados1Animacao.SetActive(false);
				Dados2Animacao.SetActive(false);
				Dados3Animacao.SetActive(false);
				Dados4Animacao.SetActive(false);
				Dados5Animacao.SetActive(true);
				Dados6Animacao.SetActive(false);
				break;

			case 6:
				Dados1Animacao.SetActive(false);
				Dados2Animacao.SetActive(false);
				Dados3Animacao.SetActive(false);
				Dados4Animacao.SetActive(false);
				Dados5Animacao.SetActive(false);
				Dados6Animacao.SetActive(true);
				break;
		}

		if (selectDadoAnimacao != 6)
		{
			//Debug.Log("Dado diferente de 6");
			switch (CorJogadorVez())
			{
				case "vermelho":
					AtualizaJogador(
						VerificaPersonagemForaDaBase(
							PlayerVermelho1,
							PlayerVermelho2,
							PlayerVermelho3,
							PlayerVermelho4
						)
					);
					//Debug.Log("Dado diferente de 6 - caso vermelho");
					break;
				case "verde":
					AtualizaJogador(
						VerificaPersonagemForaDaBase(
							PlayerVerde1,
							PlayerVerde2,
							PlayerVerde3,
							PlayerVerde4
						)
					);
					//Debug.Log("Dado diferente de 6 - caso verde");
					break;
				case "azul":
					AtualizaJogador(
						VerificaPersonagemForaDaBase(
							PlayerAzul1,
							PlayerAzul2,
							PlayerAzul3,
							PlayerAzul4
						)
					);
					//Debug.Log("Dado diferente de 6 - caso azul");
					break;
				case "amarelo":
					AtualizaJogador(
						VerificaPersonagemForaDaBase(
							PlayerAmarelo1,
							PlayerAmarelo2,
							PlayerAmarelo3,
							PlayerAmarelo4
						)
					);
					//Debug.Log("Dado diferente de 6 - caso amarelo");
					break;
			}
		} else if (selectDadoAnimacao == 6)
        {
			flagAtualizaJogador = true;
		}
	}

	private bool VerificaPersonagemForaDaBase(GameObject Player1, GameObject Player2, GameObject Player3, GameObject Player4)
    {
		//Debug.Log("verifica se tem personagem fora da base");
		if (!Player1.GetComponent<PlayerScript>().Escolhido &&
			!Player2.GetComponent<PlayerScript>().Escolhido &&
			!Player3.GetComponent<PlayerScript>().Escolhido &&
			!Player4.GetComponent<PlayerScript>().Escolhido)
		{
			ButtonDado.interactable = true;
			return true;
		}

		flagAtualizaJogador = true;

		return false;
		
	}

	private void Start()
	{
		QualitySettings.vSyncCount = 1;
		Application.targetFrameRate = 30;

		randomNo = new System.Random();

		Dados1Animacao.SetActive(false);
		Dados2Animacao.SetActive(false);
		Dados3Animacao.SetActive(false);
		Dados4Animacao.SetActive(false);
		Dados5Animacao.SetActive(false);
		Dados6Animacao.SetActive(false);

		PlayerVermelho1 = GameObject.Find("Vermelho-1");
		PlayerVermelho2 = GameObject.Find("Vermelho-2");
		PlayerVermelho3 = GameObject.Find("Vermelho-3");
		PlayerVermelho4 = GameObject.Find("Vermelho-4");
		PlayerAmarelo1 = GameObject.Find("Amarelo-1");
		PlayerAmarelo2 = GameObject.Find("Amarelo-2");
		PlayerAmarelo3 = GameObject.Find("Amarelo-3");
		PlayerAmarelo4 = GameObject.Find("Amarelo-4");
		PlayerVerde1 = GameObject.Find("Verde-1");
		PlayerVerde2 = GameObject.Find("Verde-2");
		PlayerVerde3 = GameObject.Find("Verde-3");
		PlayerVerde4 = GameObject.Find("Verde-4");
		PlayerAzul1 = GameObject.Find("Azul-1");
		PlayerAzul2 = GameObject.Find("Azul-2");
		PlayerAzul3 = GameObject.Find("Azul-3");
		PlayerAzul4 = GameObject.Find("Azul-4");

		ListaDePlayers.Add(PlayerVermelho1);
		ListaDePlayers.Add(PlayerVermelho2);
		ListaDePlayers.Add(PlayerVermelho3);
		ListaDePlayers.Add(PlayerVermelho4);
		ListaDePlayers.Add(PlayerAmarelo1);
		ListaDePlayers.Add(PlayerAmarelo2);
		ListaDePlayers.Add(PlayerAmarelo3);
		ListaDePlayers.Add(PlayerAmarelo4);
		ListaDePlayers.Add(PlayerVerde1);
		ListaDePlayers.Add(PlayerVerde2);
		ListaDePlayers.Add(PlayerVerde3);
		ListaDePlayers.Add(PlayerVerde4);
		ListaDePlayers.Add(PlayerAzul1);
		ListaDePlayers.Add(PlayerAzul2);
		ListaDePlayers.Add(PlayerAzul3);
		ListaDePlayers.Add(PlayerAzul4);

		// adicionando um listener em cada um dos personagens
		PlayerVermelho1.GetComponent<Button>().onClick.AddListener(() => PlayerVermelho1.GetComponent<PlayerScript>().FoiClicado());
		PlayerVermelho2.GetComponent<Button>().onClick.AddListener(() => PlayerVermelho2.GetComponent<PlayerScript>().FoiClicado());
		PlayerVermelho3.GetComponent<Button>().onClick.AddListener(() => PlayerVermelho3.GetComponent<PlayerScript>().FoiClicado());
		PlayerVermelho4.GetComponent<Button>().onClick.AddListener(() => PlayerVermelho4.GetComponent<PlayerScript>().FoiClicado());
		PlayerVerde1.GetComponent<Button>().onClick.AddListener(() => PlayerVerde1.GetComponent<PlayerScript>().FoiClicado());
		PlayerVerde2.GetComponent<Button>().onClick.AddListener(() => PlayerVerde2.GetComponent<PlayerScript>().FoiClicado());
		PlayerVerde3.GetComponent<Button>().onClick.AddListener(() => PlayerVerde3.GetComponent<PlayerScript>().FoiClicado());
		PlayerVerde4.GetComponent<Button>().onClick.AddListener(() => PlayerVerde4.GetComponent<PlayerScript>().FoiClicado());
		PlayerAmarelo1.GetComponent<Button>().onClick.AddListener(() => PlayerAmarelo1.GetComponent<PlayerScript>().FoiClicado());
		PlayerAmarelo2.GetComponent<Button>().onClick.AddListener(() => PlayerAmarelo2.GetComponent<PlayerScript>().FoiClicado());
		PlayerAmarelo3.GetComponent<Button>().onClick.AddListener(() => PlayerAmarelo3.GetComponent<PlayerScript>().FoiClicado());
		PlayerAmarelo4.GetComponent<Button>().onClick.AddListener(() => PlayerAmarelo4.GetComponent<PlayerScript>().FoiClicado());
		PlayerAzul1.GetComponent<Button>().onClick.AddListener(() => PlayerAzul1.GetComponent<PlayerScript>().FoiClicado());
		PlayerAzul2.GetComponent<Button>().onClick.AddListener(() => PlayerAzul2.GetComponent<PlayerScript>().FoiClicado());
		PlayerAzul3.GetComponent<Button>().onClick.AddListener(() => PlayerAzul3.GetComponent<PlayerScript>().FoiClicado());
		PlayerAzul4.GetComponent<Button>().onClick.AddListener(() => PlayerAzul4.GetComponent<PlayerScript>().FoiClicado());

		AtualizaJogador(false);
	}

	private void VerificaLimiteMovimento(GameObject Player, ref int PlayerIndex)
    {
		// se o personagem ainda não tiver sido escolhido, então não faz nada
		if (!Player.GetComponent<PlayerScript>().Escolhido ) return;

		// mover o personagem para a primeira casa caso tenha tirado 6
        if (Player.GetComponent<PlayerScript>().caminhoIndex > PlayerIndex && Player.GetComponent<PlayerScript>().EmJogo == false)
        {
            Player.GetComponent<PlayerScript>().moveAllowed = false;
			PlayerIndex = Player.GetComponent<PlayerScript>().caminhoIndex - 1;
			ButtonDado.interactable = true;

            if (flagAtualizaJogador == true)
            {
                AtualizaJogador(true);
                flagAtualizaJogador = false;
            }

            Player.GetComponent<PlayerScript>().EmJogo = true;

            Debug.Log("SÓ SAI DA CASA ");
            Debug.Log("PlayerIndex " + Player.GetComponent<PlayerScript>().Escolhido);
            Debug.Log("Caminho Length " + Player.GetComponent<PlayerScript>().caminho.Length);
            Debug.Log("Caminho index " + Player.GetComponent<PlayerScript>().caminhoIndex);
            Debug.Log("Player index " + PlayerIndex);

        }// verifica se o personagem já moveu o tanto que apareceu no dado
        else if (Player.GetComponent<PlayerScript>().caminhoIndex >
		   PlayerIndex + selectDadoAnimacao)
		{		
			Player.GetComponent<PlayerScript>().moveAllowed = false;
			PlayerIndex = Player.GetComponent<PlayerScript>().caminhoIndex - 1;

			ButtonDado.interactable = true;

			if (flagAtualizaJogador == true)
			{
				AtualizaJogador(true);
				flagAtualizaJogador = false;
				List<GameObject> OtherPlayersList = VerificaSeTemDoisJogadoresJuntos(Player, ListaDePlayers);

				foreach(GameObject otherPlayer in OtherPlayersList)
                {
					ResetApartiDoPlayerGOOPlyerIndex(otherPlayer);

				}
			}

            Debug.Log("MOVENDO NORMAL");
            Debug.Log("PlayerIndex " + Player.GetComponent<PlayerScript>().Escolhido);
            Debug.Log("Caminho Length " + Player.GetComponent<PlayerScript>().caminho.Length);
            Debug.Log("Caminho index " + Player.GetComponent<PlayerScript>().caminhoIndex);
            Debug.Log("Player index " + PlayerIndex);
		}
	}

	private void Update()
    {
		VerificaLimiteMovimento(PlayerVermelho1, ref PlayerVermelho1Index);
		VerificaLimiteMovimento(PlayerVermelho2, ref PlayerVermelho2Index);
		VerificaLimiteMovimento(PlayerVermelho3, ref PlayerVermelho3Index);
		VerificaLimiteMovimento(PlayerVermelho4, ref PlayerVermelho4Index);

		VerificaLimiteMovimento(PlayerAmarelo1, ref PlayerAmarelo1Index);
		VerificaLimiteMovimento(PlayerAmarelo2, ref PlayerAmarelo2Index);
		VerificaLimiteMovimento(PlayerAmarelo3, ref PlayerAmarelo3Index);
		VerificaLimiteMovimento(PlayerAmarelo4, ref PlayerAmarelo4Index);

		VerificaLimiteMovimento(PlayerVerde1, ref PlayerVerde1Index);
		VerificaLimiteMovimento(PlayerVerde2, ref PlayerVerde2Index);
		VerificaLimiteMovimento(PlayerVerde3, ref PlayerVerde3Index);
		VerificaLimiteMovimento(PlayerVerde4, ref PlayerVerde4Index);

		VerificaLimiteMovimento(PlayerAzul1, ref PlayerAzul1Index);
		VerificaLimiteMovimento(PlayerAzul2, ref PlayerAzul2Index);
		VerificaLimiteMovimento(PlayerAzul3, ref PlayerAzul3Index);
		VerificaLimiteMovimento(PlayerAzul4, ref PlayerAzul4Index);

		if(VerificaFinalJogo())
        {
			SceneManager.LoadScene(nextScene);
		}	
	}

	private bool VerificaFinalJogo()
    {
		foreach(GameObject Player in ListaDePlayers)
        {
			PlayerScript PlayerScript = Player.GetComponent<PlayerScript>();

            if (PlayerScript.EstaNaUltimaCasa())
            {
				if (ContagemNaUltimaCasa.ContainsKey(PlayerScript.Cor))
					ContagemNaUltimaCasa[PlayerScript.Cor] = ContagemNaUltimaCasa[PlayerScript.Cor] + 1;
				else
					ContagemNaUltimaCasa[PlayerScript.Cor] = 1;
			}
        }

		foreach(int valor in ContagemNaUltimaCasa.Values)
        {
			if (valor == 4) return true;
        }
		return false;
    }

	public static void MovePlayer(GameObject player)
	{
		player.GetComponent<PlayerScript>().IniciaMovimento();	
	}

	public void AtualizaJogador(bool passaVez)
    {
		DadosJogarTxt.SetActive(true);

		if (passaVez)
        {
			JogadorVez = (JogadorVez + 1) % 4;
		}

		JogadorText.text = (JogadorVez+1).ToString() + " " + jogadorEscolheu[JogadorVez];

		//Debug.Log("AtualizaJogador " + JogadorVez);
	}

	public string CorJogadorVez()
    {
		return jogadorEscolheu[JogadorVez];
    }
	
	public List<GameObject> VerificaSeTemDoisJogadoresJuntos(GameObject PlayerGO, List<GameObject> ListPlayers)
    {
		PlayerScript Player = PlayerGO.GetComponent<PlayerScript>();
		List<GameObject> OtherPlayerReturnList = new List<GameObject>();

		foreach (GameObject OtherPlayerGO in ListPlayers)
        {
			PlayerScript OtherPlayer = OtherPlayerGO.GetComponent<PlayerScript>();

			if (Player.JogadorNaMesmaCasa(OtherPlayer))
            {
				OtherPlayer.ResetPlayer();
				OtherPlayerReturnList.Add(OtherPlayerGO);
            }
		}
		return OtherPlayerReturnList;
	}


	private void ResetApartiDoPlayerGOOPlyerIndex(GameObject Player)
    {
		if (PlayerVermelho1 == Player) PlayerVermelho1Index = 0;
		if (PlayerVermelho2 == Player) PlayerVermelho2Index = 0;
		if (PlayerVermelho3 == Player) PlayerVermelho3Index = 0;
		if (PlayerVermelho4 == Player) PlayerVermelho4Index = 0;
		if (PlayerAmarelo1 == Player) PlayerAmarelo1Index = 0;
		if (PlayerAmarelo2 == Player) PlayerAmarelo2Index = 0;
		if (PlayerAmarelo3 == Player) PlayerAmarelo3Index = 0;
		if (PlayerAmarelo4 == Player) PlayerAmarelo4Index = 0;
		if (PlayerVerde1 == Player) PlayerVerde1Index = 0;
		if (PlayerVerde2 == Player) PlayerVerde2Index = 0;
		if (PlayerVerde3 == Player) PlayerVerde3Index = 0;
		if (PlayerVerde4 == Player) PlayerVerde4Index = 0;
		if (PlayerAzul1 == Player) PlayerAzul1Index = 0;
		if (PlayerAzul2 == Player) PlayerAzul2Index = 0;
		if (PlayerAzul3 == Player) PlayerAzul3Index = 0;
		if (PlayerAzul4 == Player) PlayerAzul4Index = 0;
	}
}


