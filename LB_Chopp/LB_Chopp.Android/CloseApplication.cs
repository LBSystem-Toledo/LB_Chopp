using Android.App;
using LB_Chopp.Interface;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(LB_Chopp.Droid.CloseApplication))]
namespace LB_Chopp.Droid
{
    public class CloseApplication : ICloseApplication
    {
        [Obsolete]
        public void closeApplication()
        {
            Activity activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}