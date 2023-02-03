using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ_2023 {
    public class NerveLine : MonoBehaviour {
        [SerializeField] private LineRenderer lineRenderer;
        private List<NervePoint> nervePointList;

        private void Awake() {
            nervePointList = new List<NervePoint>();
        }

        public void AddNerve(NervePoint nervePoint) {
            if (nervePointList.Contains(nervePoint)) return;
            
            nervePointList.Add(nervePoint);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, nervePoint.transform.position);
        }

        public void DestroySelf() {
            StartCoroutine(DestroySelfRoutine());
        }
        
        private IEnumerator DestroySelfRoutine() {
            Destroy(gameObject, 3);
            yield break;
        }

        public List<NervePoint> GetNervPointList() {
            return nervePointList;
        }
    }
}
