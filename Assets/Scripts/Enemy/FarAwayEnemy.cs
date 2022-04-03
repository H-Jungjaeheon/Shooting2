using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarAwayEnemy : Enemy
{
    [SerializeField] private GameObject Bullet, BObj;
    [SerializeField] private float ShootCount, MaxShootCount;
    private void Update()
    {
        Shoot();
    }
    void Shoot()
    {
        ShootCount += Time.deltaTime;
        if(ShootCount >= MaxShootCount)
        {
            Instantiate(Bullet, transform.position - new Vector3(0,0,0.5f), Bullet.transform.rotation);
            ShootCount = 0;
        }
    }
    public override void Dead()
    {
        if (Hp <= 0)
        {
            //파티클 시스템
            GameManager.Instance.Score += Score;
            GameManager.Instance.EnemyDead++;
            GameManager.Instance.Exp += GiveExp;
            GameManager.Instance.Pain -= Pain;
            Destroy(this.gameObject);
            Destroy(BObj);
        }
    }
}
