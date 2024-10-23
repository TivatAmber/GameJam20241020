using System.Collections.Generic;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Inter
{
    public class InterManager : BaseManager<InterManager>
    {
        [ReadOnly] [SerializeField] private float nowInterval;
        [ReadOnly] [SerializeField] private int lstImageIndex;
        [ReadOnly] [SerializeField] private int nowIndex;
        [SerializeField] private float showInterval;
        [SerializeField] private List<GameObject> objectList;

        public IReadOnlyList<GameObject> ObjectList
        {
            get => objectList.AsReadOnly();
            set => objectList = new List<GameObject>(value);
        }

        protected override void OnStart()
        {
            foreach (var obj in ObjectList) obj.SetActive(false);
            nowIndex = 0;
            lstImageIndex = 0;
        }

        private void Update()
        {
            nowInterval += Time.deltaTime;
            if (nowInterval < showInterval) return;
            nowInterval -= showInterval;
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space))
            {
                if (nowIndex == objectList.Count) EndGame();
                else
                {
                    var nowObj = objectList[nowIndex];
                    if (!nowObj) return;
                    if (nowObj.TryGetComponent<Image>(out var image))
                    {
                        while (lstImageIndex < nowIndex)
                        {
                            objectList[lstImageIndex].SetActive(false);
                            lstImageIndex++;
                        }
                    } 
                    nowObj.SetActive(true); 
                    nowIndex++;
                }
            }
        }
    }
}