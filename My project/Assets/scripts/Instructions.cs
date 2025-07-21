using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public GameObject InsPanel1;
    public GameObject InsPanel2;

    public void CloseInstruction()
    {
        InsPanel1.SetActive(false);
        InsPanel2.SetActive(false);
    }

    public void OpenInstruction()
    {
        InsPanel1.SetActive(true);
        InsPanel2.SetActive(false);
    }

    public void NextPage()
    {
        InsPanel1.SetActive(false);
        InsPanel2.SetActive(true);
    }

}
