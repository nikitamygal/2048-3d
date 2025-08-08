using System;
using System.Reflection;
using MoreMountains.Tools;


namespace SoloGames.SaveLoad
{
    public class SaveLoadSystem : MMSingleton<SaveLoadSystem>
    {
        public SaveData Data => _saveData;

        protected SaveData _saveData;
        protected const string _saveFolderName = "SaveData/";
        protected const string _saveFileName = "savedata.file";
        protected BindingFlags _bindingFlags = BindingFlags.Public | BindingFlags.Instance;
        private Type _saveDataType;

        protected override void Awake()
        {
            base.Awake();
            Initialization();
        }

        public void Initialization()
        {
            LoadData();
            if (_saveData == null)
            {
                _saveData = new SaveData();
            }
            _saveDataType = _saveData.GetType();
        }

        public void LoadData()
        {
            SaveData data = (SaveData)MMSaveLoadManager.Load(typeof(SaveData), _saveFileName, _saveFolderName);
            if (data != null)
            {
                this._saveData = data;
            }
        }

        public void SaveData()
        {
            MMSaveLoadManager.Save(this._saveData, _saveFileName, _saveFolderName);
        }

        public FieldInfo GetFieldInfo(object name)
        {
            return _saveDataType.GetField(name.ToString(), _bindingFlags);
        }

        public object GetProp(string name)
        {
            FieldInfo fieldInfo = GetFieldInfo(name);
            return fieldInfo.GetValue(name);
        }

        public void SetProp(string name, object value, bool toSave = true)
        {
            FieldInfo fieldInfo = GetFieldInfo(name);
            if (fieldInfo == null) return;

            fieldInfo.SetValue(_saveData, value);

            if (toSave)
            {
                SaveData();
            }
        }
    }
}
