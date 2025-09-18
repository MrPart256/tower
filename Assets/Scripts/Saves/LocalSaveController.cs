using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UniRx;
using UnityEngine;

namespace Saves
{
    public class LocalSaveController : ISaveController
    {
        private const string SaveName = "Save.file";
        
        public void Save(Save save)
        {
            using (FileStream stream = new FileStream(GetSavePath(), FileMode.OpenOrCreate))
            {
                SerializeSave(save, stream);
            }
        }

        public IObservable<Save> Load()
        {
            return Observable.Start(DeserializeSave, Scheduler.MainThreadIgnoreTimeScale);
        }

        private void SerializeSave(Save save, Stream stream)
        {
            var formatter = new BinaryFormatter();
            
            formatter.Serialize(stream, save);

        }

        private Save DeserializeSave()
        {
            Save save = new();

            var savePath = GetSavePath();
            
            if (File.Exists(savePath))
            {
                using (FileStream stream = new FileStream(savePath, FileMode.Open))
                {
                    var formatter = new BinaryFormatter();
                    save = (Save)formatter.Deserialize(stream);
                }
            }

            return save;
        }
        
        private string GetSavePath()
        => Path.Combine(Application.persistentDataPath, SaveName);
    }
}