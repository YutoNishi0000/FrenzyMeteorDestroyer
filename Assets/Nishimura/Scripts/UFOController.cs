using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����_���Ɍ��߂������ɑ΂��č��E�ړ����s��
[RequireComponent(typeof(Rigidbody2D))]
public class UFOController : MonoBehaviour
{
    [SerializeField] private float speed;   //�����X�s�[�h
    private float randAngle;      //����������x�����牽�x����Ă��邩
    private Vector2 directionVec;
    private Rigidbody2D rb;
    private bool direction;
    [SerializeField] private readonly float changeTime;    //���b��ɕ����]�����邩

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
