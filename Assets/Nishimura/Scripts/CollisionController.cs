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
            //インターフェイス呼び出し
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            
            //インターフェイスがヌルだったら処理は行わない
            if (damageable == null)
            {
                return;
            }
            Debug.Log("接触");
            //ダメージ処理
            damageable.Damage(damageNum);
        }
    }
}
