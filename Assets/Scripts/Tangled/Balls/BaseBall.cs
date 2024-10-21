using System;
using System.Collections.Generic;
using System.Numerics;
using Tools;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

namespace Tangled.Balls
{
    public abstract class BaseBall : MonoBehaviour
    {
        [Serializable]
        private class MoveRecord
        {
            [ReadOnly] [SerializeField] private Vector3 speed;
            [ReadOnly] [SerializeField] private Vector3 pos;
            public Vector3 Speed => speed;
            public Vector3 Pos => pos;

            public MoveRecord(Vector3 speed, Vector3 pos)
            {
                this.speed = speed;
                this.pos = pos;
            }
        }

        private const float MinSpeed = 0.1f;
        [ReadOnly] [SerializeField] private Transform spawn;
        [ReadOnly] [SerializeField] private MoveRecord nowRecord;
        [ReadOnly] [SerializeField] private Vector3 nowSpeed;
        [ReadOnly] [SerializeField] private bool auto;
        [ReadOnly] [SerializeField] private int nowTargetIndex;
        [ReadOnly] [SerializeField] private float friction;
        [ReadOnly] [SerializeField] private float accelerate;
        [ReadOnly] [SerializeField] private float maxSpeed;
        
        [SerializeField] private List<MoveRecord> _record = new();
        public Transform Spawn
        {
            get => spawn;
            set => spawn = value;
        }

        public bool Auto
        {
            get => auto;
            set => auto = value;
        }

        public void Reset()
        {
            transform.position = spawn.position;
            nowRecord = new MoveRecord(Vector3.zero, spawn.position);
            nowSpeed = Vector3.zero;
            nowTargetIndex = 0;
        }

        public void Move(Vector3 forward)
        {
            if (forward.magnitude == 0)
            {
                if (nowSpeed.magnitude >= MinSpeed) nowSpeed += -nowSpeed.normalized * (friction * Time.deltaTime);
                else nowSpeed = Vector3.zero;
            }
            else nowSpeed += forward * (accelerate * Time.deltaTime);
            
            if (nowSpeed.magnitude > maxSpeed) nowSpeed = nowSpeed.normalized * maxSpeed;
            transform.position += nowSpeed * Time.deltaTime;
        }

        public void MoveWithSpeed(Vector3 speed)
        {
            nowSpeed = speed;
            transform.position += nowSpeed * Time.deltaTime;
        }

        public bool GetNowTarget() => Vector3.Distance(transform.position, nowRecord.Pos) < 0.3f;

        public void NextTarget()
        {
            nowTargetIndex += 1;
            if (nowTargetIndex >= _record.Count)
            {
                nowRecord = new MoveRecord(Vector3.zero, transform.position);
                return;
            }
            nowRecord = _record[nowTargetIndex];
        }
        private void Record() => _record.Add(new MoveRecord(nowSpeed, transform.position));
        public void ClearRecord() => _record.Clear();
        protected virtual void Awake()
        {
            ClearRecord();
            var size = SpawnManager.Instance.PointRadius * 2;
            transform.localScale = new Vector3(size, size, 1f);
            accelerate = TangledManager.Instance.Accelerate;
            friction = TangledManager.Instance.Friction;
        }

        private void FixedUpdate()
        {
            if (!auto) Record();
            else
            {
                if (nowTargetIndex < _record.Count) MoveWithSpeed(nowRecord.Speed);
                else Move(nowRecord.Speed);
                NextTarget();
            }
        }
    }
}