using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using View = Android.Views.View;

namespace VorratsUebersicht
{
    public class ArticleListViewAdapter : BaseAdapter<ArticleListView>
    {
        List<ArticleListView> items;
        Activity context;

        public event EventHandler OptionMenu;

        public ArticleListViewAdapter(Activity context, List<ArticleListView> items) : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override ArticleListView this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ArticleListView item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.ArticleListView, null);
            
            view.FindViewById<TextView> (Resource.Id.ArticleListView_Heading).Text    = item.Heading;
            view.FindViewById<TextView> (Resource.Id.ArticleListView_SubHeading).Text = item.SubHeading;
            view.FindViewById<TextView> (Resource.Id.ArticleListView_Notes).Text      = item.Notes;
            view.FindViewById<TextView> (Resource.Id.ArticleListView_Notes).Visibility            = string.IsNullOrEmpty(item.Notes) ? ViewStates.Gone : ViewStates.Visible;
            view.FindViewById<ImageView>(Resource.Id.ArticleListView_OnShoppingList).Visibility   = item.IsOnShoppingList ? ViewStates.Visible : ViewStates.Gone;
            view.FindViewById<TextView> (Resource.Id.ArticleListView_ShoppingQuantity).Visibility = item.IsOnShoppingList ? ViewStates.Visible : ViewStates.Gone;
            view.FindViewById<TextView> (Resource.Id.ArticleListView_ShoppingQuantity).Text       = item.ShoppingQuantity;
            view.FindViewById<ImageView>(Resource.Id.ArticleListView_IsInStorage).Visibility      = item.IsInStorage ? ViewStates.Visible : ViewStates.Invisible;
            view.FindViewById<TextView> (Resource.Id.ArticleListView_StorageQuantity).Visibility  = item.IsInStorage ? ViewStates.Visible : ViewStates.Invisible;
            view.FindViewById<TextView> (Resource.Id.ArticleListView_StorageQuantity).Text        = item.StorageQuantity;

            if ((!item.IsOnShoppingList) && (!item.IsInStorage))
            {
                view.FindViewById<ImageView>(Resource.Id.ArticleListView_IsInStorage).Visibility      = ViewStates.Gone;
                view.FindViewById<TextView> (Resource.Id.ArticleListView_StorageQuantity).Visibility  = ViewStates.Gone;
            }

            TextView option = view.FindViewById<TextView>(Resource.Id.ArticleListView_Option);
            option.Click -= Option_Click;
            option.Click += Option_Click;

            ImageView image = view.FindViewById<ImageView>(Resource.Id.ArticleListView_Image);
            image.Click -= OnImageClicked;

            if (item.Image == null)
            {
                image.SetImageResource(Resource.Mipmap.ic_photo_camera_black_24dp);
                image.Alpha = 0.5f;
            }
            else
            {
                image.SetImageBitmap(item.Image);
                image.Alpha = 1f;

                image.Tag = item.ArticleId;
                image.Click += OnImageClicked;
            }

            return view;
        }

        private void Option_Click(object sender, EventArgs e)
        {
            this.OptionMenu?.Invoke(sender, EventArgs.Empty);
        }

        private void OnImageClicked(object sender, EventArgs e)
        {
            ImageView imageToView = (ImageView)sender;

            int articleId = (int)imageToView.Tag;

            var articleImage = new Intent(context, typeof(ArticleImageActivity));
            articleImage.PutExtra("ArticleId", articleId);
            context.StartActivity(articleImage);
        }
    }
}