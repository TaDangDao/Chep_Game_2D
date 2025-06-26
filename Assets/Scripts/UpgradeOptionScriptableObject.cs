using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName ="Upgrade",menuName ="Scriptable/UpgradeOption")]
public class UpgradeOptionScriptableObject : ScriptableObject
{
    public string upgradename;
    public int statsUpgrade;
    public int goldConsume;
    public Sprite Icon; 
}
