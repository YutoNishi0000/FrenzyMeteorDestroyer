using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�N���X
[RequireComponent(typeof(Rigidbody))]    //���W�b�h�{�f�B�R���|�[�l���g���擾
public class MeteoriteController : MonoBehaviour
{
    public enum MeteoriteState
    {
        Nomal,             //�m�[�}�����
        Acceleration       //�������
    }

    [SerializeField] private Vector3 defaultDirection;                    //�f�t�H���g�Ői�ޕ���
    [SerializeField] private float moveSpeed_default;            //�f�t�H���g�œ����X�s�[�h
    [SerializeField] private float moveSpeed_horizontal;         //���E�ɓ����X�s�[�h
    [SerializeField] private float rotationSpeed_default;        //�f�t�H���g�̉�]���x
    [SerializeField] private float rotationSpeed_acceleration;   //�f�t�H���g�̉�]���x
    [SerializeField] private float moveSpeed_acceleration;   //�f�t�H���g�̈ړ����x
    [SerializeField] private float AccelerationMoveSpeed;
    [SerializeField] private float AccelerationRotateSpeed;
    [SerializeField] private float rotateSpeed;                           //��]���x
    private Rigidbody rb;                                                 //�d��
    private MeteoriteState meteoState;
    private SpeedManager speedManager { get; set; }
    private readonly float ACCELERATION_TIME = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meteoState = new MeteoriteState();    //�C���X�^���X���擾
        speedManager = new SpeedManager(moveSpeed_default, rotationSpeed_default);
    }

    // Update is called once per frame
    void Update()
    {
        RotationController();
        MoveController();
        AccelerationController(ACCELERATION_TIME);
        InitializeAcceleration();
    }

    #region �ړ�����

    /// <summary>
    /// �ړ�����
    /// </summary>
    private void MoveController()
    {
        //�f�t�H���g�̈ړ�
        rb.velocity = defaultDirection.normalized * Time.deltaTime * GetSpeed(GetMeteoState()).MoveSpeed;

        rb.velocity += GetHorizontalVector(defaultDirection) * Time.deltaTime * moveSpeed_horizontal;
    }

    /// <summary>
    /// ���E�ړ�����Ƃ��̃x�N�g�����擾����֐�
    /// </summary>
    private Vector3 GetHorizontalVector(Vector3 defaultDirection)
    {
        Vector3 mousePos = GetMousePoint();                               //�}�E�X�̈ʒu���擾
        Vector3 dirMouse = (mousePos - transform.position).normalized;    //�}�E�X�̕����������Ă��鐳�K�����ꂽ�x�N�g�����擾
        float angle = Vector3.Angle(dirMouse, defaultDirection);          //�}�E�X�����̃x�N�g���ƃf�t�H���g�Ői�ރx�N�g���̊Ԃ̊p�x���擾
        defaultDirection = dirMouse * Mathf.Cos(angle * Mathf.Deg2Rad);   //�}�E�X�����̃x�N�g����Cos�������邱�ƂŃx�N�g���̑傫���𒲐�������
        Vector3 horizontalVec = (dirMouse - defaultDirection).normalized; //���K�����ꂽ���E�ړ��̃x�N�g�����擾
        return horizontalVec;
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

    #endregion

    #region ��]����

    /// <summary>
    /// ��]����
    /// </summary>
    private void RotationController()
    {
        transform.Rotate(Vector3.forward * GetSpeed(GetMeteoState()).RotateSpeed * Time.deltaTime);
    }

    #endregion

    #region ��������

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
        AccelerationMoveSpeed -= Time.deltaTime * moveSpeed_default;
        AccelerationRotateSpeed -= Time.deltaTime * rotationSpeed_default;
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

    #region �Q�b�^�[�A�Z�b�^�[

    //覐΂̏�Ԃ��Z�b�g
    public void SetMeteoState(MeteoriteState state)
    {
        meteoState = state;
    }

    //覐΂̏�Ԃ��擾
    public MeteoriteState GetMeteoState()
    {
        return meteoState;
    }

    #endregion
}

public class SpeedManager
{
    public float MoveSpeed;
    public float RotateSpeed;

    //�R���X�g���N�^
    public SpeedManager(float moveSoeed, float rotateSpeed)
    {
        MoveSpeed = moveSoeed;
        RotateSpeed = rotateSpeed;
    }
}