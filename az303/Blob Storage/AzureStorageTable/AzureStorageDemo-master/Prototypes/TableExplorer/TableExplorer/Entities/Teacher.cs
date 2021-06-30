using Microsoft.WindowsAzure.Storage.Table;

namespace TableExplorer.Entities
{
    public class Teacher : TableEntity
    {
        public Teacher(string identifier, string category)
        {
            PartitionKey = identifier;
            RowKey = category;
        }

        public Teacher()
        {

        }

        public string Name { get; set; }
        public string Assignment { get; set; }
    }
}
