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
    [Activity(Label = "All Diary Entries")]
    public class ViewEntryActivity : AppCompatActivity
    {
        string username;        
        ListView listview;
        DiaryDatabase database;
        ListAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_view);
            username = Intent.GetStringExtra("UserName");

            database = new DiaryDatabase();

            listview = FindViewById<ListView>(Resource.Id.listEntries);

            adapter = new ListAdapter(this, database.GetUserEntries(username));
            listview.Adapter = adapter;
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
                case Resource.Id.menuHome:
                    Intent intent = new Intent(this, typeof(HomeActivity));
                    intent.PutExtra("UserName", username);
                    StartActivity(intent);
                    Finish();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}