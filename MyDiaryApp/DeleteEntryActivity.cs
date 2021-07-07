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
    [Activity(Label = "Delete Diary Entry")]
    public class DeleteEntryActivity : AppCompatActivity
    {
        Button btn;
        Spinner spinner;
        string username;
        DiaryDatabase database;
        SpinnerAdapter adapter;
        List<Entry> entries;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_delete);
            username = Intent.GetStringExtra("UserName");

            database = new DiaryDatabase();

            spinner = FindViewById<Spinner>(Resource.Id.spinnerSubject);
            btn = FindViewById<Button>(Resource.Id.btn);

            entries = database.GetUserEntries(username);
            adapter = new SpinnerAdapter(this, entries);
            spinner.Adapter = adapter;

            btn.Click += Btn_Click;
            
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            string message = "";
            if (entries != null && entries.Count() > 0)
            {
                Entry entry = entries[spinner.SelectedItemPosition];
                if (database.DeleteEntry(entry))
                {
                    message = "Diary Entry is Removed";
                    entries.RemoveAt(spinner.SelectedItemPosition);
                    adapter.NotifyDataSetChanged();
                }
                else
                {
                    message = "Diary Entry is not Removed";
                }
            }
            else
            {
                message = "There is No Diary Entry Available For Delete.";
            }
            Toast.MakeText(this, message, ToastLength.Long).Show();
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