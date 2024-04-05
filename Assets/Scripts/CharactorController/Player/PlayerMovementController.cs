
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
    public float attackCD = 0.5f;
    public float recoverDuration = 1f;
    public float recoverCD = 0.5f;
    public float jumpForce = 10f;
    Vector2 movement = new Vector2();
    private TouchingDetactor touchingDetactor;

    Animator animator;
    Rigidbody2D rb2d;
    AnimatorStateInfo state;
    //��Ծʱ���ʱ��
    private float jumpTime = 0f; //��ס�ո�������ʱ��
    private float maxJumpTime = 0.2f; //������Ծʱ��
    private bool isJumpAllow = true;
    //����״̬�Ĵ�����
    private bool isAttackCD = false;
    private bool isSprinting = false;
    private bool isSprintCD = false;
    private bool isRunning = false;
    private bool isJumping = false;
    private bool isStickOnWall = false;
    private bool isRecovering = false;
    private bool isRecoverCD = false;
    private bool isAttacking = false;
    public bool isFalling = false;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.rb2d = GetComponent<Rigidbody2D>();
        this.touchingDetactor = GetComponent<TouchingDetactor>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        UpdateJumpState();
        MovePlayer();
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
            //����ɫλ��ǽ��
            //����ɫһ��������б�Ϸ�����
            if(gameObject.transform.localScale.x > 0)
            {
                //������
                rb2d.AddForce(new Vector2(0, 3));
                rb2d.velocity = new Vector2(-5, rb2d.velocity.y);
                movement.x = -5;
            }
            else
            {
                rb2d.AddForce(new Vector2(0, 3));
                rb2d.velocity = new Vector2(5, rb2d.velocity.y);
                movement.x = 5;
            }
        }
        //overturn the spirit
        TurnCheck();
        
        
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
        }
        else
        {
            isFalling = false;
        }
        if (Input.GetKey(KeyCode.Q) && !this.isRecoverCD && !this.isRunning && !this.isRecovering)
        {
            this.isRecovering = true;
            StartCoroutine(RecoverCounter());
        }
        
        animator.SetBool("isSprinting", isSprinting);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isStickOnWall", isStickOnWall);
        animator.SetBool("isRecovering", isRecovering);

        if (Input.GetKey(KeyCode.X) || isAttacking)
        {
            //����������������ڹ���ʱ����
            Attack();
        }

    }
    //��������
    private void Attack()
    {
        if (!isAttacking && !isAttackCD)
        {
            //û�����ڽ��еĹ�����û��CD
            this.isAttacking = true;
            if(Random.Range(0,2) == 0)
            {
                animator.SetBool("attack1", true);
            }
            else
            {
                animator.SetBool("attack2", true);
            }
        }
        else
        {
            //�����ڽ��еĶ������ж��Ƿ񲥷����
            state = animator.GetCurrentAnimatorStateInfo(0);
            if(state.IsName("attack1") && state.normalizedTime >= 1.0f)
            {
                //�������
                this.isAttacking = false;
                animator.SetBool("attack1", false);
                isAttackCD = true;
                StartCoroutine(AttackCounter());
            }
            else if(state.IsName("attack2") && state.normalizedTime >= 1.0f)
            {
                //�������
                this.isAttacking = false;
                animator.SetBool("attack2", false);
                isAttackCD = true;
                StartCoroutine(AttackCounter());
            }
        }
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
    private IEnumerator RecoverCounter()
    {
        if (isRecovering)
        {
            yield return new WaitForSeconds(this.recoverDuration);
            this.isRecovering = false;
            this.isRecoverCD = true;
            yield return new WaitForSeconds(this.recoverCD);
            this.isRecoverCD = false;
        }
    }
    private IEnumerator AttackCounter()
    {
        if (isAttackCD)
        {
            yield return new WaitForSeconds(this.attackCD);
            this.isAttackCD = false;
        }
    }
}
