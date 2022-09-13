using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PularScene : MonoBehaviour
{
    [SerializeField] private string nextScene;

    public void PularTela()
    {
        SoundManager.startPlayerAudioSource.Play();
        SceneManager.LoadScene(nextScene);
    }
}
