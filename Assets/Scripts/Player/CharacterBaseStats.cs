using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Base Stats", menuName = "ScriptableObjects/Player/Base Stats")]
public class CharacterBaseStats : ScriptableObject
{
    public float speed;
    public float maxLife;
    public float initialMagicDamage;
    public float initialMeleeDamage;
    public float rangedAttackCooldown;
    public float meleeAttackCooldown;

    public float baseRangedDamageModifier;
    public float baseMeleeDamageModifier;
    public float baseMaxLifeModifier;

    public int maxLevel;
}
