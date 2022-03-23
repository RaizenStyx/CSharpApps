/// <summary>
/// File: LibarayBookInventory.cs
/// By: Connor Reed
/// Email: Contact@calexreed.me
/// Course: CITC 1372
/// </summary>


using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VisualLibaray
{
    internal class LibraryBookInventory
    {
        /// <summary>
        /// Is a class that is used in the main program
        /// Creates a list of books from reading from a JSON file
        /// </summary>
        public List<Book> books;

        public LibraryBookInventory()
        {
            try
            {
                books = JsonConvert.DeserializeObject<List<Book>>(getJSONBookInventory());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Recieves a json file and reads and returns it.
        /// </summary>
        /// <returns> String taken from a json file</returns>
        private string getJSONBookInventory()
        {
            FileOperations file = new FileOperations();
            string jsonInventoryString = file.readBookInventoryJsonFile();
            return jsonInventoryString;
        }

        /// <summary>
        /// Writes to the json file and overwrites the information that is flagged as dirty
        /// </summary>
        public void writeJSONBookInventory()
        {
            FileOperations file = new FileOperations();
            string json = JsonConvert.SerializeObject(books);
            file.writeNewInventoryFile(json);
        }
        /// <summary>
        /// simple function that just returns the list of books
        /// </summary>
        /// <returns> books </returns>
        public List<Book> getAllBooks()
        {
            return books;
        }
    }
}