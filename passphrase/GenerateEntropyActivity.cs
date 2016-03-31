
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using PassphraseGen;

namespace passphrase
{
	[Activity (Label = "GenerateEntropyActivity")]			
	public class GenerateEntropyActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.GenerateEntropy);

			Button generateButton = FindViewById<Button> (Resource.Id.generateEntropyButton);
			TextView sentenceView = FindViewById<TextView> (Resource.Id.outputTextView);
			EditText entropySize = FindViewById<EditText> (Resource.Id.entropySizeTextField);

			generateButton.Click += delegate {
				int entropy;
				int.TryParse(entropySize.Text, out entropy);

                File sdcard = Android.OS.Environment.DataDirectory;

                PassphraseController passphrasegen = new PassphraseController("");
                //TODO Call DLL

                sentenceView.Text = string.Format (entropy.ToString());
			};
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.MainMenu, menu);
			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
			case Resource.Id.generateFromEntropy: 
				StartActivity (typeof(GenerateEntropyActivity));
				return true;
			case Resource.Id.translator:
				StartActivity(typeof(TranslateActivity));
				return true;
			case Resource.Id.closeApp:
				System.Environment.Exit(0);
				return true;
			default:
				return false;
			}
		}
	}
}