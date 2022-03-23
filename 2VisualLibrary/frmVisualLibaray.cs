/// <summary>
/// File: frmVisualLibaray.cs
/// By: Connor Reed
/// Email: Contact@calexreed.me
/// Course: CITC 1372
/// </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualLibaray
{
    public partial class frmVisualLibaray : Form
    {
        // creating libaray for all books and a list for both checked in and out books
        LibraryBookInventory library = new LibraryBookInventory();
        List<Book> checkedInBooks = new List<Book>();
        List<Book> checkedOutBooks = new List<Book>();

        /// <summary>
        /// Starts app
        /// </summary>
        public frmVisualLibaray()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Loads the libaray to app through the datagridviews
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmVisualLibaray_Load(object sender, EventArgs e)
        {
            try
            { 
                List<Book> books = library.getAllBooks();
                foreach (Book book in books)
                {
                    if (book.isCheckedOut == false)
                        checkedInBooks.Add(book);
                    else
                        checkedOutBooks.Add(book);
                }

                dgvCheckedInBooks.DataSource = checkedInBooks;
                dgvCheckedOutBooks.DataSource = checkedOutBooks;

                // resizing both column 
                DataGridViewColumn column = dgvCheckedInBooks.Columns[1];
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                DataGridViewColumn column2 = dgvCheckedOutBooks.Columns[1];
                column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Event call for checked in books dgv that switches it to other list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCheckedInBooks_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                return;
            }
             // get selected book
            Book book = checkedInBooks[e.RowIndex];
            
            // changes checkedout to true
            book.isCheckedOut = true;
            
            // add selected book to checkedOutBooks list
            checkedOutBooks.Add(book);
            // remove selected book from checkedInBooks
            checkedInBooks.Remove(book);

            // reseting to null and adding checkedInBooks back to Datasource
            dgvCheckedInBooks.DataSource = null;
            dgvCheckedInBooks.DataSource = checkedInBooks;

            // resizing checkedInBooks column
            DataGridViewColumn column = dgvCheckedInBooks.Columns[1];
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            // reseting to null and adding checkedOutBooks back to Datasource
            dgvCheckedOutBooks.DataSource = null;
            dgvCheckedOutBooks.DataSource = checkedOutBooks;

             // resizing checkedOutBooks column
            DataGridViewColumn column2 = dgvCheckedOutBooks.Columns[1];
            column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        /// <summary>
        ///  Event call for checked out books dgv that switches it to other list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCheckedOutBooks_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            // get selected book
            Book book = checkedOutBooks[e.RowIndex];
            
            // changes checkedout to false
            book.isCheckedOut = false;
            
            // add selected book to checkedInBooks list
            checkedInBooks.Add(book);
            // remove selected book from checkedOutBooks
            checkedOutBooks.Remove(book);

            // using binding source to refresh both
            BindingSource inBooksbind = new BindingSource();
            inBooksbind.DataSource = checkedInBooks;
            dgvCheckedInBooks.DataSource= inBooksbind;

            BindingSource outBooksbind = new BindingSource();
            outBooksbind.DataSource = checkedOutBooks;
            dgvCheckedOutBooks.DataSource = outBooksbind;

        }
     
        /// <summary>
        /// When the form closes, it saves both checkedIn and checkedOut books to json file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmVisualLibaray_FormClosing(object sender, FormClosingEventArgs e)
        {
            LibraryBookInventory newLibrary = new LibraryBookInventory();

            checkedInBooks.AddRange(checkedOutBooks);

            newLibrary.books = checkedInBooks;

            newLibrary.writeJSONBookInventory();
        }

        /// <summary>
        /// closes the app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
