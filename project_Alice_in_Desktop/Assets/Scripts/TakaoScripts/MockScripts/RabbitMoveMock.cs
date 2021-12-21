using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMoveMock : MonoBehaviour
{
    RabbitMovePointMock nowPoint;
    [SerializeField] RabbitMovePointMock pointMock;
    //[SerializeField] PlayerCoreMock player; //Mock�Ȃ̂őg�ݍ��ލۂ͕ύX�K�{
    [SerializeField] PlayerPositionGet player;//Mock�Ȃ̂őg�ݍ��ލۂ͕ύX�K�{
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] GameObject childSpring;

    private Animator animator;
    private Animator childAnimator;
    private Rigidbody2D rigd2D;
    Vector3 playerPos;
    Vector3 nowTransform;
    Vector3 nextTransform; //���̏ꏊ
    Vector3 nextPosLength; //���̍��W�ƍ��̍��W�̍���ۑ����邽�߂�Vector

    int jumpCount = 0;
    const float DIVISION_NUM = 50f; //������
    float[] jumpTrajectory; //�A�j���[�V�����J�[�u�̃W�����v�̋O�Ղ�z��ɂ�������
    float toPlayerDistance; //�v���C���[�Ƃ̋���
    float alertRange = 7f; //�x���͈�
    float jumpPower = 30f;

    bool startFlg = false; //�ړ��J�n���邩�ǂ���
    bool stopFlg = false;
    bool playFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        childAnimator = childSpring.GetComponent<Animator>();
        rigd2D = GetComponent<Rigidbody2D>();
        //�A�j���[�V�����J�[�u�̐ݒ�,�o�^
        TrajectoryCal();
        nowPoint = pointMock;
        nowTransform = pointMock.transform.position;
        //Debug.Log(nowTransform);
        //Debug.Log(pointMock.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.GetPlayerPosition(); //�v���C���[�̋������󂯎��

        if (Input.GetMouseButtonDown(0))
        {
            stopFlg = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            playFlg = true;
        }

        RabbitDirection();
        if (!(startFlg != true)) return;
        toPlayerDistance = Vector3.Distance(this.gameObject.transform.position, playerPos); //�����ƃv���C���[�̋����𑪂�
        if (toPlayerDistance < alertRange) //�x���͈͂ɂ�����
        {
            nowPoint = pointMock; //���̓_�̏��
            nowTransform = pointMock.transform.position;
            //Debug.Log(nowPoint.name);
            pointMock = pointMock.GetRabbitMovePointPos(); //���̓_���󂯎��
            nextTransform = pointMock.GetMyPosition(); //���̓_��Position���󂯎��
            nextPosLength = nextTransform - transform.position; //���W�̍�
            animator.SetTrigger("Jump");
            startFlg = true;
        }
    }

    void FixedUpdate()
    {
        StopMove();
        PlayMove();

        if (!(startFlg == true)) return;
        Move();
    }

    private void RabbitDirection()
    {
        if (nextPosLength.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Move()
    {
        if (stopFlg) return;
        //���W�̍�x�̕�����,velocityY��Y�̕������̍��v
        transform.position += new Vector3(nextPosLength.x / DIVISION_NUM, jumpTrajectory[jumpCount] + (nextPosLength.y / DIVISION_NUM), 0);

        if (jumpCount < jumpTrajectory.Length - 1)
        {
            jumpCount++;
        }
        else
        {
            jumpCount = 0;
            startFlg = false;
        }

    }

    private void StopMove()
    {
        if (stopFlg)
        {
            animator.enabled = false;
            childAnimator.enabled = false;
            rigd2D.bodyType = RigidbodyType2D.Kinematic;
            rigd2D.velocity = Vector2.zero;
        }
    }

    private void PlayMove()
    {
        if (playFlg)
        {
            stopFlg = false;
            animator.enabled = true;
            childAnimator.enabled = true;
            rigd2D.bodyType = RigidbodyType2D.Dynamic;
            nextPosLength = new Vector3(0,0,0);

            //��O�̏ꏊ�֏u�Ԉړ�
            Debug.Log(nowTransform);
            //this.transform.position = nowTransform;
            //pointMock = nowPoint;

            //���̏ꏊ�֏u�Ԉړ�
            //this.transform.position = nextTransform;
            
            //��ԋ߂��_�ɏu�Ԉړ�
            this.transform.position = pointMock.transform.position;
            pointMock = pointMock.GetRabbitMovePointPosFromAll();

            playFlg = false;
        }
        
    }

    private void TrajectoryCal() //�v�f�������߂Ĕz��ɃA�j���[�V�����J�[�u��Y��velocity������
    {
        const float TIME_IDENTITY = 1f;
        var curveEndTime = animationCurve.keys[animationCurve.length - 1].time; //�A�j���[�V�����J�[�u�̏I���̕b��
        if (jumpTrajectory == null || jumpTrajectory.Length != DIVISION_NUM * (int)curveEndTime)
        {
            jumpTrajectory = new float[(int)(DIVISION_NUM * curveEndTime)]; //���������̔z��̗v�f��
        }

        for (int i = 0; i < jumpTrajectory.Length; i++)
        {
            //1 / 50 = 0.02�@0.02 = FixedUpdate��1�t���[����
            float curretCoordinateY = animationCurve.Evaluate(TIME_IDENTITY / DIVISION_NUM * i); //���̃A�j���[�V�����J�[�u��Y���W
            float nextCoordinateY = animationCurve.Evaluate(TIME_IDENTITY / DIVISION_NUM * (i + 1)); //���̃A�j���[�V�����J�[�u��Y���W
            float velocityY = (nextCoordinateY - curretCoordinateY) * jumpPower; //velocityY�����߂�

            // �����ɓ����
            jumpTrajectory[i] = velocityY;
            //Debug.Log(jumpTrajectory[i]);//-6.3E ����Ȃ�0�ɋ߂�����(���̓i�V)
        }
    }


}
