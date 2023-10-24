using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField] protected int _exp;
    [SerializeField] protected int _gold;

    //public int Exp { get { return _exp; } set { _exp = value; } }
    public int Exp
    {
        get { return _exp; }
        set
        {
            _exp = value;
            //Checking Level-Up

            int level = Level;
            while (true)
            {
                Data.Stat stat;
                if (Managers.Data.StatDict.TryGetValue(level + 1, out stat) == false)
                    break;
                if (_exp < stat.totalExp)
                    break;
                level++;
            }

            if (level != Level)
            {
                Debug.Log("Level Up");
                Level = level;
                SetStat(Level);
            }
        }
    }
    public int Gold { get { return _gold; } set { _gold = value; } }

    private void Start()
    {
        _level = 1;
        _exp = 0;
        _def = 5;
        _moveSpeed = 7.0f;
        _gold = 0;

        SetStat(_level);
    }

    private void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dic = Managers.Data.StatDict;
        Data.Stat stat = dic[level];

        _hp = stat.maxHp;
        _maxHp = stat.maxHp;
        _atk = stat.atk;
    }

    protected override void OnDead(Stat attacker)
    {
        Managers.Game.Despawn(gameObject);
        Debug.Log("Player Dead");
    }
}
