using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public BoxCollider WeaponHitbox;

    // Start is called before the first frame update
    void Start()
    {
        if (!WeaponHitbox) Debug.LogError("WeaponHitbox is null! Please assign a weapon.");
    }
    void EnableHitbox()
    {
        WeaponHitbox.enabled = true;
    }

    void DisableHitbox()
    {
        WeaponHitbox.enabled = false;
    }
}
