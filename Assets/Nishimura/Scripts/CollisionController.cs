using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    [SerializeField] private int damageNum;

    private void OnCollisionEnter2D(Collision2D collision)
    {
     
        if (collision.gameObject.CompareTag("Player"))
        {
            //�C���^�[�t�F�C�X�Ăяo��
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            
            //�C���^�[�t�F�C�X���k���������珈���͍s��Ȃ�
            if (damageable == null)
            {
                return;
            }
            Debug.Log("�ڐG");
            //�_���[�W����
            damageable.Damage(damageNum);
        }
    }
}
