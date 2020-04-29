using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public Text score;
    public Player Player;
    public static string str;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        str = "Score : " + Player.score;
        score.text = str;
    }
}
