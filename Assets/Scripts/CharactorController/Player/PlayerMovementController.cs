
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 8.0f;
    public float sprintCD = 1.5f;
    public float sprintDuration = 0.1f;
    public float jumpForce = 10f;
    Vector2 movement = new Vector2();

    Transform viewPoint;
    TouchingDetactor touchingDetactor;
    Animator animator;
    Rigidbody2D rb2d;
    Damageable damageable;
    //��Ծʱ���ʱ��
    private float jumpTime = 0f; //��ס�ո�������ʱ��
    private float maxJumpTime = 0.2f; //������Ծʱ��
    private bool isJumpAllow = true;
    //����״̬�Ĵ�����
    private bool isSprinting = false;
    private bool isSprintCD = false;
    public bool isRunning = false;
    private bool isJumping = false;
    private bool isStickOnWall = false;
    private bool isWallJumping = false;

    public bool isFalling = false;

    //�ж����ﳯ��
    private bool _isFacingLeft = false;
    public bool isFacingLeft
    {
        get { return _isFacingLeft; }
        private set
        {
            if(this.transform.localScale.x > 0)
            {
                isFacingLeft = false;
            }
            else
            {
                isFacingLeft = true;
            }
        }
    }

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.rb2d = GetComponent<Rigidbody2D>();
        this.touchingDetactor = GetComponent<TouchingDetactor>();
        this.damageable = GetComponent<Damageable>();
        this.viewPoint = transform.Find("ViewPoint");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        UpdateJumpState();
        if (!damageable.isUnderAttackCooldown && !isWallJumping && damageable.isAlive)
        {
            MovePlayer();
        }
        //overturn the spirit
        TurnCheck();
    }
    private void FixedUpdate()
    {
        //MovePlayer();
    }
    private void MovePlayer()
    {
        //���
        if (Input.GetKey(KeyCode.LeftShift) && !isSprintCD)
        {
            this.isSprinting = true;
            //��������CD��Э��
            StartCoroutine(SprintCounter());
        }
        //�������ٶ�
        //�����ʱ�ٶ���ԭ�ٶȵ��屶
        float actualSpeed = this.movementSpeed;
        if (isSprinting)
        {
            actualSpeed = actualSpeed * 5;
        }
        else
        {
            actualSpeed = this.movementSpeed;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        //ת��Ϊ��λ����
        //movement.Normalize();
        movement.y = rb2d.velocity.y;
        //jump
        if (Input.GetKey(KeyCode.Space) && isJumpAllow)
        {
            movement.y = jumpForce;
        }else if (Input.GetKeyUp(KeyCode.Space) && jumpTime < maxJumpTime)
        {
            //������Ծʱ��С�������Ծʱ��ʱ�ɿ�����Ծ��
            movement.y = 0;
        }
        //��ǽ��
        if (Input.GetKeyDown(KeyCode.Space) && !touchingDetactor.isGrounded && touchingDetactor.isWall)
        {
            isWallJumping = true;
            //����ɫλ��ǽ��
            //����ɫһ��������б�Ϸ�����
            if(gameObject.transform.localScale.x > 0)
            {
                //������
                rb2d.AddForce(new Vector2(0, 5));
                rb2d.velocity = new Vector2(-2, rb2d.velocity.y);
                movement.x = -1;
            }
            else
            {
                rb2d.AddForce(new Vector2(0, 3));
                rb2d.velocity = new Vector2(2, rb2d.velocity.y);
                movement.x = 1;
            }
            StartCoroutine(WallJumpingCounter());
        }        
        
        rb2d.velocity = new Vector2(movement.x * actualSpeed, movement.y);
        
        
    }
    private void UpdateJumpState()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //�ɿ��ո�ʱ������Ծ ��ֹ������
            this.isJumpAllow = false;
        }else if (Input.GetKey(KeyCode.Space))
        {
            //��ס�ո�ʱ
            if (touchingDetactor.isGrounded)
            {
                //�ڵ���ʱ
                this.isJumpAllow = true;
            }
            else if (!touchingDetactor.isGrounded && jumpTime < maxJumpTime)
            {
                //û�ڵ��棬����û�ﵽ�����Ծʱ��
                jumpTime = jumpTime + Time.deltaTime; //�ۼ�ʱ��
                //this.isJumpAllow = true;
            }
            else
            {
                //û�ڵ����ҳ��������Ծʱ��
                this.isJumpAllow = false;
            }
            
        }
        else if(touchingDetactor.isGrounded)
        {
            //�ڵ���ʱ
            this.jumpTime = 0f;
            isJumpAllow = true;
        }
    }
    private void TurnCheck()
    {
        if (movement.x > 0 && transform.localScale.x < 0)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
        }
        else if (movement.x < 0 && transform.localScale.x > 0)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
        }
    }
    private void UpdateState()
    {
        if(movement.x != 0 && touchingDetactor.isGrounded)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        if (!touchingDetactor.isGrounded)
        {
            //�����ж��Ƿ������䣨���޸ģ�
            this.isJumping = true;
            
        }
        else
        {
            this.isJumping = false;
        }
        if (rb2d.velocity.y <= 0 && !touchingDetactor.isGrounded)
        {
            isFalling = true;
            this.viewPoint.transform.localPosition = new Vector2(0, -2);//�ı��������λ�ã��û����ܹ���������
        }
        else
        {
            isFalling = false;
            this.viewPoint.transform.localPosition = new Vector2(0, 3);
        }
        if (touchingDetactor.isWall && isFalling)
        {
            isStickOnWall = true;
        }
        else
        {
            isStickOnWall = false;
        }
        
        
        animator.SetBool("isSprinting", isSprinting);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isStickOnWall", isStickOnWall);
        


    }

    
    //Э�����ڼ�ʱ
    private IEnumerator SprintCounter()
    {
        if (isSprinting)
        {
            yield return new WaitForSeconds(this.sprintDuration);
            this.isSprinting = false;
            this.isSprintCD = true;
            yield return new WaitForSeconds(this.sprintCD);
            this.isSprintCD = false;
        }
    }
    private IEnumerator WallJumpingCounter()
    {
        if (isWallJumping)
        {
            yield return new WaitForSeconds(0.15f);
            this.isWallJumping = false;
        }
    }
    
}
