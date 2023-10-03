using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{

    public event Action<Status> OnStatusChanged;

    private float curHealth;
    public float CurHealth
    {
        get
        {
            return curHealth;
        }
        set
        {
            if (curHealth != value) { CallStatusChangedEvent(); }
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
            if (maxHealth != value) { CallStatusChangedEvent(); }
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
            if (stamina != value) { CallStatusChangedEvent(); }
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
            if (Hunger != value) { CallStatusChangedEvent(); }
            hunger = value;
            if (hunger >= 100f) hunger = 100f;
            if (hunger < 0f)
            {
                curHealth += hunger;
                hunger = 0f;
            }
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
            if (thirst != value) { CallStatusChangedEvent(); }
            thirst = value;
            if (thirst >= 100f) thirst = 100f;
            if (thirst < 0f)
            {
                curHealth += thirst;
                thirst = 0f;
            }
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

    public void CallStatusChangedEvent()
    {
        OnStatusChanged?.Invoke(this);
    }
}
