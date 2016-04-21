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
	[Activity (Label = "passphrase", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
        public PassphraseController controller;
        private Button executeButton;
        private TextView textOut;
        private EditText textIn;
        private Spinner spinner;

        private int spinnerSelectedItemPosition;

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.Main);
            List<StreamReader> dictionary = DictionaryLoader.fillDictionary(this.Assets);

            //TODO change controller constructor
            //controller = new PassphraseController(DictionaryLoader.fillDictionary(this.Assets));
        }

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.MainMenu, menu);
			return true;
		}

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent i = new Intent();
            switch (item.ItemId)
            {
				case Resource.Id.generateFromEntropy:
					SetContentView (Resource.Layout.GenerateEntropy);

                    executeButton = FindViewById<Button>(Resource.Id.generateEntropyButton);
                    textOut = FindViewById<TextView>(Resource.Id.outputTextView);
                    textIn = FindViewById<EditText>(Resource.Id.entropySizeTextField);

                    executeButton.Click += delegate {
                        int entropy;
                        int.TryParse(textIn.Text, out entropy);

                        textOut.Text = entropy.ToString();
                        //TODO 
                        //sentenceView.Text = string.Format(String.Join(" ", MainActivity.controller.generateSentenceFromEntrophy(entropy)));
                    };

                    return true;
                case Resource.Id.translator:
                    SetContentView (Resource.Layout.PasswordTlanslator);

                    executeButton = FindViewById<Button>(Resource.Id.translateButton);
                    textOut = FindViewById<TextView>(Resource.Id.translatedtextView);
                    textIn = FindViewById<EditText>(Resource.Id.editTextIn);
                    spinner = FindViewById<Spinner>(Resource.Id.translateSpinner);

                    spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
                    var adapter = ArrayAdapter.CreateFromResource(
                        this, Resource.Array.passwordTranslateOptions, Android.Resource.Layout.SimpleSpinnerItem);
                    adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

                    spinner.Adapter = adapter;

                    executeButton.Click += delegate {
                        if (spinnerSelectedItemPosition == 0)
                        {
                            textOut.Text = "Z hesla";
                            //TODO 
                            //textOut.Text = controller.generateSentenceFromBinary(textIn.Text);
                        }
                        else
                        {
                            textOut.Text = "Z vety";
                            //TODO 
                            //textOut.Text = controller.generateBinaryFromSentence(textIn.Text);
                        }
                    };

                    return true;
                case Resource.Id.closeApp:
                    System.Environment.Exit(0);
                    return true;
                default:
                    return false;
            }
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            spinnerSelectedItemPosition = e.Position;
        }
    }
}