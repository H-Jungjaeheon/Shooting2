using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    //[Header("ÇÃ·¹ÀÌ¾î º¯¼öµé")]
    public float Hp, MaxHp, Pain, MaxPain, Stage, EnemyDead, Damage, ShootLevel,
        ShootTime, Speed, MaxSpeed, Exp, MaxExp, Level, Boom, MaxBoom, Score,
        BoomTime, MaxBoomTime, ShieldCount; //Ã¼·Â ¸ÆÃ¼·Â °íÅë ¸Æ°íÅë ½ºÅ×ÀÌÁö Àû Á×Àº ¼ö µ¥¹ÌÁö ¹ß»ç ¼Óµµ ½ºÇÇµå °æÇèÄ¡ ·¹º§ ÆøÅº ¸Æ ÆøÅº Á¡¼ö ÆøÅº ÄðÅ¸ÀÓ ÆøÅº ¸ÆÄðÅ¸ÀÓ
    public bool IsShield, ShieldEffcting, IsHit;
    [SerializeField] private GameObject AllUiObj, Player, Shield;
    [SerializeField] private Image HpBar, PainBar, ExpBar, BoomCoolTime;
    [SerializeField] private Text HpText, PainText, ExpText, LevelText, BoomCount, ScoreText;
    MeshRenderer MR;
    [SerializeField] private Material[] ShieldMt;
    private void Awake()
    {
        MR = Shield.GetComponent<MeshRenderer>();
        Player = GameObject.Find("Player");
        Score = 0;
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    void Shielding()
    {
        Shield.transform.position = Player.transform.position;
        if (ShieldCount <= 0)
        {
            Shield.SetActive(false);
            IsShield = false;
            ShieldCount = 0;
            MR.material = ShieldMt[0];
        }
        else if (ShieldCount > 0)
        {
            Shield.SetActive(true);
            IsShield = true;
            ShieldCount -= Time.deltaTime;
            if (ShieldCount < 2.5f && ShieldCount != 0)
            {
                MR.material = ShieldMt[1];
            }
            else
            {
                MR.material = ShieldMt[0];
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        LevelUp();
        PainCount();
        HpBars();
        Shielding();
    }
    void HpBars()
    {
        HpBar.fillAmount = Hp / MaxHp;
        PainBar.fillAmount = Pain / MaxPain;
        ExpBar.fillAmount = Exp / MaxExp;
        BoomCoolTime.fillAmount = BoomTime / MaxBoomTime;
        HpText.text = $"{MaxHp} / {Hp}";
        PainText.text = $"{MaxPain} / {Pain:N0}";
        ExpText.text = $"{MaxExp} / {Exp}";
        LevelText.text = $"{Level}";
        BoomCount.text = $"{Boom}";
        ScoreText.text = $"Score : {Score}";
        if (Hp >= MaxHp)
        {
            Hp = MaxHp;
        }
        if (Pain >= MaxPain)
        {
            Pain = MaxPain;
        }
    }
    void LevelUp()
    {
        if (Exp >= MaxExp)
        {
            Exp = 0;
            MaxExp += 10 * Level;
            Level++;
            Damage++;
            MaxHp += 5;
        }
    }
    void PainCount()
    {
        Pain += (Time.deltaTime / 3);
    }
    void GameOver()
    {

    }
}
