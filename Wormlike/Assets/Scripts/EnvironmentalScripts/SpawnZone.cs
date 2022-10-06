using ManagerScripts;
using UnityEngine;

namespace StaticsAndUtilities
{
    public class SpawnZone : MonoBehaviour
    {
        public Vector3 SpawnPoint()
        {
            Vector3 position = new Vector3();
            position.x = transform.position.x + Random.Range(0f,5f);
            position.y = transform.position.y;
            position.z = transform.position.z + Random.Range(0f,5f);
            Debug.Log("Spawning at position " + position);
            return position;
        } 

        public void Start()
        {
            RegisterWithMapManager();
        }

        public void RegisterWithMapManager()
        {
            ServiceLocator.Current.Get<MapManager>().RegisterSpawnZone(this);
        }
    }
}
