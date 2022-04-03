using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : Enemy
{
    [SerializeField] private GameObject Bullet, BObj, MiniObj;
    [SerializeField] private float SpawnCount, MaxSpawnCount;
    private void Update()
    {
        Spawn();
    }
    void Spawn()
    {
        SpawnCount += Time.deltaTime;
        if (SpawnCount >= MaxSpawnCount)
        {
            Instantiate(MiniObj, transform.position, MiniObj.transform.rotation);
            SpawnCount = 0;
        }
    }
    public override void Dead()
    {
        if (Hp <= 0)
        {
            for (int a = 0; a < 360; a += 30)
            {
                Instantiate(Bullet, transform.position, Quaternion.Euler(90, 0, a));
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
