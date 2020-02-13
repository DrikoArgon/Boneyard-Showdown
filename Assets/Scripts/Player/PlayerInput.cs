using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Input Mappings", menuName = "ScriptableObjects/Player/Player Input Mappings")]
public class PlayerInput : ScriptableObject
{
    public KeyCode moveUpKey;
    public KeyCode moveRightKey;
    public KeyCode moveLeftKey;
    public KeyCode moveDownKey;
    public KeyCode shootKey;
    public KeyCode stabKey;

    public string moveHorizontalGamepadAxis;
    public string moveVerticalGamepadAxis;
    public string shootMagicGamepadButton;
    public string stabGamepadButton;
}
