namespace UI.Models
{
    public class ItemModel : AbstractModel
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }

        public ItemModel(string stringValue, int intValue)
        {
            StringValue = stringValue;
            IntValue = intValue;
        }
    }
}