using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDiaryApp
{
    [Activity(Label = "Home Page")]
    public class HomeActivity : AppCompatActivity
    {
        Button btn;
        RadioButton selectedRadio;
        RadioGroup radioGroup;
        string username;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_home);
            username = Intent.GetStringExtra("UserName");
            radioGroup = FindViewById<RadioGroup>(Resource.Id.radioGroup);
            btn = FindViewById<Button>(Resource.Id.btn);
            btn.Click += Btn_Click;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            int selectedId = radioGroup.CheckedRadioButtonId ;
            if(selectedId == -1)
            {
                Toast.MakeText(this, "Nothing is Selected", ToastLength.Long).Show();
            }
            else
            {
                selectedRadio = FindViewById<RadioButton>(selectedId);
                if(selectedRadio.Text.Contains("Add"))
                {
                    Intent intent = new Intent(this, typeof(AddEntryActivity));
                    intent.PutExtra("UserName", username);
                    StartActivity(intent);
                    Finish();
                }
                else if (selectedRadio.Text.Contains("View"))
                {
                    Intent intent = new Intent(this, typeof(ViewEntryActivity));
                    intent.PutExtra("UserName", username);
                    StartActivity(intent);
                    Finish();
                }
                else if (selectedRadio.Text.Contains("Delete"))
                {
                    Intent intent = new Intent(this, typeof(DeleteEntryActivity));
                    intent.PutExtra("UserName", username);
                    StartActivity(intent);
                    Finish();
                }
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // set the menu layout on Main Activity  
            MenuInflater.Inflate(Resource.Menu.mainmenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuLogOut:
                    StartActivity(new Intent(Application.Context, typeof(MainActivity)));
                    Finish();                    
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}