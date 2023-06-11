using UnityEngine;

[CreateAssetMenu]
public class WeaponObjectSO : ScriptableObject
{
   public string weaponObjectName;
   public SerializableDictionary<int, WeaponStats> levelStatsDictionary;
   public GameObject weaponObjectPrefab;
}
