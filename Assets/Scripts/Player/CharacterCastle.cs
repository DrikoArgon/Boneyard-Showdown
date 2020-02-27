using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Stats", menuName = "ScriptableObjects/Player/Character Castle")]
public class CharacterCastle : ScriptableObject
{
    public Sprite lowLevelMinionScrollSpriteOff;
    public Sprite lowLevelMinionScrollSpriteOn;

    public Sprite midLevelMinionScrollSpriteOff;
    public Sprite midLevelMinionScrollSpriteOn;

    public Sprite highLevelMinionScrollSpriteOff;
    public Sprite highLevelMinionScrollSpriteOn;

    public Sprite summoningAltarSprite;

    public GameObject player1Level1MinionPrefab;
    public GameObject player1Level2MinionPrefab;
    public GameObject player1Level3MinionPrefab;

    public GameObject player2Level1MinionPrefab;
    public GameObject player2Level2MinionPrefab;
    public GameObject player2Level3MinionPrefab;
}
