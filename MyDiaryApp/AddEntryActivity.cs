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
    [Activity(Label = "New Diary Entry")]
    public class AddEntryActivity : AppCompatActivity
    {
        string username;
        Button btn;
        EditText etsubject,etdetails;
        DatePicker dateentry;
        DiaryDatabase database;
        int year, month, day;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add);
            username = Intent.GetStringExtra("UserName");
            database = new DiaryDatabase();

            etsubject = FindViewById<EditText>(Resource.Id.etSubject);
            etdetails = FindViewById<EditText>(Resource.Id.etDetails);
            dateentry = FindViewById<DatePicker>(Resource.Id.dateEntry);

            btn = FindViewById<Button>(Resource.Id.btn);

            btn.Click += Btn_Click;
            dateentry.DateChanged += Dateentry_DateChanged;
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // set the menu layout on Main Activity  
            MenuInflater.Inflate(Resource.Menu.mainmenu, menu);
            return base.OnCreateOptionsMenu(menu);            
        }

        private void Dateentry_DateChanged(object sender, DatePicker.DateChangedEventArgs e)
        {
            year = e.Year;
            month = e.MonthOfYear;
            day = e.DayOfMonth;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            string subject = etsubject.Text.Trim();
            string details = etdetails.Text.Trim();
            if (year == 0 || subject.Length == 0 || details.Length == 0)
            {
                Toast.MakeText(this, "Please Fill All Boxes", ToastLength.Long).Show();
            }
            else
            {
                DateTime entrydate = new DateTime(year, month, day);
                DateTime startdate = new DateTime(1970, 1, 1);
                Entry entry = new Entry();
                entry.UserName = username;
                entry.Subject = subject;
                entry.Details = details;
                entry.EntryDate = (long)(entrydate - startdate).TotalMilliseconds;
                if (database.SaveEntry(entry))
                {
                    Toast.MakeText(this, "Diary Entry is Saved", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Diary Entry is not Saved", ToastLength.Long).Show();
                }

            }
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