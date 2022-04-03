using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : Enemy
{
    [SerializeField] private bool IsStop;
    [SerializeField] private GameObject Bullet, SObj, Shield;
    [SerializeField] private float MaxZ;
    MeshRenderer SMR;
    public float ShieldHp;
    [Header("고유 머터리얼")]
    [SerializeField] private Material[] MT;
    public override void Start()
    {
        IsBreak = true;
        base.Start();
        ShieldHp = ShieldHp * (GameManager.Instance.Stage + GameManager.Instance.Damage);
        if(IsBreak == true)
            SMR = Shield.GetComponent<MeshRenderer>();
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
        if (ShieldHp <= 0 && IsBreak == true)
        {
            IsBreak = false;
            Destroy(Shield);
        }
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(IsBreak == true)
            {
                ShieldHp -= GameManager.Instance.Damage / 2;
                StartCoroutine(ShieldHit());
            }
            else
            {
                Hp -= GameManager.Instance.Damage / 2;
                StartCoroutine(Hit());
            }
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            if (IsBreak == true)
            {
                int a = Random.Range(0, 3);
                int b = Random.Range(-50, 51);
                if(a == 0)
                {
                    Instantiate(Bullet, transform.position, Quaternion.Euler(90, 0, b));
                }
                ShieldHp -= GameManager.Instance.Damage;
                StartCoroutine(ShieldHit());
            }
            else
            {
                Hp -= GameManager.Instance.Damage;
                StartCoroutine(Hit());
            }
        }
        else if (other.gameObject.CompareTag("DestroyObj") && IsBreak == true)
        {
            GameManager.Instance.Pain += Pain;
            Destroy(Shield);
            Destroy(this.gameObject);
        }
    }
    private IEnumerator ShieldHit()
    {
        if(IsBreak == true)
        {
            SMR.material = MT[1];
            yield return new WaitForSeconds(0.2f);
            SMR.material = MT[0];
        }
        yield return null;
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
        }
    }
}
