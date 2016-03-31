using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace passphrase
{
	[Activity (Label = "passphrase", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.Main);
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