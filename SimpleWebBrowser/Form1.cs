using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Diagnostics; //we need this to find out performance of the browser.
using System.Security.Cryptography; //Security purposes/
namespace SimpleWebBrowser
{

    public partial class Form1 : Form
    {
        public SpeechSynthesizer synth; //this is my assistant, he speaks when i ask him to. :Dff\d

        public Form1()
        {
            InitializeComponent();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(searchBar.Text);
        }


        private void NavigateToPage()
        {

            toolStripStatusLabel1.Text = "Navigation Has Started.";
          //  navigateButton.Enabled = false;
            searchBar.Enabled = false;
            webBrowser1.Navigate(searchBar.Text);



            if (string.IsNullOrEmpty(searchBar.Text) == true) //is it empty? 
            {
                MessageBox.Show("Field cannot be empty."); // if it is empty, return this message.
            } 
            else if (searchBar.Text.Length != ' ')
            {
                //say Not hashed or HASHED.
                string isHashed = searchBar.Text.GetHashCode().ToString();
                isHashed = "- Hashed";

                webBrowser1.Navigate(searchBar.Text); //search.
                listBoxHistory.Items.Add(searchBar.Text.GetHashCode() + " " + isHashed + " " + DateTime.Now);
                Debug.WriteLine("Recently Added: " + searchBar.Text + " -- " + "Hashed Code: " + searchBar.Text.GetHashCode()); // should print to console - whatever was added by user.
            }

         


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Enter) //by pressing enter we will navigate to the page desired/Entered by the user.
            {
                NavigateToPage(); // this is where it will lead to.
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();

            //navigateButton.Enabled = true;
            searchBar.Enabled = true;
            toolStripStatusLabel1.Text = "Completed";

        }

        //loading bar
        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (e.CurrentProgress > 0 && e.MaximumProgress > 0)
            {
                toolStripProgressBar1.ProgressBar.Value = (int)(e.CurrentProgress * 100 / e.MaximumProgress);

            }

        }

        private void newPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show new page.
            var form1 = new Form1();
            form1.Show();
        }

        private void openNewTabWithKeys(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Tab && e.KeyChar == (char)ConsoleKey.N)
            {
                MessageBox.Show("Tab an N pressed");
            }
           
        }
        private void openListBoxHistory_Click(object sender, EventArgs e, KeyEventArgs k)
        {

            if (searchBar.Text.Length != ' ')
            {
                listBoxHistory.Items.Add(searchBar.Text);

            }

        }

        //private void clearListButton_Click(object sender, EventArgs e)
        //{
          //  if (listBoxHistory.Items.Count > 0) //clear search history.
          //  {
            //    listBoxHistory.Items.Clear(); //clear listed items in History Sect.
             //   MessageBox.Show("Items Cleared");
           // }
            //else if (listBoxHistory.Items.Count <= 0) //if nothing inside history, you cant clear.
            //{
              //  MessageBox.Show("There must be something to clear");
            //}

        //}

        private void addNewBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (addNewBookmarkToolStripMenuItem.Pressed)
            {
                MessageBox.Show(searchBar.Text + " was added to bookmarks!");
                addNewBookmarkToolStripMenuItem.DropDownItems.Add(searchBar.Text);
            }
        }


        private void removeHashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (removeHashToolStripMenuItem.Pressed)
            {
                if (searchBar.Text.Length > 0) {
                    listBoxHistory.Refresh(); // refresh collection
                    listBoxHistory.Items.Clear(); //clear collection
                    listBoxHistory.Items.Add(searchBar.Text + " " + DateTime.Now); //add search item not hashed.

                }
                else
                {
                    MessageBox.Show("there needs to be something to remove.");
                }

            }
            
        }


        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(searchBar.Text) == false) //if it it has something in it
            {



                //testToolStripMenuItem.DropDownItems.Add(searchBar.Text);


            }

        }


        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clear items from list using the drop-down menu button
            if (listBoxHistory.Items.Count > 0)
            {
                listBoxHistory.Items.Clear();
            }
            else
            {
                MessageBox.Show("There needs to be something to Delete");
                MessageBox.Show("Add a link to remove");
            }
        }

        private void clearBookmarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Show this message.
            MessageBox.Show("Bookmarks cleared!");
            //Clear items from bookmarks.
            addNewBookmarkToolStripMenuItem.DropDownItems.Clear();
        }

    }

       
    }



