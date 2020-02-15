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
}
