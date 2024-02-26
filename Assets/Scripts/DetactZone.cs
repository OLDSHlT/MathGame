using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetactionZone : MonoBehaviour
{
    public Transform selftransform;
    public UnityEvent noCollidersRemain;
    public UnityEvent EnterEvent;
    public UnityEvent InStayEvent;
    public UnityEvent StayEvent;
    public List<Collider2D> collider2Ds = new List<Collider2D>();
    public bool InTriggerStay = false;
    public bool isTriggerEnter = false;
    public bool isTriggerExit = false;
    public bool forStay = false;
    Collider2D col;
    // Start is called before the first frame update
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collider2Ds.Add(collision);
        if (isTriggerEnter)
        {
            EnterEvent?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collider2Ds.Remove(collision);
        if (collider2Ds.Count <= 0&& isTriggerExit)
        {
            noCollidersRemain?.Invoke();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)//
    {

        if (InTriggerStay)//��Ҫ����ת��
        {
            Vector2 DirectionVector = (collision.transform.position - transform.position).normalized;//ȡ��Ŀ�굽���������
            //Debug.Log(DirectionVector);
            if (DirectionVector.x < 0 && selftransform.localScale.x > 0)//���屳��Ŀ��ʱ���ܴ���
            {
                InStayEvent?.Invoke();//�����϶�����ת����
            }
            else if (DirectionVector.x > 0 && selftransform.localScale.x < 0)//��ͬ
            {
                InStayEvent?.Invoke();
            }
        }
        if (forStay)//����ת�����⣬��Ŀ�����������ʱÿһ֡���ϵ��õ��¼���������ʱ�ò���
        {
            StayEvent?.Invoke();
        }
    }
}
