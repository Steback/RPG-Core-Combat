using Saving;
using UnityEngine;

namespace Scene
{
    public class SavingWrapper : MonoBehaviour
    {
        private const string DefaultSavingFile = "save";
        
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
