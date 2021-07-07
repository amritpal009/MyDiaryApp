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
    [Activity(Label = "Register Page")]
    public class RegisterActivity : AppCompatActivity
    {
        Button btnregister;
        EditText etuser, etpassword, etconfirm;
        TextView tv;
        DiaryDatabase database;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);

            database = new DiaryDatabase();
            etuser = FindViewById<EditText>(Resource.Id.etUserName);
            etpassword = FindViewById<EditText>(Resource.Id.etPassword);
            etconfirm = FindViewById<EditText>(Resource.Id.etConfirm);
            tv = FindViewById<TextView>(Resource.Id.lnkLogin);

            btnregister = FindViewById<Button>(Resource.Id.btnRegister);
            btnregister.Click += Btnregister_Click;
            tv.Click += Tv_Click;
        }

        private void Tv_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        private void Btnregister_Click(object sender, EventArgs e)
        {
            string username = etuser.Text.Trim();
            string pass = etpassword.Text;
            string cpass = etconfirm.Text;
            if (username.Length == 0 || pass.Length == 0 || cpass.Length == 0)
            {
                Toast.MakeText(this, "Please Fill All Boxes", ToastLength.Long).Show();
            }
            else if (pass.Equals(cpass))
            {
                User user = new User();
                user.UserName = username;
                user.Password = pass;
                if (database.SaveUser(user))
                {
                    Intent intent = new Intent(this, typeof(HomeActivity));
                    intent.PutExtra("UserName", username);
                    StartActivity(intent);
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Failure Due to " + database.ErrorMessage, ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "Confirm Password must be match with Password", ToastLength.Long).Show();
            }
        }
    }
}