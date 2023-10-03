using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{

    public event Action<Status> StatusChanged;

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
            if (curHealth != value) { CallStatusChangedEvent(); }
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
            if (curHealth != value) { CallStatusChangedEvent(); }
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
            if (curHealth != value) { CallStatusChangedEvent(); }
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
            if (curHealth != value) { CallStatusChangedEvent(); }
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

    public void CallStatusChangedEvent()
    {
        StatusChanged?.Invoke(this);
    }
}
