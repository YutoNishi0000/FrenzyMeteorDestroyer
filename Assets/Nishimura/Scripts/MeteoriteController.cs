using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�N���X
[RequireComponent(typeof(Rigidbody))]    //���W�b�h�{�f�B�R���|�[�l���g���擾
public class MeteoriteController : MonoBehaviour
{
    [SerializeField] private Vector3 defaultDirection;           //�f�t�H���g�Ői�ޕ���
    [SerializeField] private float moveSpeed;                    //���E�ɓ����X�s�[�h
    [SerializeField] private float rotationSpeed;                //��]���x
    private Rigidbody rb;                                        //�d��

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
    /// �ړ��֐�
    /// </summary>
    private void Move()
    {
        rb.velocity = defaultDirection * moveSpeed * Time.deltaTime;     //�f�t�H���g�̈ړ����s��

        Vector3 mousePos = GetMousePoint();
        Vector3 dirMouse = (mousePos - transform.position).normalized - defaultDirection.normalized;    //�}�E�X�̕����������Ă��鐳�K�����ꂽ�x�N�g�����擾
    }

    /// <summary>
    /// �}�E�X�̈ʒu���擾
    /// </summary>
    /// <returns>�}�E�X���W</returns>
    private Vector3 GetMousePoint()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);   //�X�N���[�����W�����[���h���W�ɕϊ�

        return mousePos;
    }
}