using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents
{
    public static UnityAction<GameObject, float> CharacterDamaged;
    public static UnityAction<GameObject, float> CharacterHealed;
    public static UnityEvent<GameObject, float> BossRage;
}
