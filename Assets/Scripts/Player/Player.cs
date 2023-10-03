using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Status status = new Status(100, 100, 100, 100);
    public GameObject weaponPoint;
    private GameObject curEquipedWeapon;

    public void EquipWeapon(GameObject weaponPrefab)
    {
        GameObject weapon = Instantiate(weaponPrefab) as GameObject;
        weapon.transform.SetParent(weaponPoint.transform, false);
        curEquipedWeapon = weapon;
    }

    public void UnEquipWeapon(GameObject weaponPrefab)
    {
        Destroy(curEquipedWeapon);
        curEquipedWeapon = null;
    }
}
