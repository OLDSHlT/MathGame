using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathStickLoader : MonoBehaviour
{
    public List<Sprite> verticalSticks;
    public List<Sprite> horizontalSticks;
    public Sprite emptyStick;
    // Start is called before the first frame update
    void Start()
    {
        emptyStick = Resources.Load<Sprite>("empty");
        verticalSticks = new List<Sprite>();
        horizontalSticks = new List<Sprite>();

        verticalSticks.Add(Resources.Load<Sprite>("1-vertical"));
        verticalSticks.Add(Resources.Load<Sprite>("2-vertical"));
        verticalSticks.Add(Resources.Load<Sprite>("3-vertical"));
        verticalSticks.Add(Resources.Load<Sprite>("4-vertical"));
        verticalSticks.Add(Resources.Load<Sprite>("5-vertical"));
        verticalSticks.Add(Resources.Load<Sprite>("6-vertical"));
        verticalSticks.Add(Resources.Load<Sprite>("7-vertical"));
        verticalSticks.Add(Resources.Load<Sprite>("8-vertical"));
        verticalSticks.Add(Resources.Load<Sprite>("9-vertical"));

        horizontalSticks.Add(Resources.Load<Sprite>("1-horizontal"));
        horizontalSticks.Add(Resources.Load<Sprite>("2-horizontal"));
        horizontalSticks.Add(Resources.Load<Sprite>("3-horizontal"));
        horizontalSticks.Add(Resources.Load<Sprite>("4-horizontal"));
        horizontalSticks.Add(Resources.Load<Sprite>("5-horizontal"));
        horizontalSticks.Add(Resources.Load<Sprite>("6-horizontal"));
        horizontalSticks.Add(Resources.Load<Sprite>("7-horizontal"));
        horizontalSticks.Add(Resources.Load<Sprite>("8-horizontal"));
        horizontalSticks.Add(Resources.Load<Sprite>("9-horizontal"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // �����ļ�·������ͼƬΪ Sprite ����
    private Sprite LoadSpriteFromPath(string path)
    {
        // ʹ�� Unity �� API ����ͼƬ
        Texture2D texture = LoadTextureFromPath(path);
        if (texture != null)
        {
            // ���� Sprite ����
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
        return null;
    }

    // �����ļ�·������ͼƬΪ Texture2D ����
    private Texture2D LoadTextureFromPath(string path)
    {
        // ʹ�� Unity �� API ����ͼƬ
        Texture2D texture = null;
        byte[] fileData;

        if (System.IO.File.Exists(path))
        {
            fileData = System.IO.File.ReadAllBytes(path);
            texture = new Texture2D(2, 2);
            texture.LoadImage(fileData); // �Զ�����ͼƬ�ߴ�
        }
        else
        {
            Debug.LogError("File not found: " + path);
        }

        return texture;
    }
}
