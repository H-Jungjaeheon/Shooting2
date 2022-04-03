using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiltEnemy : Enemy
{
    [SerializeField] private float DMoveCount, MaxDMoveCount, XMax, XMin, a, DMoving;
    [SerializeField] private bool IsDMove, SpawnMini;
    [SerializeField] private GameObject MiniMonster, BObj;
    [SerializeField] private GameObject[] Body;
    Rigidbody rigid;
    public override void Start()
    {
        base.Start();
        a = Random.Range(0, 2);
        rigid = GetComponent<Rigidbody>();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (DMoveCount >= MaxDMoveCount)
        {
            DMove();
        }
        else
        {
            DMoveCount += Time.deltaTime;
        }
    }
    void DMove()
    {
        IsDMove = true;
        DMoving += Time.deltaTime;
        rigid.position = new Vector3(Mathf.Clamp(rigid.position.x, XMin, XMax), 0, 0);
        if (IsDMove == true && a == 0)
        {
            transform.position = transform.position + new Vector3(Speed * Time.deltaTime / 2, 0, 0);
            for (int a = 0; a < 2; a++)
            {
                Body[a].transform.position = Body[a].transform.position + new Vector3(Speed * Time.deltaTime / 2, 0, 0);
            }
        }
        else if (IsDMove == true && a == 1)
        {
            transform.position = transform.position + new Vector3(-Speed * Time.deltaTime / 2, 0, 0);
            for (int a = 0; a < 2; a++)
            {
                Body[a].transform.position = Body[a].transform.position + new Vector3(-Speed * Time.deltaTime / 2, 0, 0);
            }
        }
        if (DMoving >= 3)
        {
            StopDMove();
        }
    }
    void StopDMove()
    {
        DMoveCount = 0;
        DMoving = 0;
        IsDMove = false;
        a = Random.Range(0, 2);
    }
    public override void Move()
    {
        if (IsDMove == false)
        {
            base.Move();
            for(int a = 0; a < 2; a++)
            {
                Body[a].transform.position = Body[a].transform.position - new Vector3(0, 0, Speed * Time.deltaTime);
            }
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
            if(SpawnMini == true)
            {
                for(int a = -1; a <= 1; a += 2)
                {
                    Instantiate(MiniMonster, transform.position + new Vector3(a,0,0), MiniMonster.transform.rotation);
                }
            }
            Destroy(this.gameObject);
            Destroy(BObj.gameObject);
            for(int a = 0; a < 2; a++)
            {
                Destroy(Body[a].gameObject);
            }
        }
    }
}
