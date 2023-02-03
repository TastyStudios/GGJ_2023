using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GGJ_2023 {
    public class PlayerInteract : MonoBehaviour {
        [SerializeField] private NerveLine nerveLine;
        
        private HashSet<NervePoint> nervePointsInRange;
        private NervePoint currentNervePoint;

        private void Awake() {
            nervePointsInRange = new HashSet<NervePoint>();
        }
        
        private void SetCurrentNervePoint(NervePoint nervePoint) {
            if (currentNervePoint == nervePoint) return;
            
            if (currentNervePoint is not null) {
                currentNervePoint.RemoveHighlight();
            }

            if (nervePoint != null) {
                nervePoint.SetHighlight();
            }
            
            currentNervePoint = nervePoint;
            
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (!col.TryGetComponent<NervePoint>(out NervePoint nervePoint)) return;
            
            if (!nervePointsInRange.Add(nervePoint)) return;
            
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (!other.TryGetComponent<NervePoint>(out NervePoint nervePoint)) return;
            
            if (!nervePointsInRange.Remove(nervePoint)) return;
        }

        private void FixedUpdate() {
            RecalculateClosestNervePoint();
        }

        private void RecalculateClosestNervePoint() {
            float minDistance = float.MaxValue;

            NervePoint closestNervePoint = null;
            foreach (NervePoint nervePoint in nervePointsInRange) {
                float distance = Vector2.SqrMagnitude(transform.position - nervePoint.transform.position);
                
                if (distance < minDistance) {
                    minDistance = distance;
                    closestNervePoint = nervePoint;
                }
                    
            }
            
            SetCurrentNervePoint(closestNervePoint);
        }

        public void Interact() {
            if (currentNervePoint == null) return;
            
            nerveLine.AddNerve(currentNervePoint);
        }
    }
}