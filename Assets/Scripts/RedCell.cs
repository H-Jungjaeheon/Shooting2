using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCell : MonoBehaviour
{
    [SerializeField] private float Speed, Hp, Pain, XRotation;
    [SerializeField] private bool IsHit;
    MeshRenderer MR;
    [SerializeField] private Material[] material;
    // Start is called before the first frame update
    void Start()
    {
        MR = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Dead();
        Spin();
    }
    void Spin()
    {
        XRotation += Time.deltaTime * 30;
        transform.rotation = Quaternion.Euler(0, XRotation, -60);
    }
    void Dead()
    {
        if(Hp <= 0)
        {
            GameManager.Instance.Pain += Pain;
            Destroy(gameObject);
        }
    }
    void Move()
    {
        transform.position = transform.position - new Vector3(0, 0, Speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyBullet") && IsHit == false)
        {
            Hp--;
            IsHit = true;
            StartCoroutine(HitEffect());
        }
        else if (other.gameObject.CompareTag("DestroyObj"))
        {
            GameManager.Instance.Pain -= Pain;
            Destroy(gameObject);
        }
    }
    IEnumerator HitEffect()
    {
        MR.material = material[1];
        yield return new WaitForSeconds(0.2f);
        MR.material = material[0];
        yield return new WaitForSeconds(0.2f);
        MR.material = material[1];
        yield return new WaitForSeconds(0.2f);
        MR.material = material[0];
        IsHit = false;
        yield return null;
    }
}
