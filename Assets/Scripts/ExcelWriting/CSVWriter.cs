using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace DefaultNamespace.ExcelWriting
{
    public class CSVWriter
    {
        public static void WriteDataToCsv(List<DataCollection> dataList)
        {
            string folderName = "UserData";
        
            // Combine the project directory with the folder name
            string folderPath = Path.Combine(Application.dataPath, folderName);

            // Create the folder if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Specify the file path within the folder
            string filePath = Path.Combine(folderPath, "DataList.csv");

            // Create or overwrite the CSV file
            using (StreamWriter writer = new(filePath))
            {
                // Write header row
                for (int i = 0; i < dataList.Count; i++)
                {
                    var dataCollection = dataList[i];
                    var rowData = dataCollection.GetData().ToArray();
                    writer.WriteLine(string.Join(",", rowData));
                }
            }

            Debug.Log("CSV file created at: " + filePath);
        }
    }
}
