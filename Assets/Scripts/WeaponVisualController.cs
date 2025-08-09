using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisualController : MonoBehaviour
{
    [SerializeField] private Transform[] gunTranforms;
    [SerializeField] private Transform pistol;
    [SerializeField] private Transform revolver;
    [SerializeField] private Transform autoRifle;
    [SerializeField] private Transform shotgun;
    [SerializeField] private Transform rifle;

    private void SwitchWeaponVisual(Transform weaponTransform)
    {
        SwitchOffGuns();
        if (weaponTransform != null)
        {
            weaponTransform.gameObject.SetActive(true);
        }
    }

    private void SwitchOffGuns()
    {
        foreach (Transform gun in gunTranforms)
        {
            gun.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeaponVisual(pistol);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeaponVisual(revolver);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeaponVisual(autoRifle);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchWeaponVisual(shotgun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchWeaponVisual(rifle);
        }
    }
}
