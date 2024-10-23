using System.Collections.Generic;
using Tools;
using UnityEditor;
using UnityEngine;

namespace ShootGame
{
    public class ShootGameManager: BaseManager<ShootGameManager>
    {
        [ReadOnly] [SerializeField] private Camera mainCamera;
        [ReadOnly] [SerializeField] private float nowSummonInterval;
        [ReadOnly] [SerializeField] private int goodNum;
        [ReadOnly] [SerializeField] private int badNum;
        [HideInInspector] [ReadOnly] [SerializeField] private List<float> wordsProbList = new();
        [HideInInspector] [ReadOnly] [SerializeField] private List<float> tempProbList = new();

        [SerializeField] private float summonInterval;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private List<GameObject> wordsPrefab;
        protected override void OnStart()
        {
            mainCamera = Camera.main;
            goodNum = 0;
            badNum = 0;
        }
        public GameObject BulletPrefab => bulletPrefab;
        public IReadOnlyList<GameObject> WordsPrefab => wordsPrefab.AsReadOnly();
        public IList<float> WordsProbList => wordsProbList;
        public IList<float> TempProbList => tempProbList;
        public Camera MainCamera => mainCamera;
        public void AddGoodNum()
        {
            goodNum++;
        }

        public void AddBadNum()
        {
            badNum++;
        }

        private void Update()
        {
            if (nowSummonInterval > summonInterval)
            {
                Summon();
                nowSummonInterval -= summonInterval;
            }
            else nowSummonInterval += Time.deltaTime;
        }

        private void Summon()
        {
            var tar = Random.Range(0f, 1f);
            float lst = 0f, nowSum = 0f;
            for (var i = 0; i < wordsPrefab.Count; i++)
            {
                nowSum += wordsProbList[i];
                if (lst < tar && tar <= nowSum)
                {
                    Instantiate(wordsPrefab[i]);
                    break;
                }
                lst = nowSum;
            }
        }

        public override void EndGame()
        {
            base.EndGame();
        }
    }
}