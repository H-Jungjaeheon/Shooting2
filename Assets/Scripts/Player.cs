using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.IO;

public class Player : MonoBehaviour
{
    [SerializeField] private float MaxX, MinX, MaxZ, MinZ, NowShootTime;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject[] Enemys, Bullets;
    [SerializeField] private bool IsBoom;
    Rigidbody rigid;
    MeshRenderer MR;
    [SerializeField] private Material[] material;
    [SerializeField] private CinemachineImpulseSource Source;
    // Start is called before the first frame update
    void Start()
    {
        Json();
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
        Bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        if (Input.GetKeyDown(KeyCode.J) && GameManager.Instance.Boom > 0 && IsBoom == false)
        {
            IsBoom = true;
            GameManager.Instance.Boom--;
            GameManager.Instance.BoomTime = GameManager.Instance.MaxBoomTime;
            if (Enemys != null)
            {
                foreach (GameObject a in Enemys)
                {
                    if (a.GetComponent<Enemy>().IsStart == false && a.GetComponent<Enemy>().IsBreak == false)
                    {
                        a.GetComponent<Enemy>().Hp -= GameManager.Instance.Damage * 4;
                    }
                    else if (a.GetComponent<Enemy>().IsStart == false && a.GetComponent<Enemy>().IsBreak == true)
                    {
                        a.GetComponent<ShieldEnemy>().ShieldHp -= GameManager.Instance.Damage * 4;
                    }
                    //파티클, 작동 확인
                }
                foreach (GameObject b in Bullets)
                {
                    Destroy(b);
                }
            }
        }
        else if (IsBoom == true)
        {
            BoomCooltime();
        }
    }
    private void BoomCooltime()
    {
        GameManager.Instance.BoomTime -= Time.deltaTime;
        if (GameManager.Instance.BoomTime <= 0)
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
            switch (GameManager.Instance.ShootLevel) //총알 많이 나가고 모양 변경
            {
                case 1:
                    Instantiate(Bullet, transform.position + new Vector3(0, 0, 1), Bullet.transform.rotation);
                    break;
                case 2:
                    for (float a = -0.5f; a <= 0.5f; a += 1)
                    {
                        Instantiate(Bullet, transform.position + new Vector3(a, 0, 1), Bullet.transform.rotation);
                    }
                    break;
                case 3:
                    for (float a = -0.5f; a <= 0.5f; a += 0.5f)
                    {
                        if (a == 0)
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
        else if (NowShootTime >= GameManager.Instance.ShootTime)
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
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyBullet") && GameManager.Instance.IsShield == false)
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
    void Json()
    {
        PlayerStats PS = new PlayerStats();
        PS.Hp = GameManager.Instance.Hp;
        string json = JsonUtility.ToJson(PS);
        string filename = "PlayerStats";
        string where = Application.dataPath + "/" + filename + ".json";
        File.WriteAllText(where, json);
    }
}
public class PlayerStats
{
    public float Hp;
}


