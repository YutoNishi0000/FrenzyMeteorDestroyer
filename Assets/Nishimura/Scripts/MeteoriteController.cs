using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�I�u�W�F�N�g���N���X
public class Actor : MonoBehaviour
{
    protected MeteoriteController Instance;

    private void Awake()
    {
        Instance = FindObjectOfType<MeteoriteController>();
    }
}

//�X�s�[�h�N���X
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

//�_���[�W�����C���^�[�t�F�C�X
interface IDamageable
{
    public void Damage(int damageVal) { }

    public void Death() { }
}

//�v���C���[�N���X
[RequireComponent(typeof(Rigidbody2D))]    //���W�b�h�{�f�B�R���|�[�l���g���擾
public class MeteoriteController : Actor, IDamageable
{
    public enum MeteoriteState
    {
        Nomal,             //�m�[�}�����
        Acceleration       //�������
    }
    [SerializeField]
    private SoundManager soundManager;

    [SerializeField]
    private AudioClip clip2;
    [SerializeField] private Vector2 defaultDirection;                    //�f�t�H���g�Ői�ޕ���
    [SerializeField] private float moveSpeed_default;            //�f�t�H���g�œ����X�s�[�h
    [SerializeField] private float moveSpeed_horizontal;         //���E�ɓ����X�s�[�h
    [SerializeField] private float rotationSpeed_default;        //�f�t�H���g�̉�]���x
    [SerializeField] private float rotationSpeed_acceleration;   //�f�t�H���g�̉�]���x
    [SerializeField] private float moveSpeed_acceleration;   //�f�t�H���g�̈ړ����x
    [SerializeField] private float AccelerationMoveSpeed;
    [SerializeField] private float AccelerationRotateSpeed;
    [SerializeField] private float decelerationSpeed;              //�������x
    [SerializeField] private float rotateSpeed;                           //��]���x
    [SerializeField] private int PlayerMaxHP;                        //�v���C���[��HP�ő�l
    private readonly float ACCELERATION_TIME = 2f;
    private int PlayerHP;
    private Rigidbody2D rb;                                                 //�d��
    private MeteoriteState meteoState;
    private Vector3 InitialSize;                                 //覐΂̃Q�[���J�n���̃X�P�[�����擾
    private SpeedManager speedManager { get; set; }
    private Vector2 horizontalVec;
    private Vector2 verticalVec;

    public static bool isClear = false;
    public static int lustHP = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        meteoState = new MeteoriteState();    //�C���X�^���X���擾
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

    #region �ړ�����

    /// <summary>
    /// �ړ�����
    /// </summary>
    private void MoveController()
    {
        horizontalVec += defaultDirection.normalized * Time.deltaTime * GetSpeed(GetMeteoState()).MoveSpeed;
        verticalVec += (Vector2)GetHorizontalVector(defaultDirection) * Time.deltaTime * moveSpeed_horizontal;
        ////�f�t�H���g�̈ړ�
        //rb.velocity += horizontalVec + verticalVec;
        rb.MovePosition(horizontalVec + verticalVec);
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

    #region ��e����

    //�_���[�W����
    public void Damage(int damageVal)
    {
        //HP�����炷
        PlayerHP -= damageVal;
        soundManager.Play(clip2);
        //�傫��������������
        transform.localScale -= (InitialSize / PlayerMaxHP) * damageVal;

        //�����v���C���[��HP��0�ɂȂ�����
        if(PlayerHP <= 0)
        {
            //���S�������s��
            Death();
        }
    }

    //���S����
    public void Death()
    {
        isClear = false;
        //�Q�[���I�[�o�[
        GameManager.Instance.LoadScene("result");
        
    }


    #endregion

    #region �n���Փˏ���


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("goal"))
        {
            isClear = true;
            //�Q�[���I�[�o�[
            lustHP = PlayerHP;
            GameManager.Instance.LoadScene("result");
        }
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