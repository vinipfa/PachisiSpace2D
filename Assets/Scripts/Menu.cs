using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Menu : MonoBehaviour
{
    [SerializeField] private string nextScene;

    public void Play()
    {
        SoundManager.startPlayerAudioSource.Play();
        SceneManager.LoadScene(nextScene);
    }

    public void Sair()
    {
        Application.Quit();
    }
}