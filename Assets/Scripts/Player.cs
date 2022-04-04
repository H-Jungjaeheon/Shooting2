using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField] private float MaxX, MinX, MaxZ, MinZ, NowShootTime;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject[] Enemys;
    [SerializeField] private bool IsBoom;
    Rigidbody rigid;
    MeshRenderer MR;
    [SerializeField] private Material[] material;
    [SerializeField] private CinemachineImpulseSource Source;
    // Start is called before the first frame update
    void Start()
    {
        MR = GetComponent<MeshRenderer>();
        Source = GetComponent<CinemachineImpulseSource>();
        GameManager.Instance.Pain = GameManager.Instance.Stage == 1 ? 10 : 30;
        NowShootTime = GameManager.Instance.ShootTime;
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Shoot();
        Boom();
    }
    private void Boom()
    {
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if (Input.GetKeyDown(KeyCode.J) && GameManager.Instance.Boom > 0 && IsBoom == false)
        {
            IsBoom = true;
            GameManager.Instance.Boom--;
            GameManager.Instance.BoomTime = GameManager.Instance.MaxBoomTime;
            if(Enemys != null)
            {
                for (int a = 0; a < Enemys.Length; a++)
                {
                    if(Enemys[a].GetComponent<Enemy>().IsStart == false && Enemys[a].GetComponent<Enemy>().IsBreak == false)
                    {
                        Enemys[a].GetComponent<Enemy>().Hp -= GameManager.Instance.Damage * 4;
                    }
                    else if(Enemys[a].GetComponent<Enemy>().IsStart == false && Enemys[a].GetComponent<Enemy>().IsBreak == true)
                    {
                        Enemys[a].GetComponent<ShieldEnemy>().ShieldHp -= GameManager.Instance.Damage * 4;
                    }
                    //��ƼŬ, �۵� Ȯ��
                }
            }
        }
        else if(IsBoom == true)
        {
            BoomCooltime();
        }
    }
    private void BoomCooltime()
    {
        GameManager.Instance.BoomTime -= Time.deltaTime;
        if(GameManager.Instance.BoomTime <= 0)
        {
            GameManager.Instance.BoomTime = 0;
            IsBoom = false;
        }
    }
    private void Shoot()
    {
        NowShootTime += Time.deltaTime; 
        if (Input.GetKey(KeyCode.H) && NowShootTime >= GameManager.Instance.ShootTime)
        {
            NowShootTime = 0;
            switch (GameManager.Instance.ShootLevel) //�Ѿ� ���� ������ ��� ����
            {
                case 1:
                    Instantiate(Bullet, transform.position + new Vector3(0,0,1), Bullet.transform.rotation);
                    break;
                case 2:
                    for(float a = -0.5f; a <= 0.5f; a += 1)
                    {
                        Instantiate(Bullet, transform.position + new Vector3(a, 0, 1), Bullet.transform.rotation);
                    }
                    break;
                case 3:
                    for (float a = -0.5f; a <= 0.5f; a += 0.5f)
                    {
                        if(a == 0)
                            Instantiate(Bullet, transform.position + new Vector3(a, 0, 1.5f), Bullet.transform.rotation);
                        else
                            Instantiate(Bullet, transform.position + new Vector3(a, 0, 1), Bullet.transform.rotation);
                    }
                    break;
                case 4:
                    for (float a = -0.5f; a <= 0.5f; a += 0.5f)
                    {
                        if (a == 0)
                            Instantiate(Bullet, transform.position + new Vector3(a, 0, 1.5f), Bullet.transform.rotation);
                        else
                            Instantiate(Bullet, transform.position + new Vector3(a, 0, 1), Bullet.transform.rotation);
                    }
                    for (int a = -20; a <= 20f; a += 40)
                    {
                        Instantiate(Bullet, transform.position, Quaternion.Euler(90, 0, a));
                    }
                    break;
                case 5:
                    for (float a = -0.5f; a <= 0.5f; a += 0.5f)
                    {
                        if (a == 0)
                            Instantiate(Bullet, transform.position + new Vector3(a, 0, 1.5f), Bullet.transform.rotation);
                        else
                            Instantiate(Bullet, transform.position + new Vector3(a, 0, 1), Bullet.transform.rotation);
                    }
                    for (int a = -45; a <= 45f; a += 30)
                    {
                        Instantiate(Bullet, transform.position, Quaternion.Euler(90, 0, a));
                    }
                    break;
            }
        }
        else if(NowShootTime >= GameManager.Instance.ShootTime)
        {
            NowShootTime = GameManager.Instance.ShootTime;
        }
    }
    private void Move()
    {
        rigid.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * GameManager.Instance.Speed, 0, Input.GetAxisRaw("Vertical") * GameManager.Instance.Speed);
        rigid.position = new Vector3(Mathf.Clamp(rigid.position.x, MinX, MaxX), 0, Mathf.Clamp(rigid.position.z, MinZ, MaxZ));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyBullet") && GameManager.Instance.IsShield == false)
        {
            StartCoroutine(Hits());
        }
    }
    IEnumerator Hits()
    {
        Source.GenerateImpulse();
        GameManager.Instance.IsHit = true;
        MR.material = material[1];
        yield return new WaitForSeconds(3);
        GameManager.Instance.IsHit = false;
        MR.material = material[0];
        yield return null;
    }
}
