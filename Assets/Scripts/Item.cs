using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float Speed, ItemKind;

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        transform.position = transform.position - new Vector3(0, 0, Speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (ItemKind)
            {
                case 1:
                    if(GameManager.Instance.ShootLevel < 5)
                    GameManager.Instance.ShootLevel++;
                    else
                        GameManager.Instance.Score += 600;
                    break;
                case 2:
                    GameManager.Instance.ShieldCount = 3;
                    break;
                case 3:
                    if (GameManager.Instance.Hp < GameManager.Instance.MaxHp)
                        GameManager.Instance.Hp += 15 * GameManager.Instance.Stage;
                    else
                        GameManager.Instance.Score += 400;
                    break;
                case 4:
                    if (GameManager.Instance.Pain < GameManager.Instance.MaxPain)
                        GameManager.Instance.Pain -= 20 * GameManager.Instance.Stage;
                    else
                        GameManager.Instance.Score += 400;
                    break;
                case 5:
                    if (GameManager.Instance.Boom < GameManager.Instance.MaxBoom)
                        GameManager.Instance.Boom++;
                    else
                        GameManager.Instance.Score += 800;
                    break;
                case 6:
                    if (GameManager.Instance.Speed < GameManager.Instance.MaxSpeed)
                        GameManager.Instance.Speed++;
                    else
                        GameManager.Instance.Score += 500;
                    break;
                case 7:
                    GameManager.Instance.Exp += GameManager.Instance.MaxExp / 10;
                    break;
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("DestroyObj"))
        {
            Destroy(gameObject);
        }
    }
}
