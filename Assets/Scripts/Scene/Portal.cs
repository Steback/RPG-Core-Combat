using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private int sceneToLoad = -1;
        
        private void OnTriggerEnter(Collider other)
        {
            print(other);
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
