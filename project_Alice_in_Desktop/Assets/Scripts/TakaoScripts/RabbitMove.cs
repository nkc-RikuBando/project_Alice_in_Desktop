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
    Vector3 nextTransform; //次の場所
    Vector3 nextPosLength; //次の座標と今の座標の差を保存するためのVector

    int jumpCount = 0;
    const float DIVISION_NUM = 50f; //分割数
    float[] jumpTrajectory; //アニメーションカーブのジャンプの軌跡を配列にしたもの
    float toPlayerDistance; //プレイヤーとの距離
    float alertRange = 7f; //警戒範囲
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

        //アニメーションカーブの設定,登録
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
        toPlayerDistance = Vector3.Distance(this.gameObject.transform.position, playerPos); //自分とプレイヤーの距離を測る
        if (toPlayerDistance < alertRange) //警戒範囲にいたら
        {
            nowPoint = pointMock; //今の点の情報
            nowTransform = pointMock.transform.position;
            pointMock = pointMock.GetRabbitMovePointPos(); //次の点を受け取る
            nextTransform = pointMock.GetMyPosition(); //次の点のPositionを受け取る
            nextPosLength = nextTransform - transform.position; //座標の差
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
        //座標の差xの分割数,velocityYとYの分割数の合計
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
            //一番近い点に瞬間移動
            Debug.Log("一番近い点に瞬間移動");
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

    private void TrajectoryCal() //要素数を求めて配列にアニメーションカーブのYのvelocityを入れる
    {
        const float TIME_IDENTITY = 1f;
        var curveEndTime = animationCurve.keys[animationCurve.length - 1].time; //アニメーションカーブの終わりの秒数
        if (jumpTrajectory == null || jumpTrajectory.Length != DIVISION_NUM * (int)curveEndTime)
        {
            jumpTrajectory = new float[(int)(DIVISION_NUM * curveEndTime)]; //分割数分の配列の要素数
        }

        for (int i = 0; i < jumpTrajectory.Length; i++)
        {
            //1 / 50 = 0.02　0.02 = FixedUpdateの1フレーム毎
            float curretCoordinateY = animationCurve.Evaluate(TIME_IDENTITY / DIVISION_NUM * i); //今のアニメーションカーブのY座標
            float nextCoordinateY = animationCurve.Evaluate(TIME_IDENTITY / DIVISION_NUM * (i + 1)); //次のアニメーションカーブのY座標
            float velocityY = (nextCoordinateY - curretCoordinateY) * jumpPower; //velocityYを求める

            // ここに入れる
            jumpTrajectory[i] = velocityY;
            //Debug.Log(jumpTrajectory[i]);//-6.3E 限りなく0に近いだけ(問題はナシ)
        }
    }
}
