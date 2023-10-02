using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    private float curHealth;
    public float CurHealth
    {
        get
        {
            return curHealth;
        }
        set
        {
            curHealth = value;
        }
    }

    private float maxHealth;
    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            maxHealth = value;
        }
    }

    private float stamina;
    public float Stamina
    {
        get
        {
            return stamina;
        }
        set
        {
            stamina = value;
        }
    }

    private float hunger;
    public float Hunger
    {
        get
        {
            return hunger;
        }
        set
        {
            hunger = value;
        }
    }

    private float thirst;
    public float Thirst
    {
        get
        {
            return thirst;
        }
        set
        {
            thirst = value;
        }
    }

    public Status(int _maxHealth, int _stamina, int _hunger, int _thirst)
    {
        curHealth = _maxHealth;
        maxHealth = _maxHealth;
        stamina = _stamina;
        hunger = _hunger;
        thirst = _thirst;
    }
}
