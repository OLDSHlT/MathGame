using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetactionArea : MonoBehaviour
{
    private Collider2D detactZone;
    public Transform target;
    public bool isTargetEnter = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // �����봥��������ײ���Ƿ���Ŀ�����Ϸ����
        if (collider.transform.gameObject == target.gameObject)
        {
            isTargetEnter = true;
            //Debug.Log("The target entered the trigger!");
            // ������ִ��Ŀ����봥�������߼�
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        // �����봥��������ײ���Ƿ���Ŀ�����Ϸ����
        if (collider.transform.gameObject == target.gameObject)
        {
            isTargetEnter = false;
            // ������ִ��Ŀ����봥�������߼�
        }
    }
}
