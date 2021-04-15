using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Core;
using Xamarin.Essentials;

namespace Phoneword
{
    [Activity(Label = "TinyTorch", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our UI controls from the loaded layout
            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            Button TorchOn = FindViewById<Button>(Resource.Id.TorchOn);
			TextView translatedPhoneWord = FindViewById<TextView>(Resource.Id.TranslatedPhoneWord);

			// Add code to translate number
			string translatedNumber = string.Empty;

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            TorchOn.Click += async (sender, e) =>
            {
                Button button = sender as Button;
                if (button.Text == "Torch On")
                {
                    button.Text = "Torch Off";
                    await Flashlight.TurnOnAsync();
                }
                else
                {
                    button.Text = "Torch On";
                    await Flashlight.TurnOffAsync();

                }
            };

            translateButton.Click += async (sender, e) =>
            {
                // Translate user’s alphanumeric phone number to numeric
                translatedNumber = PhonewordTranslator.ToNumber(phoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(translatedNumber))
                {
                    translatedPhoneWord.Text = string.Empty;
                }   
                else
                {
                    translatedPhoneWord.Text = translatedNumber;
                    PhonewordTranslator.SendMorseAsync(translatedNumber);
                }
            };
        }

    }
}