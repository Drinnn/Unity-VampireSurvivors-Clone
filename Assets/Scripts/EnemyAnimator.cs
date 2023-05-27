using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAnimator : MonoBehaviour
{
   [SerializeField] private Transform spriteTransform;
   [SerializeField] private float animationSpeed = 0.5f;
   [SerializeField] private float minSize = 0.9f;
   [SerializeField] private float maxSize = 1.1f;

   private float _activeSize;
   
   private void Start()
   {
      _activeSize = maxSize;

      animationSpeed = animationSpeed * Random.Range(.75f, 1.25f);
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
}
