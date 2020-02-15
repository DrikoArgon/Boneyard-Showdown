using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleManager : MonoBehaviour
{

    public CharacterCastle castleInfo;

    public SummonCircle lowLevelScroll;
    public SummonCircle midLevelScroll;
    public SummonCircle highLevelScroll;


    public enum BelongsToPlayer {
        Player1,
        Player2
    }

    public BelongsToPlayer belongsToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if(belongsToPlayer == BelongsToPlayer.Player1) {
            castleInfo = (CharacterCastle)Instantiate(Resources.Load("ScriptableObjects/Castles/" + GameManager.instance.player1ChosenCharacter.ToString() + "Castle"));
        } else {
            castleInfo = (CharacterCastle)Instantiate(Resources.Load("ScriptableObjects/Castles/" + GameManager.instance.player2ChosenCharacter.ToString() + "Castle"));
        }

        lowLevelScroll.SetupScrollSprites(castleInfo.lowLevelMinionScrollSpriteOff, castleInfo.lowLevelMinionScrollSpriteOn);
        midLevelScroll.SetupScrollSprites(castleInfo.midLevelMinionScrollSpriteOff, castleInfo.midLevelMinionScrollSpriteOn);
        highLevelScroll.SetupScrollSprites(castleInfo.highLevelMinionScrollSpriteOff, castleInfo.highLevelMinionScrollSpriteOn);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
