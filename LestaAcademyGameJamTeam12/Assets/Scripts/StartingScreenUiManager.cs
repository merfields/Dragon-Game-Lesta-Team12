using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScreenUiManager : MonoBehaviour
{
    public void OnStartGameButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
}
