using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Transform target;
    // Start is called before the first frame update

    GameObject enemy;
    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy"); 
        Debug.Log(enemy.name);
    }

    // Update is called once per frame
    void Update()
    {
        AimWeapon();
    }
    private void AimWeapon()
    {
        weapon.transform.LookAt(enemy.transform);
    }
}
