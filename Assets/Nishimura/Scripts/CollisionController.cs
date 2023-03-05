using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    [SerializeField] private int damageNum;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //�C���^�[�t�F�C�X�Ăяo��
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            //�C���^�[�t�F�C�X���k���������珈���͍s��Ȃ�
            if(damageable == null)
            {
                return;
            }

            //�_���[�W����
            damageable.Damage(damageNum);
        }
    }
}
