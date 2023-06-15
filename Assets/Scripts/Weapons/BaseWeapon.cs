using System;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected Transform holder;
    [SerializeField] protected WeaponObjectSO weaponObjectSO;

    protected float spawnTimer;
    protected bool isReady;

    private void Start()
    {
        Setup();
    }

    protected virtual void Setup() {}
}
