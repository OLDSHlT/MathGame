using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectGameEvent : MonoBehaviour
{
    /**
     * ��ÿ��ѡ��ؿ��İ�ť���ø����
     * �����ֵΪ-1ʱΪ���Թؿ�
     * 0-3Ϊ��Ϸ���ĸ��ؿ�
     */
    public int sceneNumber;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void goToGame()
    {
        if(sceneNumber == -1)
        {
            SceneManager.LoadScene("TestScene");
        }
    }

}
