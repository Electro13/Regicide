﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillGod
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Idle Animations")]
        public string Right_Arm_Idle;
        public string Left_Arm_Idle;

        [Header("One Handed Attack Animations")]
        public string OH_Light_Attack_01;
        public string OH_Heavy_Attack_01;

        [Header("Two Handed Attack Animations")]
        public string TH_Light_Attack_01;
        public string TH_Heavy_Attack_01;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;
    }
}
