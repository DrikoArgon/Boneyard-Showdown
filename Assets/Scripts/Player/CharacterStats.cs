using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Stats", menuName = "ScriptableObjects/Player/Character Stats")]
public class CharacterStats : ScriptableObject
{
    public GameObject projectilePrefab;
    public GameObject meleeAttackPrefab;

    public RuntimeAnimatorController animatorController;
}
