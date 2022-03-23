﻿/// <summary>
/// File: Book.cs
/// By: Connor Reed
/// Email: Contact@calexreed.me
/// Course: CITC 1372
/// </summary>


namespace ConsoleLibrary
{
    internal class Book
    {
        /// <summary>
        /// Class called Book that has a public string for the ISBN
        /// </summary>
        public string isbn13;
        /// <summary>
        /// Will return the isbn from a book
        /// </summary>
        /// <returns>string that is from the book from the libaray</returns>
        public string getIsbn13()
        {
            return isbn13;
        }
        /// <summary>
        /// Sets a isbn from the param
        /// </summary>
        /// <param name="isbn13"></param>
        public void setIsbn13(string isbn13)
        {
            this.isbn13 = isbn13;
        }

        //public string isbn13 { get; set; }
        public string title { get; set; }
        public bool isCheckedOut { get; set; }        

        public Book(string isbn13, string title)
        {
            this.isbn13 = isbn13;
            this.title = title;
            this.isCheckedOut = false;
        }

        //Use Interpolated Strings
        //override base class toString
        public override string ToString()
        {
            return $"ISBN13: {isbn13} Title: {title} Checked Out: {isCheckedOut}";            
        }
    }
}
