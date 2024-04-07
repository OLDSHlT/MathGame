using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����ű����𹥻��ͻָ��ȶ���
public class Player : MonoBehaviour
{
    Damageable damageable;
    AnimatorStateInfo state;
    Animator animator;
    PlayerMovementController movementController;
    AOEDamageZone damageZone;
    Rigidbody2D rb2d;
    SlabStoneContainer slabStoneContainer;

    public bool isAttacking = false;
    private bool isAttackCD = false;
    public bool isAttackKeyFrame = false;
    public bool isRecovering = false;
    private bool isRecoverCD = false;

    public float attackCD = 0.5f;
    public float recoverDuration = 1f;
    public float recoverCD = 0.5f;


    void Start()
    {
        this.damageable = GetComponent<Damageable>();
        this.animator = GetComponent<Animator>();
        this.movementController = GetComponent<PlayerMovementController>();
        this.damageZone = transform.Find("AttackArea").gameObject.GetComponent<AOEDamageZone>();
        this.rb2d = GetComponent<Rigidbody2D>();
        this.slabStoneContainer = GetComponent<SlabStoneContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        UpdateStatus();
    }

    void FixedUpdate()
    {
        
    }
    private void UpdateStatus()
    {
        animator.SetBool("isRecovering", isRecovering);
        if (damageable.isUnderAttackCooldown)
        {
            //������Ӳֱ
            animator.SetBool("isUnderAttack", true);
        }
        else
        {
            animator.SetBool("isUnderAttack", false);
        }
        if (!damageable.isAlive)
        {
            //die
            animator.SetBool("isSprinting", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", false);
            animator.SetBool("isStickOnWall", false);
            animator.SetBool("isUnderAttack", false);
            animator.SetBool("isDead", true);
            Die();
        }
    }
    private void UpdateInput()
    {
        if (Input.GetKey(KeyCode.X) || isAttacking)
        {
            //����������������ڹ���ʱ����
            Attack();
        }
        if (Input.GetKey(KeyCode.Q) && !this.isRecoverCD && !this.isRecovering && !movementController.isRunning)
        {
            this.isRecovering = true;
            StartCoroutine(RecoverCounter());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //�л�Լ��ʯ��
            slabStoneContainer.SwitchSelectSlabStone();
        }
        
    }
    //��������
    private void Attack()
    {
        if (!isAttacking && !isAttackCD)
        {
            //û�����ڽ��еĹ�����û��CD
            this.isAttacking = true;
            if (UnityEngine.Random.Range(0, 2) == 0)
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
            if (state.IsName("attack1") && state.normalizedTime >= 1.0f)
            {
                //�������
                this.isAttacking = false;
                animator.SetBool("attack1", false);
                isAttackCD = true;
                StartCoroutine(AttackCounter());
            }
            else if (state.IsName("attack2") && state.normalizedTime >= 1.0f)
            {
                //�������
                this.isAttacking = false;
                animator.SetBool("attack2", false);
                isAttackCD = true;
                StartCoroutine(AttackCounter());
            }
        }
        if (isAttackKeyFrame)
        {
            //λ�ڹ��ƹؼ�֡�У�����˺�
            foreach(GameObject enemy in damageZone.enemyList)
            {
                enemy.GetComponent<Damageable>().Hit(10, new Vector2(3,0));
            }
        }
    }
    private void Die()
    {
        rb2d.velocity = new Vector2(0, 0);
        //����
        state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("death") && state.normalizedTime >= 1.0f)
        {
            //�����������
            Destroy(gameObject);
        }
    }
    //���㹥��CD��Э��
    private IEnumerator AttackCounter()
    {
        if (isAttackCD)
        {
            yield return new WaitForSeconds(this.attackCD);
            this.isAttackCD = false;
        }
    }
    //����ָ���CDЭ��
    private IEnumerator RecoverCounter()
    {
        if (isRecovering)
        {
            yield return new WaitForSeconds(this.recoverDuration);
            this.isRecovering = false;

            damageable.Heal(10);

            this.isRecoverCD = true;
            yield return new WaitForSeconds(this.recoverCD);
            this.isRecoverCD = false;
        }
    }
}
