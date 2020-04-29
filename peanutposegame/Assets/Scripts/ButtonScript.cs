using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    // Update is called once per frame
    public void LoadScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}