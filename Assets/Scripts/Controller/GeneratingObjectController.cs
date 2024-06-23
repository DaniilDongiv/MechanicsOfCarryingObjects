using UnityEngine;

namespace Controller
{
    public class GeneratingObjectController : MonoBehaviour
    {
        public static GeneratingObjectController Instance;
        
        [SerializeField]
        private GameObject _objectPrefab;
        [SerializeField]
        private Transform _startPosition;
        [SerializeField]
        private Transform _endPosition;
    
        private int _numberOfObjectsToCreate = 5;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            
            GenerateObjects();
        }
        
        public void CreateObject()
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(_startPosition.position.x, _endPosition.position.x),
                Random.Range(_startPosition.position.y, _endPosition.position.y),
                Random.Range(_startPosition.position.z, _endPosition.position.z)
            );

            Instantiate(_objectPrefab, randomPosition, Quaternion.identity);
        }
        
        private void GenerateObjects()
        {
            for (int i = 0; i < _numberOfObjectsToCreate; i++)
            {
                CreateObject();
            }
        }
    }
}
