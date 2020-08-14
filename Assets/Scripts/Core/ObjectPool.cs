using System.Collections.Generic;
using UnityEngine;

namespace Bubbles
{
    public sealed class ObjectPool
    {
        private readonly GameObject _prefab;
        private Transform _parent;
        private readonly Queue<GameObject> _pool;

        public ObjectPool(GameObject prefab)
        {
            _prefab = prefab;
            _pool = new Queue<GameObject>();
        }

        public Transform Parent
        {
            get => _parent;
            set => _parent = value;
        }

        public GameObject Acquire(Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
        {
            var go = _pool.Count == 0 ? Object.Instantiate(_prefab) : _pool.Dequeue();

            if (_parent != null) go.transform.SetParent(_parent);

            go.transform.position = position;
            go.transform.rotation = rotation;
            go.SetActive(true);

            return go;
        }

        public void Release(GameObject go)
        {
            _pool.Enqueue(go);
            go.SetActive(false);
        }
    }
}