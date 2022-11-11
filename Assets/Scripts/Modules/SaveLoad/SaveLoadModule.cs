using System.IO;
using Modules.SaveLoad.Data;
using UnityEngine;

namespace Modules.SaveLoad
{
    public interface ISaveLoadModule
    {
        void Save(ListsData data);
        ListsData Load();
        // void SaveCharacters(CharactersData data, string filePath);
        // LayersData LoadLayers(string filePath);
    }

    public class SaveLoadModule : ISaveLoadModule
    {
        private string _fileName = "Lists";
        public void SaveLayers(ListsData data, string filePath)
        {
            var json = JsonUtility.ToJson(data);
            using var writer = new StreamWriter(File.Create(filePath));
            writer.Write(json);
        }
        // public ListsData LoadLayers(string filePath)
        // {
        //     using var reader = new StreamReader(filePath);
        //     var json = reader.ReadToEnd();
        //     var data = JsonUtility.FromJson<ListsData>(json);
        //     return data;
        // }
        //
        // private static void Save(object data, string filePath)
        // {
        //     var json = JsonUtility.ToJson(data);
        //     using var writer = new StreamWriter(File.Create(filePath));
        //     writer.Write(json);
        // }
        
        public ListsData Load()
        {
            using var reader = new StreamReader(GetFilePath());
            var json = reader.ReadToEnd();
            var data = JsonUtility.FromJson<ListsData>(json);
            return data;
        }

        public void Save(ListsData data)
        {
            var json = JsonUtility.ToJson(data);
            using var writer = new StreamWriter(File.Create(GetFilePath()));
            writer.Write(json);
        }

        private string GetFilePath()
        {
            return Application.dataPath + "/" + _fileName;
        }
    }
}