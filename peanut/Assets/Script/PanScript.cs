using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanScript : MonoBehaviour
{
    public int PanCnt = 10;
    public GameObject PanPrefab = null;

    public GameObject[] RightArrPan = null;
    public GameObject[] LeftArrPan = null;

    public Vector3 RightLastPanPos;
    public Vector3 LeftLastPanPos;
    public GameObject Player = null;
    
    int m_CurRightActivePan = 0;
    int m_CurLeftActivePan = 0;

    // Start is called before the first frame update
    void Awake(){

        PanPrefab = Resources.Load<GameObject>("Prefab/ActivePan");

        if(null == PanPrefab)
        {
            print("Err : Prefab does not exist");
            return;
        }

        if(null == Player)
        {
            print("Err : Player does not exist");
            return;
        }

        RightLastPanPos = new Vector3(transform.position.x + 4f, Player.transform.position.y-4, Player.transform.position.z + 0f);
        LeftLastPanPos = new Vector3(transform.position.x , Player.transform.position.y-4, Player.transform.position.z + 0f);

        //게임오브젝트를 담을 수 있는 30개의 칸을 만듦
        RightArrPan = new GameObject[PanCnt];
        LeftArrPan = new GameObject[PanCnt];

        for(int i = 0; i < PanCnt; i++)
        {
            RightArrPan[i] = GameObject.Instantiate<GameObject>(PanPrefab);
            LeftArrPan[i] = GameObject.Instantiate<GameObject>(PanPrefab);

            //부모 자식관의 관계보다는 trnasform의 형태에 더 가까움
            RightArrPan[i].transform.SetParent(transform);
            LeftArrPan[i].transform.SetParent(transform);

            //gameobject를 키고 꺼는 기능
            RightArrPan[i].SetActive(false);
            LeftArrPan[i].SetActive(false);
        }
    }
        //플레이어의 위치를 파악하여 판이 이동되어야함
        //어떤 기준에 의해 이동되어야하는가
    void Update() 
    {   
        Vector3 randomLength;
        Vector3 randomRot;
    }
}