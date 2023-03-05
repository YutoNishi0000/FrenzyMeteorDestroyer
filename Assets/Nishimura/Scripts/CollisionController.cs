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
            //インターフェイス呼び出し
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            //インターフェイスがヌルだったら処理は行わない
            if(damageable == null)
            {
                return;
            }

            //ダメージ処理
            damageable.Damage(damageNum);
        }
    }
}
