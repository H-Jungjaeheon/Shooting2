using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("적 기본 변수")]
    public float Speed, Damage, Hp, MaxHp, Score, GiveExp, Pain;
    public bool IsStart, IsBreak;
    [Header("머터리얼 모음")]
    MeshRenderer MR;
    [SerializeField] private Material[] material;
    // Start is called before the first frame update
    public virtual void Start()
    {
        MR = GetComponent<MeshRenderer>();
        MR.material = material[0];
        Damage = Damage * GameManager.Instance.Stage;
        MaxHp = MaxHp * (GameManager.Instance.Stage + GameManager.Instance.Damage);
        Hp = MaxHp;
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        Move();
        Dead();
    }
    public virtual void Move()
    {
        transform.position = transform.position - new Vector3(0, 0, Speed * Time.deltaTime);
    }
    public virtual void Dead()
    {
        if(Hp <= 0)
        {
            //파티클 시스템
            GameManager.Instance.Score += Score;
            GameManager.Instance.EnemyDead++;
            GameManager.Instance.Exp += GiveExp;
            GameManager.Instance.Pain -= Pain;
            Destroy(this.gameObject);
        }
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Hp -= GameManager.Instance.Damage / 2;
            if(GameManager.Instance.IsShield == false && GameManager.Instance.IsHit == false)
                GameManager.Instance.Hp -= Damage;
            StartCoroutine(Hit());
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            Hp -= GameManager.Instance.Damage;
            StartCoroutine(Hit());
        }
        else if (other.gameObject.CompareTag("DestroyObj"))
        {
            GameManager.Instance.Pain += Pain;
            Destroy(this.gameObject);
        }
    }
    public virtual IEnumerator Hit()
    {
        MR.material = material[1];
        yield return new WaitForSeconds(0.2f);
        MR.material = material[0];
        yield return null;
    }
}
