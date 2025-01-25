using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Etc
{
    public class ScaleIn : MonoBehaviour
    {
        public float StartScale = 2f;
        public float Time = 0.25f;

        public bool ScaleOnSpawn;

        public void ScaleNow()
        {
            StartCoroutine(CoScaleIn());
        }

        private IEnumerator CoScaleIn()
        {
            float timer = 0;
            Vector3 oldScale = Vector3.one * StartScale;
            Vector3 newScale = transform.localScale;

            while (timer < Time)
            {
                transform.localScale = Vector3.Lerp(oldScale, newScale, timer / Time);
                timer += UnityEngine.Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            transform.localScale = newScale;
        }

        private void Start()
        {
            if (ScaleOnSpawn)
                ScaleNow();
        }
    }
}
