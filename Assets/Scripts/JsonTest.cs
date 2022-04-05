using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Json();
    }
    private void Update()
    {

    }
    // Update is called once per frame
//    void Json()
//    {
//        PlayerStats PS = new PlayerStats();
//        PS.Hp = 1;
//        PS.Damage = 1;
//        PS.Speed = 1;
//        PS.ShootLevel = 1;
//        string json = JsonUtility.ToJson(PS);
//        string filename = "PlayerStats";
//        string path = Application.dataPath + "/" + filename + ".Json";
//        File.WriteAllText(path, json);
//    }
}
//public class PlayerStats
//{
//    public float Hp;
//    public float Damage;
//    public float Speed;
//    public float ShootLevel;
//}