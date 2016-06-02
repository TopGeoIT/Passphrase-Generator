using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using PassphraseGen;
using Android.Content;
using System.IO;
using System.Collections.Generic;
using System;

namespace passphrase
{
    [Activity(Label = "Passphrase", MainLauncher = true, Icon = "@drawable/PassphraseClean")]
    public class MainActivity : Activity
    {
        public PassphraseController controller;
        private Button executeButton;
        private TextView textOut;
        private EditText textIn;
        private Spinner spinner;
        //private  dictionary;
        private int spinnerSelectedItemPosition;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            try
            {
                DictionaryLoader dl = new DictionaryLoader();
                Dictionary<string, string> dictionary = dl.fillDictionary(this.Assets);

                if (dictionary != null)
                {
                    controller = new PassphraseController(dictionary);
                    dictionary = null;
                    return;
                }
            }
            catch
            {
                TextView infoTextView = FindViewById<TextView>(Resource.Id.infoTextView);
                infoTextView.Text = GetString(Resource.String.loadingError);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.MainMenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (controller != null)
            {
                Intent i = new Intent();
                switch (item.ItemId)
                {
                    case Resource.Id.generateFromEntropy:
                        SetContentView(Resource.Layout.GenerateEntropy);

                        executeButton = FindViewById<Button>(Resource.Id.generateEntropyButton);
                        textOut = FindViewById<TextView>(Resource.Id.outputTextView);
                        textIn = FindViewById<EditText>(Resource.Id.entropySizeTextField);

                        executeButton.Click += delegate
                        {
                            int entropy;
                            int.TryParse(textIn.Text, out entropy);

                            try
                            {
                                textOut.Text = string.Format(String.Join(" ", controller.generateSentenceFromEntrophy(entropy)));
                            }
                            catch
                            {
                                textOut.Text = GetString(Resource.String.generatingError);
                            }
                        };

                        return true;
                    case Resource.Id.translator:
                        SetContentView(Resource.Layout.PasswordTlanslator);

                        executeButton = FindViewById<Button>(Resource.Id.translateButton);
                        textOut = FindViewById<TextView>(Resource.Id.translatedtextView);
                        textIn = FindViewById<EditText>(Resource.Id.editTextIn);
                        spinner = FindViewById<Spinner>(Resource.Id.translateSpinner);

                        spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
                        var adapter = ArrayAdapter.CreateFromResource(
                            this, Resource.Array.passwordTranslateOptions, Android.Resource.Layout.SimpleSpinnerItem);
                        adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

                        spinner.Adapter = adapter;

                        executeButton.Click += delegate
                        {
                            try
                            {
                                if (spinnerSelectedItemPosition == 0)
                                {
                                    // from password
                                    textOut.Text = string.Format(String.Join(" ", controller.generateSentenceFromBinary(textIn.Text)));

                                }
                                else
                                {
                                    // from sntence
                                    textOut.Text = controller.generateBinaryFromSentence(textIn.Text);
                                }
                            }
                            catch
                            {
                                textOut.Text = GetString(Resource.String.translaeError);
                            }
                        };

                        return true;
                    case Resource.Id.closeApp:
                        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                        return true;
                    default:
                        return false;
                }
            }

            return false;
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            spinnerSelectedItemPosition = e.Position;
        }
    }
}