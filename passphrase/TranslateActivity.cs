
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

namespace passphrase
{
	[Activity (Label = "TranslateActivity")]			
	public class TranslateActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.PasswordTlanslator);

			Spinner spinner = FindViewById<Spinner> (Resource.Id.translateSpinner);

			spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected);
			var adapter = ArrayAdapter.CreateFromResource (
				this, Resource.Array.passwordTranslateOptions, Android.Resource.Layout.SimpleSpinnerItem);
			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spinner.Adapter = adapter;

			Button generateButton = FindViewById<Button> (Resource.Id.translateButton);
			TextView sentenceView = FindViewById<TextView> (Resource.Id.translatedtextView);
			EditText entropySize = FindViewById<EditText> (Resource.Id.editTextIn);

			generateButton.Click += delegate {


				//TODO Call DLL

				//sentenceView.Text = string.Format ();
			};
		}

		private void spinner_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;


			//string.Format ( (spinner.GetItemAtPosition (e.Position)).ToString());
//			Toast.MakeText (this, toast, ToastLength.Long).Show ();
		}
	}
}

