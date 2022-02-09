using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Window;
using Connector.Player;

public class RabbitMoveMock : MonoBehaviour,IWindowLeave,IWindowTouch
{
    RabbitMovePointMock nowPoint;
    RabbitHit rabbitHit;
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

    bool startFlg = false; //移動開始するかどうか
    bool stopFlg = false;
    bool playFlg = false;
    bool hitFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        childAnimator = childSpring.GetComponent<Animator>();
        rigd2D = GetComponent<Rigidbody2D>();
        rabbitHit = GetComponent<RabbitHit>();
        playerPositionSentable = player.gameObject.GetComponent<IPlayerPotionSentable>();
        //アニメーションカーブの設定,登録
        TrajectoryCal();
        nowPoint = pointMock;
        nowTransform = pointMock.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = playerPositionSentable.PlayerPotionSentable();
        hitFlg = rabbitHit.HitRabbitFlg();
        RabbitDirection();
        if (!(startFlg != true)) return;
        toPlayerDistance = Vector3.Distance(this.gameObject.transform.position, playerPos); //自分とプレイヤーの距離を測る
        if (toPlayerDistance < alertRange) //警戒範囲にいたら
        {
            nowPoint = pointMock; //今の点の情報
            nowTransform = pointMock.transform.position;
            pointMock = pointMock.GetRabbitMovePointPos(); //次の点を受け取る
            nextTransform = pointMock.GetMyPosition(); //次の点のPositionを受け取る
            nextPosLength = nextTransform - transform.position; //座標の差
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
        if (hitFlg) return;
        if (stopFlg) return;
        //座標の差xの分割数,velocityYとYの分割数の合計
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
            if (hitFlg) return;
            nextPosLength = new Vector3(0, 0, 0);

            StartCoroutine("TeleportRabbit");

            //if(nowPoint.GetUseFlg() == true)
            //{
            //    //一つ前の場所へ瞬間移動
            //    Debug.Log("一つ前の場所へ瞬間移動");
            //    this.transform.position = nowTransform;
            //    nowTransform = nextTransform;
            //    pointMock = nowPoint;
            //    nowPoint = pointMock.GetRabbitMovePointPos();
            //}

            if(pointMock.GetUseFlg() == true)
            {
                transform.position = pointMock.transform.position;
            }
            
            //if(nowPoint.GetUseFlg() == false)
            //{
            //    //次の場所へ瞬間移動
            //    Debug.Log("次の場所へ瞬間移動");
            //    this.transform.position = nextTransform;
            //}

            playFlg = false; 
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

    public void WindowTouchAction()
    {
        stopFlg = true;
    }

    public void WindowLeaveAction()
    {
        playFlg = true;
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
        }
    }
}
