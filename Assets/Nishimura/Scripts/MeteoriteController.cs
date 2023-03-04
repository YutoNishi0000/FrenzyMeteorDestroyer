using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤークラス
[RequireComponent(typeof(Rigidbody))]    //リジッドボディコンポーネントを取得
public class MeteoriteController : MonoBehaviour
{
    [SerializeField] private Vector3 defaultDirection;           //デフォルトで進む方向
    [SerializeField] private float moveSpeed;                    //左右に動くスピード
    [SerializeField] private float rotationSpeed;                //回転速度
    private Rigidbody rb;                                        //重力

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 移動関数
    /// </summary>
    private void Move()
    {
        rb.velocity = defaultDirection * moveSpeed * Time.deltaTime;     //デフォルトの移動を行う

        Vector3 mousePos = GetMousePoint();
        Vector3 dirMouse = (mousePos - transform.position).normalized - defaultDirection.normalized;    //マウスの方向を向いている正規化されたベクトルを取得
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
}