/// <summary>
/// File: FileOperations.cs
/// By: Connor Reed
/// Email: Contact@calexreed.me
/// Course: CITC 1372
/// </summary>

using System;
using System.IO;
using System.Text;

namespace VisualLibaray
{
    internal class FileOperations
    {
        /// <summary>
        /// This file handles is a class that handles the file operations
        /// the fileLocation is a private string that is read from the JSON file
        /// </summary>
        private static string fileLocation = "BookInventory.json";
        /// <summary>
        /// This will read the JSON file from file location and then close the reader 
        /// throws an exception if not found
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string readBookInventoryJsonFile()
        {
            string inventory;

            try
            {
                StreamReader reader = new StreamReader(fileLocation);
                inventory = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return inventory;
        }

        /// <summary>
        /// This writes the changes to the same JSON file and overwrites it
        /// throws exception if not found
        /// </summary>
        /// <param name="Inventory"></param>
        public void writeNewInventoryFile(string Inventory)
        {
            try
            {
                StreamWriter writer = new StreamWriter(fileLocation, false, Encoding.ASCII);
                writer.WriteLine(Inventory);
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

        }
    }

}
