using System.Collections.Generic;
namespace DefaultNamespace.ExcelWriting
{
    public class DataCollection
    {
        private List<string> data;
        
        public void AddData(string newData)
        {
            data.Add(newData);
        }
        
        public List<string> GetData()
        {
            return data;
        }

        public DataCollection()
        {
            data = new List<string>();
        }
    }
}
