using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Vector3 playerPos;
    public GameObject[] waterdrops;
    public Camera mainCamera;

    public Vector3 I_error;

    public int score = 0;
    public int index = 0;
    int cnt = 0;

    Vector3[] initPos = new [] {
        new Vector3(-4, -3, 0), new Vector3(0, -3, 0), new Vector3(4, -3, 0)
        };

    // Start is called before the first frame update
    void Awake()
    {
        playerPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKey(KeyCode.LeftArrow) == true)){
            index = 0;
            cnt = 0;
        }
        else if((Input.GetKey(KeyCode.RightArrow) == true)){
            index = 2;
            cnt = 0;
        }
        else{
            index = 1;
        }

        Vector3 error = initPos[index] - transform.position;
        I_error += error;
        transform.position += Time.deltaTime * (30 * error + I_error); 
        cnt++;
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "obstacle"){
            Debug.Log("충돌");
            SceneManager.LoadScene("LastScene");
            
        }
        if(other.tag == "waterdrop"){
            Debug.Log("score + 1");
            score++;
            other.gameObject.SetActive(false);
        } 
    }
    
}
