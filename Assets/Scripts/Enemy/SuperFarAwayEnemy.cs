using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperFarAwayEnemy : Enemy
{
    [SerializeField] private bool IsStop;
    [SerializeField] private GameObject Bullet, SObj;
    [SerializeField] private float ShootCount, MaxShootCount, MaxZ, PZ;
    public override void Start()
    {
        base.Start();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        PZ = (GameObject.Find("Player").transform.position.x * -9f) - (GameObject.Find("Player").transform.position.z * -9f);
        Shoot();
    }
    void Shoot()
    {
        if(IsStop == true)
        {
            ShootCount += Time.deltaTime;
            if (ShootCount >= MaxShootCount)
            {
                ShootCount = 0;
                for (int a = -20; a <= 20; a += 20)
                {
                    Instantiate(Bullet, transform.position, Quaternion.Euler(0, PZ + a, 0));
                }
            }
        }
    }
    public override void Move()
    {
        if (IsStop == false)
        {
            if (transform.position.z <= MaxZ)
            {
                IsStop = true;
            }
            base.Move();
            SObj.transform.position = SObj.transform.position - new Vector3(0, 0, Speed * Time.deltaTime);
        }         
    }
    public override void Dead()
    {
        if (Hp <= 0)
        {
            for(int a = Random.Range(0, 361); a <= a * 4; a += a)
            {
                Instantiate(Bullet, transform.position, Quaternion.Euler(0, a, 0));
            }
            //파티클 시스템
            GameManager.Instance.Score += Score;
            GameManager.Instance.EnemyDead++;
            GameManager.Instance.Exp += GiveExp;
            GameManager.Instance.Pain -= Pain;
            Destroy(this.gameObject);
        }
    }
}
