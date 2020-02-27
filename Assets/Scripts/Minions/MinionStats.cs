using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Minion Stats", menuName = "ScriptableObjects/Minions/Minion Stats")]
public class MinionStats : ScriptableObject
{
    public float life;
    public int damage;
    public float speed;
    public AudioClip riseSound;
    public AudioClip attackingCastleSound;


}
