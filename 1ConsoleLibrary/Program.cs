/// <summary>
/// File: Program.cs
/// By: Connor Reed
/// Email: Contact@calexreed.me
/// Course: CITC 1372
/// </summary>

namespace ConsoleLibrary
{
    /// <summary>
    /// The namespace of the program file
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point for the program to execute
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // instantiate a new LibraryBookInventory object called libaray
            LibraryBookInventory library = new LibraryBookInventory();

            // Some color for terminal
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();

            // Creating some bool vars that are used for loops later
            bool done = false;
            bool isDirty = false; // if true, it will write to JSON file
            bool isFound = false; // if its found, it will be sucessful in checking in or out

            while (!done) // Starts a while loops, keeps going until done is changed
            {

                Console.WriteLine(DisplayMenu()); // function described later.

                var results = Console.ReadLine(); // reads input from user and stores into var

                if (results == "1") // if 1, it will show whole libaray, sectioned by isCheckedOut true/false
                {

                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.DarkRed; // changing color
                    Console.WriteLine("Books checked out"); // shows all books that are checked out

                    foreach (var book in library.books) // foreach loop that goes through libaray object and shows all books that have isCheckedOut as true
                    {
                        // check if isCheckOut is true and writes to console
                        if (book.isCheckedOut == true)
                        {
                            Console.WriteLine(book);
                        } // end if
                    } // end foreach

                    Console.ForegroundColor = ConsoleColor.DarkGreen; // changing color
                    Console.WriteLine("Books checked in"); // shows all books that are checked out

                    foreach (var book in library.books) // foreach loop that goes through libaray object and shows all books that have isCheckedOut as false
                    {
                        // check if isCheckOut is false and writes to console
                        if (book.isCheckedOut == false)
                        {
                            Console.WriteLine(book);
                        } // end if
                    } // end foreach

                    // changing color back to blue 
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Enter to return");
                    Console.ReadLine();
                } // end if


                else if (results == "2") // is the check out function
                {

                    foreach (var book in library.books) // foreach showing all books that have isCheckedOut as false (checked in)
                    {
                        if (book.isCheckedOut == false) // shows books that have isCheckedOut as false (checked in)
                        {
                            Console.WriteLine(book);
                        } // end if
                    } // end foreach

                    // takes user input for ISBM to check out book
                    Console.Write("Enter book ISBM: ");
                    string isbn = Console.ReadLine();

                    foreach (var book in library.books) // foreach to see if user input matches ISBM of list
                    {
                        if (book.getIsbn13().Equals(isbn)) // checks if book isbm is equal to user isbm
                        {
                            book.isCheckedOut = true; // is getting checked out, so changed to true
                            isDirty = true; // changed so changed to dirty
                            isFound = true; // was found, so changed to true
                        } // end if
                    } // end foreach

                    if (isFound) // if saying if it was found, its sucessful, and changes back to false
                    {
                        isFound = false;
                        Console.WriteLine("Successfully Checked Out");
                    } // end if
                    else // if user isbm and book isbm doesnt match, it is not found
                        Console.WriteLine("Could not find book");

                    Console.WriteLine("Enter to return");
                    Console.ReadLine();
                } // end else if


                else if (results == "3") // function to check in a book
                {
                    foreach (var book in library.books) // foreach showing all books that have isCheckedOut as true (checked out)
                    {
                        if (book.isCheckedOut == true) // shows books that have isCheckedOut as true
                        {
                            Console.WriteLine(book);
                        } // end if
                    } // end foreach

                    // gathering input from user (ISBM to be check in)
                    Console.Write("Enter book ISBM: ");
                    string isbn = Console.ReadLine();

                    foreach (var book in library.books) // foreach checking is input matches anything from libaray object
                    {
                        if (book.getIsbn13().Equals(isbn)) // checking if input equals object properties
                        {
                            book.isCheckedOut = false; // changing isCheckedOut to false, saying it was checked in
                            isDirty = true; // was changed so it is dirty
                            isFound = true; // book was found so it should be sucessful
                        } // end if
                    } // end foreach
                    if (isFound) // if found it will be sucessful and changing isFound back to false
                    {
                        isFound = false;
                        Console.WriteLine("Successfully Checked In");
                    } // end if

                    else // could not find book
                        Console.WriteLine("Could not find book");

                    Console.WriteLine("Enter to return");
                    Console.ReadLine();
                } // end else if


                else if (results == "4")
                {
                    if (isDirty) // as it closes, if isDirty is true, it will rewrite JSON file
                    {
                        library.writeJSONBookInventory(); // function in other file
                    } //end if
                    done = true;
                } // end else if
                else // saying invaild input for display function
                {
                    Console.WriteLine("Please select a value 1, 2, 3, or 4");
                    Console.WriteLine("Enter to go back");
                    results = Console.ReadLine();
                } // end else

                Console.Clear();
            }

        }
        /// <summary>
        /// Function to return a string that shows the display menu
        /// </summary>
        /// <returns> menu </returns>
        private static string DisplayMenu()
        {
            string menu = "1. View Books\n2. Checkout Book\n3. Check-in Book\n4. Exit Library";
            return menu;
        }
    }
}
