using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCell : MonoBehaviour
{
    [SerializeField] private float Speed, Hp, XRotation;
    MeshRenderer MR;
    [SerializeField] private Material[] material;
    [SerializeField] private GameObject[] Item;
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
        if (Hp <= 0)
        {
            int a = Random.Range(0, 7);
            Instantiate(Item[a], transform.position, Item[a].transform.rotation);
            Destroy(gameObject);
        }
    }
    void Move()
    {
        transform.position = transform.position - new Vector3(0, 0, Speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet"))
        {
            Hp--;
            StartCoroutine(HitEffect());
        }
        else if (other.gameObject.CompareTag("DestroyObj"))
        {
            Destroy(gameObject);
        }
    }
    IEnumerator HitEffect()
    {
        MR.material = material[1];
        yield return new WaitForSeconds(0.3f);
        MR.material = material[0];
        yield return null;
    }
}
