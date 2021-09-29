using System;
using System.Collections;
using Saving;
using UnityEngine;

namespace Scene
{
    public class SavingWrapper : MonoBehaviour
    {
        private const string DefaultSavingFile = "save";
        [SerializeField] private float fadeInTime = 0.2f;

        private IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(DefaultSavingFile);
            yield return fader.FadeIn(fadeInTime);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(DefaultSavingFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(DefaultSavingFile);
        }
    }
}
