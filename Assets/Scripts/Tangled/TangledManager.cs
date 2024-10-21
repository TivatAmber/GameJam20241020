using System;
using System.Collections.Generic;
using Tangled.Balls;
using Tools;
using UnityEngine;

namespace Tangled
{
    public class TangledManager: Singleton<TangledManager>
    {
        private List<BaseBall> _balls = new();
        [ReadOnly] [SerializeField] private int nowActiveBall;
        [ReadOnly] [SerializeField] private int nowTurn;
        [ReadOnly] [SerializeField] private bool endGame;
        [ReadOnly] [SerializeField] private float nowTime;
        
        [SerializeField] private int slowDownRatio;
        [SerializeField] private float maxTime;
        [SerializeField] private float friction;
        [SerializeField] private float accelerate;
        [SerializeField] private GameObject whiteBallPrefab;
        [SerializeField] private GameObject blackBallPrefab;

        [SerializeField] private KeyCode upOrder;
        [SerializeField] private KeyCode downOrder;
        [SerializeField] private KeyCode leftOrder;
        [SerializeField] private KeyCode rightOrder;
        public Transform WhiteBallPos => _balls[0].transform;
        public float Friction => friction;
        public float Accelerate => accelerate;
        protected override void OnStart()
        {
            nowTime = 0f;
            nowActiveBall = 0;
            var spawn = SpawnManager.Instance.WhiteSpawnPoint;
            var whiteBall = Instantiate(whiteBallPrefab, spawn.position, Quaternion.identity).GetComponent<WhiteBall>();
            _balls.Add(whiteBall);
            whiteBall.Spawn = spawn;
            whiteBall.Reset();
            endGame = false;
        }

        Vector3 GetForward()
        {
            var ret = Vector3.zero;
            if (Input.GetKey(upOrder)) ret += Vector3.up;
            if (Input.GetKey(downOrder)) ret += Vector3.down;
            if (Input.GetKey(leftOrder)) ret += Vector3.left;
            if (Input.GetKey(rightOrder)) ret += Vector3.right;
            return ret;
        }
        private void Update()
        {
            if (endGame) return;
            var forward = GetForward();
            _balls[nowActiveBall].Move(forward);
            nowTime += Time.deltaTime;
            if (nowTime >= maxTime)
            {
                RestartTurn();
                nowTime = 0f;
            }
        }

        public void WhiteDestroy()
        {
            if ((nowTurn & 1) == 1) NextTurn();
            else RestartTurn();
        }

        public void WhiteGetTarget()
        {
            if ((nowTurn & 1) == 0) NextTurn();
            else RestartTurn();
        }

        void TurnReset()
        {
            nowTime = 0f;
            foreach (var ball in _balls) ball.Reset();
        }
        public void RestartTurn()
        {
            TurnReset();
            _balls[nowActiveBall].ClearRecord();
        }

        public void NextTurn()
        {
            if (endGame) return;
            TurnReset();
            _balls[nowActiveBall].Auto = true;
            nowTurn += 1;
            if ((nowTurn & 1) == 0)
            {
                nowActiveBall = 0;
                _balls[nowActiveBall].Auto = false;
                _balls[nowActiveBall].ClearRecord();
            }
            else
            {
                nowActiveBall = nowTurn / 2 + 1;
                var nowBlackBallSpawn = nowActiveBall - 1;
                if (nowBlackBallSpawn < SpawnManager.Instance.BlackSpawnPoints.Count)
                {
                    var spawn = SpawnManager.Instance.BlackSpawnPoints[nowBlackBallSpawn];
                    var newBlackBall = Instantiate(blackBallPrefab, spawn.position, Quaternion.identity).GetComponent<BlackBall>();
                    newBlackBall.Spawn = SpawnManager.Instance.BlackSpawnPoints[nowBlackBallSpawn];
                    newBlackBall.Reset();
                    newBlackBall.Auto = false;
                    _balls.Add(newBlackBall);
                }
                else EndGame();
            }
        }

        void EndGame()
        {
            endGame = true;
        }
    }
}