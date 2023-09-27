using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    private int maxHealth;
    public int MaxHealth
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

    private int stamina;
    public int Stamina
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

    private int hunger;
    public int Hunger
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

    private int thirst;
    public int Thirst
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
        maxHealth = _maxHealth;
        stamina = _stamina;
        hunger = _hunger;
        thirst = _thirst;
    }
}
