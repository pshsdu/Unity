using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pebblesPrefab = null;
    public GameObject dropPrefab = null;
    public GameObject player;
    
    public GameObject[] pebbles;
    public GameObject[] waterdrops;

    public Vector3[] pebblesPosition;
    public Vector3[] pebblesScale;

    public Vector3[] waterdropPosition;
    public Vector3[] waterdropScale;

    Vector3[] initPos = new [] {new Vector3(-4, 4, 0), new Vector3(0, 4, 0), new Vector3(4, 4, 0)};
    
    public Vector3 pebbles_I_err;
    public Vector3 waterdrop_I_err;

    public Vector3 waterdrop_I_scale_err;
    public Vector3 pebbles_I_scale_err;

    int[] pebblesInitPos;
    int[] waterDropInitPos;
    int numOfPebbles;
    int curActivePebbles = 0;
    int curActiveWaterdrop = 0;

    public float speed = 0.01f;
    public float incresingScaleSpeed = 0.01f;
    public int timerCntB = 0;
    public int timerCntA = 0;
    public int timerCnt = 0;

    public Text level;

    // Start is called before the first frame update
    void Awake()
    {
        pebblesPrefab = Resources.Load<GameObject>("Prefab/Pebbles");
        dropPrefab = Resources.Load<GameObject>("Prefab/Waterdrop");

        pebbles = new GameObject[20];
        waterdrops = new GameObject[20];

        pebblesInitPos = new int[20];
        waterDropInitPos = new int[20];

        pebblesPosition = new Vector3[20];
        waterdropPosition = new Vector3[20];

        pebblesScale = new Vector3[20];
        waterdropScale = new Vector3[20];

        for(int i = 0; i < pebbles.Length; i++)
        {
            pebbles[i] = GameObject.Instantiate<GameObject>(pebblesPrefab);
            waterdrops[i] = GameObject.Instantiate<GameObject>(dropPrefab);
            //부모 자식관의 관계보다는 trnasform의 형태에 더 가까움
            pebbles[i].transform.SetParent(transform);
            waterdrops[i].transform.SetParent(transform);
            //gameobject를 키고 꺼는 기능
            pebbles[i].SetActive(false);
            waterdrops[i].SetActive(false);
        }

        MakeRandomObject();
    }

    int getRandomNum(int num){
        int[] index = new [] {0, 1, 2};

        int tmp = index[num];
        
        index[num] = index[2];
        index[2] = tmp;

        int randomIndex = Random.Range(0,2);
        
        return index[randomIndex];
    }

    void MakeRandomObject()
    {
        int numOfObj = Random.Range(1, 3);
        int cnt = 0;

        numOfPebbles = Random.Range(1, numOfObj);
 
        for(int i = curActivePebbles; i < curActivePebbles + numOfPebbles; i++){
            //Debug.Log("curActivePebbles : "+i);
            pebblesInitPos[i] = Random.Range(0, 3);
            if(i > 0)    pebblesInitPos[i] = getRandomNum(pebblesInitPos[i-1]);

            pebbles[i].transform.position = initPos[pebblesInitPos[i]];
            pebblesPosition[i] = pebbles[i].transform.position;
            pebbles[i].SetActive(true);
            pebbles[i].transform.localScale = new Vector3(0.1f, 0.1f, 0f);
            pebblesScale[i] = pebbles[i].transform.localScale;
            cnt++;
        }

        for(int i = curActiveWaterdrop; i < curActiveWaterdrop + numOfObj - numOfPebbles; i++){
            //Debug.Log("pebble init position : " + pebblesInitPos[curActivePebbles]);

            if(cnt == 2){
                waterDropInitPos[i] = 3 - (pebblesInitPos[curActivePebbles] + pebblesInitPos[curActivePebbles + 1]);
            }else if(cnt == 1){
                waterDropInitPos[i] = getRandomNum(pebblesInitPos[curActivePebbles]);
            }else{
                waterDropInitPos[i] = Random.Range(0, 3);
            }

            waterdrops[i].transform.position = initPos[waterDropInitPos[i]];
            waterdropPosition[i] = waterdrops[i].transform.position;
            waterdrops[i].SetActive(true);
            waterdrops[i].transform.localScale = new Vector3(0.1f, 0.1f, 0f);
            waterdropScale[i] = waterdrops[i].transform.localScale;
        }

        curActivePebbles += numOfPebbles;
        curActiveWaterdrop += (numOfObj - numOfPebbles);
    }

    void updateScale(){
        for(int i=0; i<20; i++){
            if(pebbles[i].transform.localScale.x <= 0.2){
                //scale update
                pebblesScale[i].x += incresingScaleSpeed * Time.deltaTime;
                pebblesScale[i].y += incresingScaleSpeed * Time.deltaTime;

                //scale 변화를 부드럽게 하기 위한 PI 제어
                Vector3 error = pebblesScale[i] - pebbles[i].transform.localScale;
                pebbles_I_scale_err += error;
                pebbles[i].transform.localScale += Time.deltaTime * (3 * error + pebbles_I_scale_err);
            }

            if(waterdrops[i].transform.localScale.x <= 0.3){
                waterdropScale[i].x += incresingScaleSpeed * Time.deltaTime;
                waterdropScale[i].y += incresingScaleSpeed * Time.deltaTime;
                
                //scale 변화를 부드럽게 하기 위한 PI 제어
                Vector3 error = waterdropScale[i] - waterdrops[i].transform.localScale;
                waterdrop_I_scale_err += error;
                waterdrops[i].transform.localScale += Time.deltaTime * (3 * error + waterdrop_I_scale_err);
            }
        }
    }

    void UpdatePosition(){
        for(int i=0; i<20; i++){
            if(pebbles[i].transform.position.y >= -7){
                pebblesPosition[i].y -= speed * Time.deltaTime;
                
                Vector3 error = pebblesPosition[i] - pebbles[i].transform.position;
                pebbles_I_err += error;
                pebbles[i].transform.position += Time.deltaTime * (30 * error + pebbles_I_err); 
            }
       
            if(waterdrops[i].transform.position.y >= -7){
                waterdropPosition[i].y -= speed * Time.deltaTime;

                Vector3 error = waterdropPosition[i] - waterdrops[i].transform.position;
                waterdrop_I_err += error;
                waterdrops[i].transform.position += Time.deltaTime * (30 * error + waterdrop_I_err); 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( ((pebbles[curActivePebbles-1].transform.position.y - player.transform.position.y) <= 4)){
            if(curActivePebbles >= 19){
                curActivePebbles = 0;
            }

            if(curActiveWaterdrop >= 19){
                curActiveWaterdrop = 0;
            }  
            MakeRandomObject();
        }

        if(timerCntB++ >= 3){
            updateScale();
            timerCntB = 0;
        }

        if(timerCntA++ >= 2){
            UpdatePosition();
            timerCntA = 0;
        }
    }
}
