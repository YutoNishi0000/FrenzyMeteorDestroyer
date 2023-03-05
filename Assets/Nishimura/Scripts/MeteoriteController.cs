using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//オブジェクト基底クラス
public class Actor : MonoBehaviour
{
    protected MeteoriteController Instance;

    private void Awake()
    {
        Instance = FindObjectOfType<MeteoriteController>();
    }
}

//スピードクラス
public class SpeedManager
{
    public float MoveSpeed;
    public float RotateSpeed;

    //コンストラクタ
    public SpeedManager(float moveSoeed, float rotateSpeed)
    {
        MoveSpeed = moveSoeed;
        RotateSpeed = rotateSpeed;
    }
}

//ダメージ処理インターフェイス
interface IDamageable
{
    public void Damage(int damageVal) { }

    public void Death() { }
}

//プレイヤークラス
[RequireComponent(typeof(Rigidbody2D))]    //リジッドボディコンポーネントを取得
public class MeteoriteController : Actor, IDamageable
{
    public enum MeteoriteState
    {
        Nomal,             //ノーマル状態
        Acceleration       //加速状態
    }
    [SerializeField]
    private SoundManager soundManager;

    [SerializeField]
    private AudioClip clip2;
    [SerializeField] private Vector2 defaultDirection;                    //デフォルトで進む方向
    [SerializeField] private float moveSpeed_default;            //デフォルトで動くスピード
    [SerializeField] private float moveSpeed_horizontal;         //左右に動くスピード
    [SerializeField] private float rotationSpeed_default;        //デフォルトの回転速度
    [SerializeField] private float rotationSpeed_acceleration;   //デフォルトの回転速度
    [SerializeField] private float moveSpeed_acceleration;   //デフォルトの移動速度
    [SerializeField] private float AccelerationMoveSpeed;
    [SerializeField] private float AccelerationRotateSpeed;
    [SerializeField] private float decelerationSpeed;              //減衰速度
    [SerializeField] private float rotateSpeed;                           //回転速度
    [SerializeField] private int PlayerMaxHP;                        //プレイヤーのHP最大値
    private readonly float ACCELERATION_TIME = 2f;
    private int PlayerHP;
    private Rigidbody2D rb;                                                 //重力
    private MeteoriteState meteoState;
    private Vector3 InitialSize;                                 //隕石のゲーム開始時のスケールを取得
    private SpeedManager speedManager { get; set; }
    private Vector2 horizontalVec;
    private Vector2 verticalVec;

    public static bool isClear = false;
    public static int lustHP = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        meteoState = new MeteoriteState();    //インスタンスを取得
        speedManager = new SpeedManager(moveSpeed_default, rotationSpeed_default);
        InitializeAcceleration();
        InitialSize = transform.localScale;
        PlayerHP = PlayerMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        RotationController();
        MoveController();
        AccelerationController(ACCELERATION_TIME);
    }

    #region 移動処理

    /// <summary>
    /// 移動処理
    /// </summary>
    private void MoveController()
    {
        horizontalVec += defaultDirection.normalized * Time.deltaTime * GetSpeed(GetMeteoState()).MoveSpeed;
        verticalVec += (Vector2)GetHorizontalVector(defaultDirection) * Time.deltaTime * moveSpeed_horizontal;
        ////デフォルトの移動
        //rb.velocity += horizontalVec + verticalVec;
        rb.MovePosition(horizontalVec + verticalVec);
    }

    /// <summary>
    /// 左右移動するときのベクトルを取得する関数
    /// </summary>
    private Vector3 GetHorizontalVector(Vector3 defaultDirection)
    {
        Vector3 mousePos = GetMousePoint();                               //マウスの位置を取得
        Vector3 dirMouse = (mousePos - transform.position).normalized;    //マウスの方向を向いている正規化されたベクトルを取得
        float angle = Vector3.Angle(dirMouse, defaultDirection);          //マウス方向のベクトルとデフォルトで進むベクトルの間の角度を取得
        defaultDirection = dirMouse * Mathf.Cos(angle * Mathf.Deg2Rad);   //マウス方向のベクトルにCosをかけることでベクトルの大きさを調整したい
        Vector3 horizontalVec = (dirMouse - defaultDirection).normalized; //正規化された左右移動のベクトルを取得
        return horizontalVec;
    }

    /// <summary>
    /// マウスの位置を取得
    /// </summary>
    /// <returns>マウス座標</returns>
    private Vector3 GetMousePoint()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);   //スクリーン座標をワールド座標に変換
        return mousePos;
    }

    #endregion

    #region 回転処理

    /// <summary>
    /// 回転処理
    /// </summary>
    private void RotationController()
    {
        transform.Rotate(Vector3.forward * GetSpeed(GetMeteoState()).RotateSpeed * Time.deltaTime);
    }

    #endregion

    #region 加速処理

    private void AccelerationController(float AccelerationTime)
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(nameof(Acceleration), AccelerationTime);
        }
    }

    IEnumerator Acceleration(float time)
    {
        SetMeteoState(MeteoriteState.Acceleration);
        yield return new WaitForSeconds(time);
        SetMeteoState(MeteoriteState.Nomal);
        InitializeAcceleration();
    }

    private SpeedManager GetSpeed(MeteoriteState state)
    {
        switch(state)
        {
            case MeteoriteState.Nomal:
                return GetNomalSpeed(speedManager);

            case MeteoriteState.Acceleration:
                return GetAccelerationSpeed(speedManager);

            default:
                return null;
        }
    }

    private SpeedManager GetNomalSpeed(SpeedManager speed)
    {
        speed.MoveSpeed = moveSpeed_default;
        speed.RotateSpeed = rotationSpeed_default;
        return speed;
    }

    private SpeedManager GetAccelerationSpeed(SpeedManager speed)
    {
        AccelerationMoveSpeed -= Time.deltaTime * decelerationSpeed;
        AccelerationRotateSpeed -= Time.deltaTime * decelerationSpeed;
        speed.MoveSpeed = AccelerationMoveSpeed;
        speed.RotateSpeed = AccelerationRotateSpeed;
        return speed;
    }

    private void InitializeAcceleration()
    {
        AccelerationMoveSpeed = moveSpeed_acceleration;
        AccelerationRotateSpeed = rotationSpeed_acceleration;
    }

    #endregion

    #region 被弾処理

    //ダメージ処理
    public void Damage(int damageVal)
    {
        //HPを減らす
        PlayerHP -= damageVal;
        soundManager.Play(clip2);
        //大きさを小さくする
        transform.localScale -= (InitialSize / PlayerMaxHP) * damageVal;

        //もしプレイヤーのHPが0になったら
        if(PlayerHP <= 0)
        {
            //死亡処理を行う
            Death();
        }
    }

    //死亡処理
    public void Death()
    {
        isClear = false;
        //ゲームオーバー
        GameManager.Instance.LoadScene("result");
        
    }


    #endregion

    #region 地球衝突処理


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("goal"))
        {
            isClear = true;
            //ゲームオーバー
            lustHP = PlayerHP;
            GameManager.Instance.LoadScene("result");
        }
    }        
    #endregion

            #region ゲッター、セッター

            //隕石の状態をセット
            public void SetMeteoState(MeteoriteState state)
    {
        meteoState = state;
    }

    //隕石の状態を取得
    public MeteoriteState GetMeteoState()
    {
        return meteoState;
    }

    #endregion
}