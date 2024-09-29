using System;

using Android.App;
using Android.OS;
using Android.Webkit;
using Android.Views;

using WebView = Android.Webkit.WebView;
using Button = Android.Widget.Button;
using Color = Android.Graphics.Color;
using ProgressBar = Android.Widget.ProgressBar;
using Switch = Android.Widget.Switch;
using View = Android.Views.View;

namespace VorratsUebersicht
{
    [Activity(Label = "Open-Source-Lizenzen")]
    public class LicensesActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.Licenses);

            // ActionBar Hintergrund Farbe setzen
            var backgroundPaint = this.GetDrawable(Resource.Color.Application_ActionBar_Background);
            backgroundPaint.SetBounds(0, 0, 10, 10);
            ActionBar.SetBackgroundDrawable(backgroundPaint);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            var textView = FindViewById<WebView>(Resource.Id.Licenses_Text);
            textView.LoadUrl("file:///android_asset/Licenses.html");
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    this.Finish();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}