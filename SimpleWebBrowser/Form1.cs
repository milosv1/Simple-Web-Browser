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
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace SimpleWebBrowser
{

    public partial class Form1 : Form
    {

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
                webBrowser1.Navigate(searchBar.Text); //search.
                Debug.WriteLine("searchBar.Text"); // should print to console - whatever was added by user.
                historyToolStripMenuItem.DropDownItems.Add(searchBar.Text);
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

        private void openListBoxHistory_Click(object sender, EventArgs e, KeyEventArgs k)
        {

            if (searchBar.Text.Length != ' ')
            {
                listBoxHistory.Items.Add(searchBar.Text);

            }

        }

        private void addNewBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (addNewBookmarkToolStripMenuItem.Pressed)
            {
                if (searchBar.Text == "https://" || searchBar.Text == "http://")
                {
                    Debug.WriteLine("Needs to be a valid URL");
                }
                else
                {
                    addNewBookmarkToolStripMenuItem.DropDownItems.Add(searchBar.Text);
                }
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clear items from list using the drop-down menu button
            if (listBoxHistory.Items.Count > 0)
            {
                listBoxHistory.Items.Clear();
                Debug.WriteLine(searchBar.Text + " has been deleted.");
            }
            else
            {
                MessageBox.Show("No items in list.");
            }
        }

        private void clearBookmarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Show this message.
            MessageBox.Show("Bookmarks cleared!");
            //Clear items from bookmarks.
            addNewBookmarkToolStripMenuItem.DropDownItems.Clear();
        }

        private async void searchSymbolToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            //Try and create a small stock tracking component into the web browser.
            //search symbol using searchBar.Text ;)
            var symbol = searchBar.Text;
            var apiKey = "replace with your API key."; // replace with your Alpha Vantage API key

            var url = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={searchBar.Text}&apikey={apiKey}";

            using (var httpClient = new HttpClient())
            using (var response = await httpClient.GetAsync(url))
            using (var content = response.Content)
            {
                var json = await content.ReadAsStringAsync();
                //listBoxHistory.Items.Add(json);
                MessageBox.Show(json);
            }
        }
    }

       
    }



