using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Assets
{
    [RequireComponent(typeof(Collider2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private float delayTime;
        [SerializeField] private float startDistance;
        
        [SerializeField] private bool activate;
       [SerializeField] private bool startTimer;
         [SerializeField] private float nowTime;
        [SerializeField] private int nowIndex;
        private List<Vector3> _record = new();

        private void Awake()
        {
            activate = false;
            nowTime = 0f;
            nowIndex = 0;
        }

        private void Update()
        {
            if (startTimer)
            {
                nowTime += Time.deltaTime;
                if (nowTime >= delayTime)
                {
                    startTimer = false;
                    activate = true;
                }
            }

            if (!target) return;
            if (Vector3.Distance(target.transform.position, transform.position) > startDistance && (!activate && !startTimer))
            {
                startTimer = true;
            }
        }

        private void FixedUpdate()
        {
            if (!target) return;
            _record.Add(target.transform.position);
            if (!activate) return;
            if (nowIndex == _record.Count) return;
            var targetPosition = _record[nowIndex];
            transform.position = targetPosition;
            nowIndex++;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<playerlife>(out var component))
            {
                if (activate) Destroy(other.gameObject);
            }
        }
    }
}
