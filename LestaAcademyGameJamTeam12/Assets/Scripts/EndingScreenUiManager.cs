using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreenUiManager : MonoBehaviour
{
    public void OnRestartGameButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
}
