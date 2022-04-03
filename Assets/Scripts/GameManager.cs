using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    //[Header("ÇÃ·¹ÀÌ¾î º¯¼öµé")]
    public float Hp, MaxHp, Pain, MaxPain, Stage, EnemyDead, Damage, ShootLevel, 
        ShootTime, Speed, Exp, MaxExp, Level, Boom, MaxBoom, Score,
        BoomTime, MaxBoomTime; //Ã¼·Â ¸ÆÃ¼·Â °íÅë ¸Æ°íÅë ½ºÅ×ÀÌÁö Àû Á×Àº ¼ö µ¥¹ÌÁö ¹ß»ç ¼Óµµ ½ºÇÇµå °æÇèÄ¡ ·¹º§ ÆøÅº ¸Æ ÆøÅº Á¡¼ö ÆøÅº ÄðÅ¸ÀÓ ÆøÅº ¸ÆÄðÅ¸ÀÓ
    [SerializeField] private GameObject AllUiObj;
    [SerializeField] private Image HpBar, PainBar, ExpBar, BoomCoolTime;
    [SerializeField] private Text HpText, PainText, ExpText, LevelText, BoomCount, ScoreText;
    private void Awake()
    {
        Score = 0;
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LevelUp();
        PainCount();
        HpBars();
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
        if(Pain >= MaxPain)
        {
            Pain = MaxPain;
        }
    }
    void LevelUp()
    {
        if(Exp >= MaxExp)
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
