using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Status status = new Status(100, 100, 100, 100);
    public Weapon weapon;
    public GameObject weaponPoint;  

    public void EquipWeapon(GameObject weaponPrefab)
    {
        GameObject _weapon = Instantiate(weaponPrefab) as GameObject;
        _weapon.transform.SetParent(weaponPoint.transform, false);

        weapon = _weapon.GetComponent<Weapon>();
    }
}