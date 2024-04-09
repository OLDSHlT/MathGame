using fractionProcessor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class XuanShuFractionComponent : MonoBehaviour
{
    public Image numeratorTen; //����ʮλ
    public Image numeratorUnit; //���Ӹ�λ
    public Image denominatorTen; //��ĸʮλ
    public Image denominatorUnit; //��ĸ��λ
    private FractionProcessor fractionProcessor;//����������
    public int numerator;
    public int denominator;
    public SlabStoneContainer playerFraction;
    Damageable damageable;

    List<Sprite> verticalSticks;
    List<Sprite> horizontalSticks;
    Sprite emptyStick;
    Transform shield;

    // Start is called before the first frame update
    void Start()
    {
        Transform canvas = transform.Find("FractionDisplay");
        Transform img1 = canvas.Find("Numerator-Ten");
        Transform img2 = canvas.Find("Denominator-Ten");
        Transform img3 = canvas.Find("Numerator-Unit");
        Transform img4 = canvas.Find("Denominator-Unit");
        this.numeratorTen = img1.GetComponent<Image>();
        this.denominatorTen = img2.GetComponent<Image>();
        this.numeratorUnit = img3.GetComponent<Image>();
        this.denominatorUnit = img4.GetComponent<Image>();
        this.damageable = GetComponent<Damageable>();
        this.shield = transform.Find("Shield");

        int randomInt = Random.Range(0, 10);
        //���ݲ�ͬ����������2����3��������
        if (randomInt >= 5)
        {
            fractionProcessor = new FractionProcessor(new FractionGeneratorWith3());
        }
        else
        {
            fractionProcessor = new FractionProcessor(new FractionGeneratorWith2());
        }

        emptyStick = LoadSpriteFromPath("Assets/Art/MathSticks/empty.png");
        verticalSticks = new List<Sprite>();
        horizontalSticks = new List<Sprite>();

        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/1-vertical.png"));
        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/2-vertical.png"));
        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/3-vertical.png"));
        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/4-vertical.png"));
        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/5-vertical.png"));
        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/6-vertical.png"));
        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/7-vertical.png"));
        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/8-vertical.png"));
        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/9-vertical.png"));

        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/1-horizontal.png"));
        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/2-horizontal.png"));
        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/3-horizontal.png"));
        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/4-horizontal.png"));
        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/5-horizontal.png"));
        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/6-horizontal.png"));
        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/7-horizontal.png"));
        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/8-horizontal.png"));
        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/9-horizontal.png"));

        SetCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFraction();
    }
    private void UpdateFraction()
    {
        this.numerator = fractionProcessor.GetDivisor();
        this.denominator = fractionProcessor.GetDividend();
        if (fractionProcessor.IsSimplestFraction())
        {
            //������
            damageable.isInvincible = false;
            this.shield.gameObject.SetActive(false);
        }
        else
        {
            damageable.isInvincible = true;
        }
        
    }
    void SetCanvas()
    {
        //���ݲ�ͬ����ʹ�ò�ͬ�����ͼƬ
        DisplayMathStick(fractionProcessor.GetDivisor() / 10, this.numeratorTen, 0); //����ʮλ
        DisplayMathStick(fractionProcessor.GetDivisor() % 10, this.numeratorUnit, 1); //���Ӹ�λ
        DisplayMathStick(fractionProcessor.GetDividend() / 10, this.denominatorTen, 0); //��ĸʮλ
        DisplayMathStick(fractionProcessor.GetDividend() % 10, this.denominatorUnit, 1); //��ĸ��λ
        //this.numeratorTen.sprite = LoadSpriteFromPath("Assets/Art/MathSticks/1-horizontal.png");
    }

    private void DisplayMathStick(int number , Image img , int mode)
    {
        //0 vertical
        //1 horizontal
        if(mode == 0)
        {
            DisplayMathStickVertical(number, img);
        }
        else
        {
            DisplayMathStickHorizontal(number, img);
        }
    }
    private void DisplayMathStickVertical(int number, Image img)
    {
        switch (number)
        {
            case 1:
                img.sprite = verticalSticks[0];
                break;
            case 2:
                img.sprite = verticalSticks[1];
                break;
            case 3:
                img.sprite = verticalSticks[2];
                break;
            case 4:
                img.sprite = verticalSticks[3];
                break;
            case 5:
                img.sprite = verticalSticks[4];
                break;
            case 6:
                img.sprite = verticalSticks[5];
                break;
            case 7:
                img.sprite = verticalSticks[6];
                break;
            case 8:
                img.sprite = verticalSticks[7];
                break;
            case 9:
                img.sprite = verticalSticks[8];
                break;
            default:
                img.sprite = emptyStick;
                break;
        }
    }
    private void DisplayMathStickHorizontal(int number, Image img)
    {
        switch (number)
        {
            case 1:
                img.sprite = horizontalSticks[0];
                break;
            case 2:
                img.sprite = horizontalSticks[1];
                break;
            case 3:
                img.sprite = horizontalSticks[2];
                break;
            case 4:
                img.sprite = horizontalSticks[3];
                break;
            case 5:
                img.sprite = horizontalSticks[4];
                break;
            case 6:
                img.sprite = horizontalSticks[5];
                break;
            case 7:
                img.sprite = horizontalSticks[6];
                break;
            case 8:
                img.sprite = horizontalSticks[7];
                break;
            case 9:
                img.sprite = horizontalSticks[8];
                break;
            default:
                img.sprite = emptyStick;
                break;
        }
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
    public void OnHit(int damage, Vector2 knockback)//��������
    {
        if(playerFraction != null)
        {
            //Debug.Log("fraction counter");
            if (playerFraction.selectedSlabStone != null)
            {
                fractionProcessor.Reduction(playerFraction.selectedSlabStone.reductionNumber);
            }
        }
        SetCanvas();//���»���
    }
}
