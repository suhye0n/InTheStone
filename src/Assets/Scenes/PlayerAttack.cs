using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    public GameObject gun;
    private Gun currentGun;
    int currentBulletCount = 10;
    private bool isReload = false;

    void Update()
    {
        TryShoot();
    }

    private void TryShoot()
    {
        if (!isReload)
        {
            if (currentBulletCount > 0)
                Shoot();
        }
    }

    private void Shoot()
    {
        currentBulletCount--;
        Debug.Log(currentBulletCount);
    }
}
