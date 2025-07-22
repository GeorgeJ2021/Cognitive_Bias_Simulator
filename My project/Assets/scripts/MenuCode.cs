using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCode : MonoBehaviour
{
    public GameObject DisclaimerPanel;
    public GameObject StartPanel;

    public void StartGame()
    {
        DisclaimerPanel.SetActive(true);
        StartPanel.SetActive(false);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(1);
    }

}
