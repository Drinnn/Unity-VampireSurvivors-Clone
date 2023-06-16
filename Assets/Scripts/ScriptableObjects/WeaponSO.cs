using UnityEngine;

[CreateAssetMenu]
public class WeaponSO : ScriptableObject
{
   public string weaponName;
   public SerializableDictionary<int, WeaponStats> levelStatsDictionary;
   public GameObject spawnable;
}
