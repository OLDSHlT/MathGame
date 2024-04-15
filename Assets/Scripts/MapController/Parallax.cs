using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Transform PlayerPosStart;
    Vector3 SpritePosStart;
    public Transform TargetPosX;//��Զ��
    public Transform TargetPosY;//��ߵ�
    float TotalDistanceX;//��ҳ�ʼλ�ú�ͼƬ�յ��x����
    float TotalDistanceY;//��ҳ�ʼλ�ú�ͼƬ�յ��y����
    public Transform PosX1;
    public Transform PosX2;
    public Transform PosY1;
    public Transform PosY2;
    //����Y������Ӳ�����
    public bool usingYParallax=false;//�Ƿ�����Y�����ϵ��Ӳ�
    public float ParallaxY = 0.5f;//Y�����Ӳ������ϵ��
    //public bool isX = true;
    void Awake()
    {
        PlayerPosStart = GameObject.Find("Main Camera").GetComponent<Transform>();
        TotalDistanceX=TargetPosX.transform.position.x-PlayerPosStart.transform.position.x;
        TotalDistanceY = TargetPosY.transform.position.y - PlayerPosStart.transform.position.y;
        SpritePosStart = transform.position;
    }
    float Wide;//�ƶ���Ŀ��λ�ú�ԭ��ĳһλ�ڱ���Pos1���ӽǻ��ƶ�������Pos2
    float High;
    void Start()
    {   
        Wide = PosX2.transform.position.x - PosX1.transform.position.x;    
        High = PosY2.transform.position.y - PosY1.transform.position.y;
    }

    float ToTargetDistanceX;
    float ToTargetDistanceY;
    void Update()
    {
        Transform PlayerPos = GameObject.Find("Main Camera").GetComponent<Transform>();
        ToTargetDistanceX = TargetPosX.transform.position.x - PlayerPos.transform.position.x;//��ҵ�Ŀ���x����
        ToTargetDistanceY = TargetPosY.transform.position.y - PlayerPos.transform.position.y;//��ҵ�Ŀ���y����
        float ParallaxDistanceX = (1 - ToTargetDistanceX / TotalDistanceX) * Wide;//�Ӳ�x����
        float ParallaxDistanceY = (1 - ToTargetDistanceX / TotalDistanceX) * High;//�Ӳ�y����
        transform.position = SpritePosStart +new Vector3(( -1*ParallaxDistanceX + (TotalDistanceX-ToTargetDistanceX)),0,0);//X�Ӳ�

        if (usingYParallax)//Y�Ӳ�
        {
            transform.position = SpritePosStart
            + new Vector3((-1 * ParallaxDistanceX + (TotalDistanceX - ToTargetDistanceX)), (-1 * ParallaxDistanceY + TotalDistanceY - ToTargetDistanceY) * ParallaxY, 0);
        }
        else 
        {
            transform.position = SpritePosStart+ new Vector3((-1 * ParallaxDistanceX + (TotalDistanceX - ToTargetDistanceX)), 0, 0);
        }
    }
}
