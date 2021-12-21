using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMoveMock : MonoBehaviour
{
    RabbitMovePointMock nowPoint;
    [SerializeField] RabbitMovePointMock pointMock;
    //[SerializeField] PlayerCoreMock player; //Mockなので組み込む際は変更必須
    [SerializeField] PlayerPositionGet player;//Mockなので組み込む際は変更必須
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] GameObject childSpring;

    private Animator animator;
    private Animator childAnimator;
    private Rigidbody2D rigd2D;
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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        childAnimator = childSpring.GetComponent<Animator>();
        rigd2D = GetComponent<Rigidbody2D>();
        //アニメーションカーブの設定,登録
        TrajectoryCal();
        nowPoint = pointMock;
        nowTransform = pointMock.transform.position;
        //Debug.Log(nowTransform);
        //Debug.Log(pointMock.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.GetPlayerPosition(); //プレイヤーの距離を受け取る

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
        toPlayerDistance = Vector3.Distance(this.gameObject.transform.position, playerPos); //自分とプレイヤーの距離を測る
        if (toPlayerDistance < alertRange) //警戒範囲にいたら
        {
            nowPoint = pointMock; //今の点の情報
            nowTransform = pointMock.transform.position;
            //Debug.Log(nowPoint.name);
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

            //一つ前の場所へ瞬間移動
            Debug.Log(nowTransform);
            //this.transform.position = nowTransform;
            //pointMock = nowPoint;

            //次の場所へ瞬間移動
            //this.transform.position = nextTransform;
            
            //一番近い点に瞬間移動
            this.transform.position = pointMock.transform.position;
            pointMock = pointMock.GetRabbitMovePointPosFromAll();

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


}
