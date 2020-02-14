using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Stats", menuName = "ScriptableObjects/Player/Character Stats")]
public class CharacterStats : ScriptableObject
{
    public GameObject projectilePrefab;
    public GameObject meleeAttackPrefab;

    public GameObject projectilePrefabPlayer2;
    public GameObject meleeAttackPrefabPlayer2;

    public RuntimeAnimatorController animatorController;
}
