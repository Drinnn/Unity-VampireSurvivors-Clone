using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAnimator : MonoBehaviour
{
   [SerializeField] private EnemyController enemyController;

   [SerializeField] private Color baseColor = Color.white;
   [SerializeField] private Color onDamageColor;
   [SerializeField] private float damageTransitionTime = 1f;
   
   [SerializeField] private Transform spriteTransform;
   [SerializeField] private float animationSpeed = 0.5f;
   [SerializeField] private float minSize = 0.9f;
   [SerializeField] private float maxSize = 1.1f;

   public float DamageTransitionTime => damageTransitionTime;

   private SpriteRenderer _spriteRenderer;
   
   private float _activeSize;

   private void Awake()
   {
      _spriteRenderer = GetComponent<SpriteRenderer>();
   }

   private void Start()
   {
      enemyController.OnTookDamage += EnemyController_OnTookDamage;
      
      _activeSize = maxSize;

      animationSpeed *= Random.Range(.75f, 1.25f);
   }

   private void EnemyController_OnTookDamage(object sender, EnemyController.OnTookDamageEventArgs e)
   {
      if (e.IsFatal)
      {
         StartCoroutine(nameof(AnimateDeathColorTransition));
      }
      else
      {
         StartCoroutine(nameof(AnimateDamageColorTransition));
      }
   }
   
   private IEnumerator AnimateDamageColorTransition()
   {
      float elapsedTime = 0f;
      while (elapsedTime < damageTransitionTime)
      {
         _spriteRenderer.color = Color.Lerp(baseColor, onDamageColor, elapsedTime / damageTransitionTime);
         elapsedTime += Time.deltaTime;
         yield return null;
      }

      _spriteRenderer.color = baseColor;
   }
   
   private IEnumerator AnimateDeathColorTransition()
   {
      float elapsedTime = 0f;
      while (elapsedTime < damageTransitionTime)
      {
         _spriteRenderer.color = Color.Lerp(baseColor, onDamageColor, elapsedTime / damageTransitionTime);
         elapsedTime += Time.deltaTime;
         yield return null;
      }
   }
   
   private void Update()
   {
      spriteTransform.localScale = Vector3.MoveTowards(spriteTransform.localScale, Vector3.one * _activeSize,
         animationSpeed * Time.deltaTime);
      if (spriteTransform.localScale.x == _activeSize)
      {
         if (_activeSize == maxSize)
         {
            _activeSize = minSize;
         }
         else
         {
            _activeSize = maxSize;
         }
      }
   }

   private void OnDestroy()
   {
      enemyController.OnTookDamage -= EnemyController_OnTookDamage;
   }
}
