using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseEnemy : Enemy
{
    [SerializeField] private float DMoveCount, MaxDMoveCount, a, DMoving;
    [SerializeField] private bool IsDMove, Change;
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
        if(DMoveCount >= MaxDMoveCount)
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
        if(IsDMove == true && a == 0)
        {
            if(transform.position.x >= 5)
            {
                Change = true;
                transform.position = transform.position - new Vector3(Speed * Time.deltaTime / 2, 0, 0);
            }
            else if(Change == false)
                transform.position = transform.position + new Vector3(Speed * Time.deltaTime / 2, 0, 0);
        }
        else if (IsDMove == true && a == 1)
        {
            if (transform.position.x <= -5)
            {
                Change = true;
                transform.position = transform.position + new Vector3(-Speed * Time.deltaTime / 2, 0, 0);
            }
            else if(Change == false)
                transform.position = transform.position - new Vector3(Speed * Time.deltaTime / 2, 0, 0);
        }
        if(DMoving >= 3)
        {
            StopDMove();
        }
    }
    void StopDMove()
    {
        Change = false;
        DMoveCount = 0;
        DMoving = 0;
        IsDMove = false;
        a = Random.Range(0, 2);
    }
    public override void Move()
    {
        if(IsDMove == false)
        {
            base.Move();
        }
    }
}
