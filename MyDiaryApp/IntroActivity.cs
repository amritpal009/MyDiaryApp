using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiaryApp
{
    [Activity(Label = "IntroActivity", MainLauncher = true)]
    public class IntroActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_intro);
        }
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        public override void OnBackPressed() { }

        async void SimulateStartup()
        {
            await Task.Delay(3000);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}