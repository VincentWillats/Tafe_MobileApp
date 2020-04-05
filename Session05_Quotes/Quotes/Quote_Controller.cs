using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using PCLStorage;

/**
 *  Controls all the quote functions
 *  Saving, Loading, Retriving Random 
 *    
 */


namespace Quotes
{
    class Quote_Controller
    {
        List<Quote_Class> listOfQuotes = new List<Quote_Class>(); 
        
        string filename = "Quotes.json";
        string folderName = "SavedQuotes";

        
        public async void LoadQuotes()
        {
            string _json; // temp string of _json
            List<Quote_Class> _tempList = new List<Quote_Class>(); // Temp list of quotes 


            IFolder folder = FileSystem.Current.LocalStorage; // Sets current folder
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists); // creates and opens folder "SavedQuotes"

            IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists); // creates if not exists and opens file "Quotes.json"
            _json = await file.ReadAllTextAsync(); // Reads "Quotes.json" and stores the json as text

            _tempList = JsonConvert.DeserializeObject<List<Quote_Class>>(_json); // Converts json text into Quote_Class objects and stores them in a list

            if(_tempList != null) // Checks tempList isn't null, was changing listOfQuotes into null if json file was empty.
            {
                listOfQuotes.AddRange(_tempList); // adds tempList to main list
            }            
        }

        public async void SaveQuotes()
        {
            string _json;

            IFolder folder = FileSystem.Current.LocalStorage; // Sets current folder
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists); // creates and opens folder "SavedQuotes"

            IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists); // Opens the file ready to be written too
            _json = JsonConvert.SerializeObject(listOfQuotes); // Converts out list of quotes into json
            await file.WriteAllTextAsync(_json);            // Writes the json into the file
        }

        public Quote_Class RetrieveRandomQuote()
        {
            Random r = new Random();
            try
            {
                int randInt = r.Next(0, (listOfQuotes.Count));

                return listOfQuotes[randInt];
            }
            catch
            {
                return new Quote_Class();
            }
        }

        public bool AddQuote(string _quote, string _author)
        {
            Quote_Class _temp = new Quote_Class();
            _temp.Quote = _quote;
            _temp.Author = _author;
            if(_temp.Quote != "" && _temp.Author != "")
            {
                listOfQuotes.Add(_temp);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
