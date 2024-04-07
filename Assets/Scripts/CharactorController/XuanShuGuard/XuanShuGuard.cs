using fractionProcessor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XuanShuGuard : MonoBehaviour
{
    public float speed = 2f;
    public DetactionArea warningZone; //�趨��Ѳ�߷�Χ
    public float minDistance = 1f;
    public float attackCD = 1.5f;

    private AnimatorStateInfo state; //����״̬��״̬
    private bool isAttackCDing = false;
    private bool isAttacking = false;
    public bool isAttackKeyFrame = false;

    private AttackModes attackMode = AttackModes.ShortRange;
    private Rigidbody2D rb2d;
    private Vector2 movement;
    private bool isPlayerInTrigger = false;
    private Animator animator;
    private Damageable damageable;
    private enum AttackModes{
        LongRange = 0,
        ShortRange = 1
    }
    // Start is called before the first frame update
    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.damageable.isAlive && !damageable.isUnderAttackCooldown)
        {
            Move();
        }
        UpdateStatus();
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
    private void Move()
    {
        
        if (warningZone != null)
        {
            float distance = 0f;
            if (warningZone.target != null)
            {
                distance = Vector2.Distance(this.transform.position, warningZone.target.transform.position);
            }
            //��Ѳ�߷�Χ
            if (warningZone.isTargetEnter)
            {
                if (attackMode == AttackModes.ShortRange)
                {
                    //��սģʽ
                    //�����Ҳ��ڹ�����Χ��
                    //�������
                    if (!isPlayerInTrigger && !isAttacking || isAttackCDing && distance >= minDistance)
                    {
                        if (this.transform.position.x < warningZone.target.transform.position.x)
                        {
                            movement = new Vector2(1, 0);
                        }
                        else
                        {
                            movement = new Vector2(-1, 0);
                        }
                    }
                    else
                    {
                        AttackShortRange();
                        movement = new Vector2();
                    }
                }
                
            }
            else
            {
                movement = new Vector2();
            }
        }
        
        rb2d.velocity = new Vector2(movement.x * speed, rb2d.velocity.y);
        TurnCheck();
    }
    //ʹ�ô������ж�����Ƿ�����˽�ս������Χ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.isPlayerInTrigger = true;
        }
    }
    //�ж�����Ƿ��뿪�˽�ս������Χ
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.isPlayerInTrigger = false;
        }
    }
    private void UpdateStatus()
    {
        if(movement.x != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        animator.SetBool("isAttacking", this.isAttacking);
        if (!damageable.isAlive)
        {
            animator.SetBool("isDead", true);
            animator.SetBool("isWalking", false);
            Die();
        }
    }

    private void Die()
    {
        //����
        state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("die") && state.normalizedTime >= 1.0f)
        {
            //�����������
            Destroy(gameObject);
        }
    }

    private void AttackShortRange()
    {
        if (!isAttackCDing)
        {
            //û��CD
            if (!isAttacking)
            {
                //û���Ѿ���ʼ�Ĺ���
                this.isAttacking = true;
            }
            else
            {
                //���Ѿ���ʼ�Ĺ������жϽ���
                state = animator.GetCurrentAnimatorStateInfo(0);
                if(state.IsName("attack") && state.normalizedTime >= 1.0f)
                {
                    //�����������
                    this.isAttacking = false;
                    //��ʼʹ��Э�̼���CD
                    this.isAttackCDing = true;
                    StartCoroutine(AttackCDControl());
                }
                if (isAttackKeyFrame)
                {
                    //����˺�
                    if (this.isPlayerInTrigger)
                    {
                        Damageable d = warningZone.target.GetComponent<Damageable>();//��ȡdamageable���
                        if (warningZone.target.transform.position.x - this.transform.position.x < 0)
                        {
                            d.Hit(20, new Vector2(-5, 2));
                        }
                        else
                        {
                            d.Hit(20, new Vector2(5, 2));
                        }
                    }
                }
            }
        }
    }
    //���㹥��CD��Э��
    private IEnumerator AttackCDControl()
    {
        if (this.isAttackCDing)
        {
            yield return new WaitForSeconds(this.attackCD);
            this.isAttackCDing = false;
        }
    }
}
