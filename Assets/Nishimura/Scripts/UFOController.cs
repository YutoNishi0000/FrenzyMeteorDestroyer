using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����_���Ɍ��߂������ɑ΂��č��E�ړ����s��
[RequireComponent(typeof(Rigidbody2D))]
public class UFOController : MonoBehaviour
{
    [SerializeField] private float speed;   //�����X�s�[�h
    private float randAngle;      //����������x�����牽�x����Ă��邩
    private Vector3 direction;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        randAngle = Random.Range(0, 360);
        direction = GetDirection(randAngle);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity += (Vector2)direction * Time.deltaTime * speed;
    }

    private Vector3 GetDirection(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;

        float x = Mathf.Cos(rad);
        float y = Mathf.Sin(rad);
        float z = 0;

        return new Vector3(x, y, z);
    }
}
