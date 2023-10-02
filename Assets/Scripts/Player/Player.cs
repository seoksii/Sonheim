using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Status status = new Status(100, 100, 100, 100);
    public Transform weaponPoint;

    public void EquipWeapon(GameObject weapon)
    {
        Instantiate(weapon).transform.parent = weaponPoint;
    }
}