using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ_2023.Nerves {
    public class NerveLine : MonoBehaviour {
        [SerializeField] private LineRenderer lineRenderer;
        private List<NervePoint> nervePointList;

        [NonSerialized] public Transform PlayerHand;

        private void Awake() {
            nervePointList = new List<NervePoint>();
            lineRenderer.positionCount = 1;
        }

        private void Update()
        {
            if(PlayerHand != null)
            {
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, PlayerHand.position);
            }
        }

        public void AddNerve(NervePoint nervePoint) {
            if (nervePointList.Contains(nervePoint)) return;
            
            nervePointList.Add(nervePoint);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 2, nervePoint.transform.position);
        }

        public void DestroySelf() {
            lineRenderer.positionCount--;
            PlayerHand = null;

            StartCoroutine(DestroySelfRoutine());
        }
        
        private IEnumerator DestroySelfRoutine() {
            for (var t = 1f; t>0; t -= Time.deltaTime / 3)
            {
                // Waste to make new material for this, but gamejam so I don't care (ITR)
                lineRenderer.material.color = new Color(1, 1, 1, t);
                yield return null;
            }
            Destroy(gameObject);
        }

        public List<NervePoint> GetNervPointList() {
            return nervePointList;
        }
    }
}
