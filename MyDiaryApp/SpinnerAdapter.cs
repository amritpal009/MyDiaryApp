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
    public class SpinnerAdapter : BaseAdapter<Entry>
    {
        private readonly Activity context;
        private readonly List<Entry> entries;

        public SpinnerAdapter(Activity context, List<Entry> entries)
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
                row = LayoutInflater.From(context).Inflate(Resource.Layout.spinner_row, null, false);
            }

            TextView txt= row.FindViewById<TextView>(Resource.Id.textSubject);

            txt.Text = entries[position].Subject;

            return row;
        }
    }
}