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

namespace MyDiaryApp
{
    public class ListAdapter : BaseAdapter<Entry>
    {
        private readonly Activity context;
        private readonly List<Entry> entries;

        public ListAdapter(Activity context, List<Entry> entries)
        {
            this.entries = entries;
            this.context = context;
        }

        public override int Count
        {
            get { return entries.Count; }

        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Entry this[int position]
        {
            get { return entries[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.list_row, null, false);
            }

            TextView txtsubject = row.FindViewById<TextView>(Resource.Id.textSubject);
            TextView txtdate = row.FindViewById<TextView>(Resource.Id.textDate);
            TextView txtdetails = row.FindViewById<TextView>(Resource.Id.textDetails);

            Entry entry = entries[position];
            txtsubject.Text = entry.Subject;
            txtdetails.Text = "Details: " + entry.Details;
            TimeSpan time = TimeSpan.FromMilliseconds(entry.EntryDate);
            DateTime date = new DateTime(1970, 1, 1) + time;
            txtdate.Text = "Date: " + date.ToLongDateString();

            return row;
        }
    }
}