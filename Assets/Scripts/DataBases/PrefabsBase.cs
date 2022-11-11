using System;
using UnityEngine;

namespace DataBases
{
    public interface IPrefabsBase
    {
        GameObject Get(string name);
    }
    
    [CreateAssetMenu(menuName = "Bases/PrefabsBase", fileName = "PrefabsBase")]
    public class PrefabsBase : ScriptableObject, IPrefabsBase
    {
        [SerializeField] private Prefab[] prefabs;

        public GameObject Get(string prefabName)
        {
            foreach (var prefab in prefabs)
            {
                if (prefab.Name == prefabName)
                    return prefab.GameObject;
            }

            throw new Exception("[PrefabsBase] Can't find prefab with name: " + prefabName);
        }

        [Serializable]
        public class Prefab
        {
            public string Name;
            public GameObject GameObject;
        }
    }
}