using System;

namespace Modules.SaveLoad.Data
{
    [Serializable]
    public class ItemData
    {
        public string StringValue;
        public int IntValue;

        public ItemData(string stringValue, int intValue)
        {
            StringValue = stringValue;
            IntValue = intValue;
        }
    }
}