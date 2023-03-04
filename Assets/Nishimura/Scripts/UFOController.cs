using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ランダムに決めた方向に対して左右移動を行う
[RequireComponent(typeof(Rigidbody2D))]
public class UFOController : MonoBehaviour
{
    [SerializeField] private float speed;   //動くスピード
    private float randAngle;      //動く方向がx軸から何度離れているか
    private Vector2 directionVec;
    private Rigidbody2D rb;
    private bool direction;
    [SerializeField] private readonly float changeTime;    //何秒後に方向転換するか

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        randAngle = Random.Range(0, 360);
        directionVec = GetDirectionVec(randAngle);
        direction = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity += directionVec.normalized * Time.deltaTime * speed;
    }

    private Vector2 GetDirectionVec(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;

        float x = Mathf.Cos(rad);
        float y = Mathf.Sin(rad);

        return new Vector2(x, y);
    }

    private int GetDirection(bool flag)
    {
        if (flag)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    private bool InverseFlag(bool flag)
    {
        flag = !flag;
        return flag;
    }
}
