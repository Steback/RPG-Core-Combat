using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class Portal : MonoBehaviour
    {
        enum Destination
        {
            A, B, C, D, E
        }
        
        [SerializeField] private int sceneToLoad = -1;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Destination destination;
        [SerializeField] private float fadeWaitTime = 0.5f;
        [SerializeField] private float fadeOutTime = 1f;
        [SerializeField] private float fadeInTime = 1f;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime);

            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();
            
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            
            wrapper.Load();

            Portal portal = GetOtherPortal();
            UpdatePlayer(portal);
            
            wrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);
            
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal portal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(portal.spawnPoint.position);
            player.transform.rotation = portal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;

                    return portal;
            }

            return null;
        }
    }
}
