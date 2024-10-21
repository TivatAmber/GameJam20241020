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
        [ReadOnly] [SerializeField] private Transform spawn;
        [ReadOnly] [SerializeField] private bool auto;
        [ReadOnly] [SerializeField] private int nowTargetIndex;
        [ReadOnly] [SerializeField] private MoveRecord nowRecord;
        [ReadOnly] [SerializeField] private Vector3 nowSpeed;
        [ReadOnly] [SerializeField] private int slowDownRatio;

        [SerializeField] private float accelerate;
        [SerializeField] private float maxSpeed;
        [SerializeField] private List<MoveRecord> record = new();
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
            nowSpeed += forward * (accelerate * Time.deltaTime);
            if (nowSpeed.magnitude > maxSpeed) nowSpeed = nowSpeed.normalized * maxSpeed;
            transform.position += nowSpeed * Time.deltaTime;
        }

        public void MoveWithSpeed(Vector3 speed)
        {
            transform.position += speed * Time.deltaTime;
            nowSpeed = speed;
        }

        public bool GetNowTarget() => Vector3.Distance(transform.position, nowRecord.Pos) < 0.3f;

        public void NextTarget()
        {
            nowTargetIndex += 1;
            if (nowTargetIndex >= record.Count)
            {
                var tempSpeed = nowRecord.Speed / Mathf.Exp(1.0f * (nowTargetIndex - record.Count) / slowDownRatio);
                Debug.Log(tempSpeed);
                nowRecord = new MoveRecord(tempSpeed, transform.position);
                return;
            }
            nowRecord = record[nowTargetIndex];
        }
        private void Record() => record.Add(new MoveRecord(nowSpeed, transform.position));
        public void ClearRecord() => record.Clear();
        protected virtual void Awake()
        {
            ClearRecord();
            var size = SpawnManager.Instance.PointRadius * 2;
            transform.localScale = new Vector3(size, size, 1f);
            slowDownRatio = TangledManager.Instance.SlowDownRatio;
        }

        // protected virtual void Update()
        // {
        // }

        private void FixedUpdate()
        {
            if (!auto) Record();
            else
            {
                MoveWithSpeed(nowRecord.Speed);
                if (GetNowTarget()) NextTarget(); 
            }
        }
    }
}