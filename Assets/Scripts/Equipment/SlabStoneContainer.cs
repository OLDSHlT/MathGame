using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

//��������Լ��ʯ�������

public class SlabStoneContainer : MonoBehaviour
{
    private List<ReductionSlabstone> slabstones = new List<ReductionSlabstone>();
    public ReductionSlabstone selectedSlabStone = null;
    int currentIndex;//
    public UnityEvent slabstonePick;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R) && selectedSlabStone != null)
        //    SwitchIndex();

    }
    public void AddSlabStone(ReductionSlabstone slabstone)
    {
        slabstones.Add(slabstone);
        if(selectedSlabStone == null || slabstones.Count == 0)
        {
            selectedSlabStone = slabstone;
            currentIndex = 0;
            
        }
    }
    public void RemoveSlabStone(ReductionSlabstone slabstone)
    {
        int index = slabstones.IndexOf(slabstone); // �ҵ�Ҫ�Ƴ��� SlabStone ���б��е�����
        if (index != -1)
        {
            slabstones.Remove(slabstone); // �Ƴ� SlabStone

            // ���� currentIndex
            if (index < currentIndex)
            {
                currentIndex--; // ������Ƴ��� SlabStone �ڵ�ǰѡ�е� SlabStone ֮ǰ������Ҫ�� currentIndex ��һ
            }
            else if (index == currentIndex)
            {
                currentIndex = 0; // ������Ƴ��� SlabStone �����ǵ�ǰѡ�е� SlabStone���� currentIndex ��Ϊ 0
                if (slabstones.Count > 0)
                {
                    selectedSlabStone = slabstones[currentIndex]; // ����ѡ�е� SlabStone
                }
                else
                {
                    selectedSlabStone = null; // ����б�Ϊ�գ���ѡ�е� SlabStone Ϊ null
                }
            }
        }
    }
    //public GameObject SlabStoneUI;
    public void SwitchSelectSlabStone()
    {   if (slabstones.Count == 1)//����ʱ
            slabstonePick?.Invoke();
        if (slabstones.Count == 0)
        {
            selectedSlabStone = null; // ����б�Ϊ�գ���ѡ�е� SlabStone Ϊ null
            currentIndex = -1;
            return;
        }

        // ���� currentIndex������ﵽ�б��ĩβ����ص��б�Ŀ�ͷ
        currentIndex = (currentIndex + 1) % slabstones.Count;
        selectedSlabStone = slabstones[currentIndex];
        int Num = selectedSlabStone.GetReductionNumber();
        ChosenNum = Num.ToString();
        TextChange();
    }

    string ChosenNum;
    public void SwitchIndex()
    {

        {
            if (currentIndex >= slabstones.Count-1 || slabstones.Count == 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
        }

        int Num = slabstones[currentIndex].GetReductionNumber();
        ChosenNum = Num.ToString();
        TextChange();
    }
    string[] TextNum = new string[] { "��", "һ", "��", "��", "��", "��", "��", "��", "��", "��" };
    string actualText;
    public TMP_Text SlabText;
    private void TextChange()
    {
        actualText = "";
        char[] Nums = new char[ChosenNum.Length];
        for (int i = 0; i < Nums.Length; i++)
        {
            Nums[i] = ChosenNum[i];
            char actualChar = Convert.ToChar(TextNum[Convert.ToInt32(Nums[i])-48]);
            actualText = actualText.PadRight(i+1, actualChar);
            //print(actualChar);
        }
        SlabText.text = actualText;
        
    }
}
