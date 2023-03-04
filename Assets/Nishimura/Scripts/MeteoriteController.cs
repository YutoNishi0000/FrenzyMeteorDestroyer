using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤークラス
[RequireComponent(typeof(Rigidbody))]    //リジッドボディコンポーネントを取得
public class MeteoriteController : MonoBehaviour
{
    public enum MeteoriteState
    {
        Nomal,             //ノーマル状態
        Acceleration       //加速状態
    }

    [SerializeField] private Vector3 defaultDirection;                    //デフォルトで進む方向
    [SerializeField] private float moveSpeed_default;            //デフォルトで動くスピード
    [SerializeField] private float moveSpeed_horizontal;         //左右に動くスピード
    [SerializeField] private float rotationSpeed_default;        //デフォルトの回転速度
    [SerializeField] private float rotationSpeed_acceleration;   //デフォルトの回転速度
    [SerializeField] private float moveSpeed_acceleration;   //デフォルトの移動速度
    [SerializeField] private float moveSpeed;                             //移動速度
    [SerializeField] private float rotateSpeed;                           //回転速度
    private Rigidbody rb;                                                 //重力
    private MeteoriteState meteoState;
    private SpeedManager speedManager;
    private readonly float ACCELERATION_TIME = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meteoState = new MeteoriteState();    //インスタンスを取得
        speedManager = new SpeedManager(moveSpeed_default, rotationSpeed_default);
    }

    // Update is called once per frame
    void Update()
    {
        RotationController();
        MoveController();
        SpeedController(GetMeteoState(), speedManager);
        AccelerationController(ACCELERATION_TIME);
    }

    #region 移動処理

    /// <summary>
    /// 移動処理
    /// </summary>
    private void MoveController()
    {
        //デフォルトの移動
        rb.velocity = defaultDirection.normalized * Time.deltaTime * GetSpeedManageer().MoveSpeed;

        rb.velocity += GetHorizontalVector(defaultDirection) * Time.deltaTime * moveSpeed_horizontal;
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
        transform.Rotate(Vector3.forward * GetSpeedManageer().RotateSpeed * Time.deltaTime);
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
    }

    private void SpeedController(MeteoriteState state, SpeedManager speed)
    {
        switch(state)
        {
            case MeteoriteState.Nomal:
                SetSpeedManageer(GetNomalSpeed(speed));
                break;

            case MeteoriteState.Acceleration:
                SetSpeedManageer(GetAccelerationSpeed(ACCELERATION_TIME, speed));
                break;
        }
    }

    private SpeedManager GetNomalSpeed(SpeedManager speed)
    {
        speed.MoveSpeed = moveSpeed_default;
        speed.RotateSpeed = rotationSpeed_default;
        return speed;
    }

    private SpeedManager GetAccelerationSpeed(float time, SpeedManager speed)
    {
        speed.MoveSpeed = moveSpeed_acceleration; //Mathf.Lerp(moveSpeed_default, moveSpeed_acceleration, time);
        speed.RotateSpeed = rotationSpeed_acceleration; //Mathf.Lerp(rotationSpeed_default, rotationSpeed_acceleration, time);
        return speed;
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

    public void SetSpeedManageer(SpeedManager speed)
    {
        speedManager = speed;
    }

    public SpeedManager GetSpeedManageer()
    {
        return speedManager;
    }

    #endregion
}

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