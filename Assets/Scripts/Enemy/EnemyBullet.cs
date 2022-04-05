using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float Speed, Damage, BulletKind;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private bool IsSpawn;
 
    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        if (BulletKind == 0)
            transform.Translate(0, 0, 0);
        else if (BulletKind == 1)
            transform.Translate(0, -Speed * Time.deltaTime, 0);
        else if (BulletKind == 2)
        {
            transform.Translate(0, 0, -Speed * Time.deltaTime);
            transform.RotateAround(GameObject.Find("NullBullet").transform.position, Vector3.down, 2f);
        }
        else if(BulletKind == 3 && IsSpawn == false)
        {
            for(int a = 0; a <= 360; a += 40)
            {
                Instantiate(Bullet, transform.position, Quaternion.Euler(0, a, 0));
            }
            IsSpawn = true;
        }
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
