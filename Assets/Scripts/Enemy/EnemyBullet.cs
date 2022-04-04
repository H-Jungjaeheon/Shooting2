using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float Speed, Damage, BulletKind;
 
    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        if(BulletKind == 0)
            transform.Translate(0, 0, -Speed * Time.deltaTime);
        else if(BulletKind == 1)
            transform.Translate(0, -Speed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(GameManager.Instance.IsShield == false && GameManager.Instance.IsHit == false)
                GameManager.Instance.Hp -= Damage;
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("DestroyObj"))
        {
            Destroy(this.gameObject);
        }
    }
}
