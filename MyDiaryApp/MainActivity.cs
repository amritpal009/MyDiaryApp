using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace MyDiaryApp
{
    [Activity(Label = "Login", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        Button btnlogin;
        EditText etuser, etpassword;
        TextView tv;
        DiaryDatabase database;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            database = new DiaryDatabase();
            etuser = FindViewById<EditText>(Resource.Id.etUserName);
            etpassword = FindViewById<EditText>(Resource.Id.etPassword);
            tv = FindViewById<TextView>(Resource.Id.lnkRegister);

            btnlogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnlogin.Click += Btnlogin_Click;
            tv.Click += Tv_Click;
        }

        private void Btnlogin_Click(object sender, System.EventArgs e)
        {
            string username = etuser.Text.Trim();
            string password = etpassword.Text;
            if (username.Length == 0 || password.Length == 0)
            {
                Toast.MakeText(this, "Please Fill All Boxes", ToastLength.Long).Show();
            }
            else
            {
                if (database.ValidUser(username, password))
                {
                    Intent intent = new Intent(this, typeof(HomeActivity));
                    intent.PutExtra("UserName", username);
                    StartActivity(intent);
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Invalid User Name and Password", ToastLength.Long).Show();
                }
            }
        }

        private void Tv_Click(object sender, System.EventArgs e)
        {
            StartActivity(new Intent(Application.Context, typeof(RegisterActivity)));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}