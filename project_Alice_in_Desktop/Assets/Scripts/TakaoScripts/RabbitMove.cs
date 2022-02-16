using Connector.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMove : MonoBehaviour
{
    RabbitCore rabbitCore;
    RabbitMovePointMock nowPoint;
    [SerializeField] RabbitMovePointMock pointMock;
    [SerializeField] GameObject player;
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] GameObject childSpring;

    private Animator animator;
    private Animator childAnimator;
    private Rigidbody2D rigd2D;
    IPlayerPotionSentable playerPositionSentable;
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

    // Start is called before the first frame update
    void Start()
    {
        rabbitCore = GetComponent<RabbitCore>();
        animator = GetComponent<Animator>();
        childAnimator = childSpring.GetComponent<Animator>();
        rigd2D = GetComponent<Rigidbody2D>();
        playerPositionSentable = player.gameObject.GetComponent<IPlayerPotionSentable>();

        nowPoint = pointMock;
        nowTransform = pointMock.transform.position;

        //�A�j���[�V�����J�[�u�̐ݒ�,�o�^
        TrajectoryCal();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = playerPositionSentable.PlayerPotionSentable();
        RabbitDirection();
        Debug.Log("rabbitCore.isTeleportation = " + rabbitCore.isTeleportation);
        if (!(rabbitCore.isMove != true)) return;
        if (rabbitCore.isTeleportation) return;
        toPlayerDistance = Vector3.Distance(this.gameObject.transform.position, playerPos); //�����ƃv���C���[�̋����𑪂�
        if (toPlayerDistance < alertRange) //�x���͈͂ɂ�����
        {
            nowPoint = pointMock; //���̓_�̏��
            nowTransform = pointMock.transform.position;
            pointMock = pointMock.GetRabbitMovePointPos(); //���̓_���󂯎��
            nextTransform = pointMock.GetMyPosition(); //���̓_��Position���󂯎��
            nextPosLength = nextTransform - transform.position; //���W�̍�
            animator.SetTrigger("Jump");
            rabbitCore.isMove = true;
        }
    }

    void FixedUpdate()
    {
        if (!(rabbitCore.isMove)) return;
        Act_RabbitMove();
    }

    private void Act_RabbitMove()
    {
        if (rabbitCore.isStop) return;
        if (rabbitCore.isHit) return;
        rigd2D.gravityScale = 0;
        //���W�̍�x�̕�����,velocityY��Y�̕������̍��v
        transform.position += new Vector3(nextPosLength.x / DIVISION_NUM, jumpTrajectory[jumpCount] + (nextPosLength.y / DIVISION_NUM), 0);

        if (jumpCount < jumpTrajectory.Length - 1)
        {
            jumpCount++;
        }
        else
        {
            jumpCount = 0;
            rigd2D.gravityScale = 2;
            rabbitCore.isMove = false;
        }
    }

    public void Act_RabbitTeleportation()
    {
        if (!rabbitCore.isTeleportation) return;
        nextPosLength = new Vector3(0, 0, 0);
        StartCoroutine("TeleportRabbit");
    }

    IEnumerator TeleportRabbit()
    {
        yield return new WaitForSeconds(0.25f);
        animator.SetTrigger("Teleport");
        if (nowPoint.GetUseFlg() == false || pointMock.GetUseFlg() == false)
        {
            //��ԋ߂��_�ɏu�Ԉړ�
            Debug.Log("��ԋ߂��_�ɏu�Ԉړ�");
            pointMock = pointMock.GetRabbitMovePointPosFromAll();
            this.transform.position = pointMock.transform.position;
            rabbitCore.isTeleportation = false;
        }
        else
        {
            rabbitCore.isTeleportation = false;
        }
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
