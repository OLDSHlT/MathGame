using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class CharactorUIEvents : MonoBehaviour
{   //���˵Ķ�����������ֵ
    public static UnityAction<GameObject, int> characterDamaged;
    //�ָ��Ķ�����ָ���ֵ
    public static UnityAction<GameObject, int> characterHealed;
    //�������ֵ�仯
    public static UnityAction<GameObject, int> characterMaxHealthChange;
    //public static UnityAction<Transform, string> controlTextEnable;
}
