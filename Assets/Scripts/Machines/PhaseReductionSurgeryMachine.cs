using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��������������ű�
public class NewBehaviourScript : MonoBehaviour
{
    public bool isActive = false;
    // Start is called before the first frame update
    public Canvas UI;
    void Start()
    {
        UI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                UpdateInput();
        }
    }
    public void OnActive()//����������Ļص�����
    {
        Time.timeScale = 0f;
        this.isActive = true;
        UI.gameObject.SetActive(true);
    }
    void Deactivation()//�����ر�
    {
        Time.timeScale = 1.0f;
        this.isActive = false;
        UI.gameObject.SetActive(false);
    }
    public void UpdateInput()
    {
        
        {
            //�˳�����
            Deactivation();
        }
    }
}
