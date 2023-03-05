using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//�����_���Ɍ��߂������ɑ΂��č��E�ړ����s��
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CollisionController))]
public class UFOController : MonoBehaviour
{
    public float length = 4.0f;     //UFO��������
    public float speed = 2.0f;      //UFO�������X�s�[�h
    private Vector3 startPos;
    private float randAngle;      //����������x�����牽�x����Ă��邩
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        randAngle = Random.Range(0, 360);
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(new Vector2((Mathf.Sin((Time.time) * speed) * GetDirectionVec(randAngle).x + startPos.x) , Mathf.Sin((Time.time) * speed) * GetDirectionVec(randAngle).y + startPos.y));
    }

    private Vector2 GetDirectionVec(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;

        float x = Mathf.Cos(rad);
        float y = Mathf.Sin(rad);

        return new Vector2(x, y);
    }
}