using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��������Լ��ʯ�������

public class SlabStoneContainer : MonoBehaviour
{
    public List<ReductionSlabstone> slabstones;
    public ReductionSlabstone selectedSlabStone = null;
    private int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        slabstones = new List<ReductionSlabstone>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void SwitchSelectSlabStone()
    {
        if (slabstones.Count == 0)
        {
            selectedSlabStone = null; // ����б�Ϊ�գ���ѡ�е� SlabStone Ϊ null
            currentIndex = -1;
            return;
        }

        // ���� currentIndex������ﵽ�б��ĩβ����ص��б�Ŀ�ͷ
        currentIndex = (currentIndex + 1) % slabstones.Count;
        selectedSlabStone = slabstones[currentIndex];
    }
}
