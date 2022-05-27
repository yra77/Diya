using Android.App;
using Android.OS;
using Android.Views;
using System.Threading.Tasks;
using Android.Widget;
using Android.Views.Animations;
using Android.Content.PM;


namespace MyApp.Droid
{
    [Activity(ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait, 
              Label = "Дія", Icon = "@drawable/diya1", MainLauncher = true, Theme = "@style/MyTheme.Splash", NoHistory = true)]
    public class SplashActivity : Activity
    {

        ImageView imageView; 
        ImageView imageView1;
        ImageView imageView2;
        Animation view_animation;
        Animation view_animation1;
        Animation view_animation2;


        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Animation.loadingpageanimation);
     
            imageView = (ImageView)FindViewById(Resource.Id.imageview);
            imageView1 = (ImageView)FindViewById(Resource.Id.imageview1);
            imageView2 = (ImageView)FindViewById(Resource.Id.imageview2);

            imageView2.Visibility = ViewStates.Invisible;//disabled logotip3
            
            view_animation = AnimationUtils.LoadAnimation(this, Resource.Animation.hyperspace);
            view_animation1 = AnimationUtils.LoadAnimation(this, Resource.Animation.logo1_2);
            view_animation2 = AnimationUtils.LoadAnimation(this, Resource.Animation.logo3);
          
            imageView.StartAnimation(view_animation);
            imageView1.StartAnimation(view_animation1);

            imageView.Visibility = ViewStates.Invisible;//disabled logotip1
            //imageView1.Visibility = ViewStates.Invisible;//disabled logotip1_2

              imageView1.SetX(80);
            await Task.Delay(1200);
            imageView2.Visibility = ViewStates.Visible;//enabled logotip3
            imageView2.StartAnimation(view_animation2);
            
            imageView2.SetX(320);
            //view_animation.AnimationEnd += Rotate_AnimationEnd;
            //view_animation1.AnimationEnd += Rotate_AnimationEnd1;
            view_animation2.AnimationEnd += AnimationEnd;
        }

        private void AnimationEnd(object sender, Animation.AnimationEndEventArgs e)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(o => SimulateStartup());            
        }
      
        public override void OnBackPressed() { }// Prevent the back button from canceling the startup process

        async void SimulateStartup()
        {
            await Task.Delay(200);
          //  Finish();
            RunOnUiThread(() => StartActivity(typeof(MainActivity))); //StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

    }
}