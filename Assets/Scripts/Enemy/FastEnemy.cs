using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    [SerializeField] private GameObject Warning;
    [SerializeField] private float StartTime, MaxStartTime;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        IsStart = true;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (IsStart == true)
            Starts();
    }
    void Starts()
    {
        StartTime += Time.deltaTime;
        if(StartTime >= MaxStartTime)
        {
            Warning.SetActive(false);
            IsStart = false;
        }
    }
    public override void Move()
    {
        if(IsStart == false)
            base.Move();
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (IsStart == false)
            base.OnTriggerEnter(other);
    }
}
