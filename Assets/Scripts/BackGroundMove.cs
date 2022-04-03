using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    [SerializeField] private float MoveSpeed, EndPosition, StartPosition;
    [SerializeField] private GameObject[] BackGround;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    { 
        BackGround[0].transform.position = BackGround[0].transform.position - new Vector3(0, 0, MoveSpeed * Time.deltaTime);
        BackGround[1].transform.position = BackGround[1].transform.position - new Vector3(0, 0, MoveSpeed * Time.deltaTime);
        for(int a = 0; a < 2; a++)
        {
            if (BackGround[a].transform.position.z <= EndPosition)
                BackGround[a].transform.position = new Vector3(0, -22, StartPosition);
        }
    }
}
