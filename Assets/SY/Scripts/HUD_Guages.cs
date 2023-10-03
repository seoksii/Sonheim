using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Guages : MonoBehaviour
{
    public RectTransform hpGuageFill;
    public RectTransform hungerGuageFill;
    public RectTransform staminaGuageFill;
    public RectTransform thirstyGuageFill;


    private void Start()
    {
        GameManager.Instance.Player.status.StatusChanged += OnStatusChanged;
    }

    public void OnStatusChanged(Status newStatus)
    {
        Vector2 hp = hpGuageFill.sizeDelta;
        Vector2 stamina = staminaGuageFill.sizeDelta;
        Vector2 hunger = hungerGuageFill.sizeDelta;
        Vector2 thirsty = thirstyGuageFill.sizeDelta;

        hp.x = (newStatus.CurHealth / newStatus.MaxHealth) * 300f;
        hpGuageFill.sizeDelta = hp;
        stamina.x = newStatus.Stamina * 3f;
        hpGuageFill.sizeDelta = hp;
        hunger.x = newStatus.Hunger * 3f;
        hpGuageFill.sizeDelta = hp;
        thirsty.x = newStatus.Thirst * 3f;
        hpGuageFill.sizeDelta = hp;

    }
}
