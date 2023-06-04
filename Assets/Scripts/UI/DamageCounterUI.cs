using System.Collections.Generic;
using UnityEngine;

public class DamageCounterUI : MonoBehaviour
{
   public static DamageCounterUI Instance { private set; get; }

   [SerializeField] private Transform canvas;
   [SerializeField] private DamageCounter damageCounterPrefab;

   private List<DamageCounter> _damageCountersPool;

   private void Awake()
   {
      Instance = this;

      _damageCountersPool = new List<DamageCounter>();
   }

   public void RenderTextDamage(float damageAmount, Vector3 location)
   {
      int roundedDamageAmount = Mathf.RoundToInt(damageAmount);

      DamageCounter newDamageCounter = GetDamageCounterFromPool();
      newDamageCounter.Setup(roundedDamageAmount, location);
   }
   
   private DamageCounter GetDamageCounterFromPool()
   {
      DamageCounter damageCounter;

      if (_damageCountersPool.Count == 0)
      {
         damageCounter = Instantiate(damageCounterPrefab, canvas);
      }
      else
      {
         damageCounter = _damageCountersPool[0];
         _damageCountersPool.RemoveAt(0);
      }

      return damageCounter;
   }

   public void PlaceDamageCounterInPool(DamageCounter damageCounter)
   {
      damageCounter.gameObject.SetActive(false);
      
      _damageCountersPool.Add(damageCounter);
   }
}
