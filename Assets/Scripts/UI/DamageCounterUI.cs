using UnityEngine;

public class DamageCounterUI : MonoBehaviour
{
   public static DamageCounterUI Instance { private set; get; }

   [SerializeField] private Transform canvas;
   [SerializeField] private DamageCounter damageCounterPrefab;

   private void Awake()
   {
      Instance = this;
   }

   public void RenderTextDamage(float damageAmount, Vector3 location)
   {
      int roundedDamageAmount = Mathf.RoundToInt(damageAmount);

      DamageCounter newDamageCounter = Instantiate(damageCounterPrefab, location, Quaternion.identity, canvas);
      newDamageCounter.Setup(roundedDamageAmount);
   }
}
