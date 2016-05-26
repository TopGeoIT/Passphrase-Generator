using PassphraseGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App27
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage1 : Page
    {
        public MainPage1()
        {
            this.InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {


            string fileContent;
            Dictionary<string, string> Dictionaries = new Dictionary<string, string>();

            StorageFile fileNouns = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Resources/dictionaries/Nouns.xml"));
            using (StreamReader sRead = new StreamReader(await fileNouns.OpenStreamForReadAsync()))
                fileContent = await sRead.ReadToEndAsync();

            Dictionaries.Add("nouns", fileContent);

            StorageFile fileAdj = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Resources/dictionaries/Adjectives.xml"));
            using (StreamReader sRead = new StreamReader(await fileAdj.OpenStreamForReadAsync()))
                fileContent = await sRead.ReadToEndAsync();

            Dictionaries.Add("adjectives", fileContent);

            StorageFile fileAdv = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Resources/dictionaries/Adverbs.xml"));
            using (StreamReader sRead = new StreamReader(await fileAdv.OpenStreamForReadAsync()))
                fileContent = await sRead.ReadToEndAsync();

            Dictionaries.Add("adverbs", fileContent);

            StorageFile fileCon = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Resources/dictionaries/Conjunctions.xml"));
            using (StreamReader sRead = new StreamReader(await fileCon.OpenStreamForReadAsync()))
                fileContent = await sRead.ReadToEndAsync();

            Dictionaries.Add("conjuctions", fileContent);

            StorageFile fileVerb = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Resources/dictionaries/Verbs.xml"));
            using (StreamReader sRead = new StreamReader(await fileVerb.OpenStreamForReadAsync()))
                fileContent = await sRead.ReadToEndAsync();

            Dictionaries.Add("verbs", fileContent);

            PassphraseController passphrasegen = new PassphraseController(Dictionaries);


            try {
                int enth = int.Parse(enthropyTextBox.Text);
                string[] passphrase = passphrasegen.generateSentenceFromEntrophy(enth);

                string separator = " ";
                textBox.Text = string.Join(separator, passphrase);
            }
            catch
            {
               
            }
        }

        private void RichTextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
