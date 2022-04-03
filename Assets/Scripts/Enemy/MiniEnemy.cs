using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEnemy : Enemy
{
    [SerializeField] private GameObject Bullet, BObj;
    [SerializeField] private float ShootCount, MaxShootCount, DeadBullet;
    public override void Start()
    {
        base.Start();
        DeadBullet = Random.Range(0, 2);
    }

    private void Update()
    {
        Shoot();
    }
    void Shoot()
    {
        ShootCount += Time.deltaTime;
        if (ShootCount >= MaxShootCount)
        {
            for(int a = -20; a <= 20; a += 40)
            {
                Instantiate(Bullet, transform.position, Quaternion.Euler(90, 0, a));
            }
            ShootCount = 0;
        }
    }
    public override void Dead()
    {
        if (Hp <= 0)
        {
            if (DeadBullet == 0) 
            {
                for (int a = -40; a <= 80; a += 80)
                {
                    Instantiate(Bullet, transform.position, Quaternion.Euler(90, 0, a));
                }
                for (int a = 140; a <= 220; a += 80)
                {
                    Instantiate(Bullet, transform.position, Quaternion.Euler(90, 0, a));
                }
            }
            else
            {
                for (int a = 0; a <= 270; a += 90)
                {
                    Instantiate(Bullet, transform.position, Quaternion.Euler(90, 0, a));
                }
            }
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
