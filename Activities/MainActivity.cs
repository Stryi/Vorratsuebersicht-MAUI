using Android.Content;


namespace VorratsUebersicht
{
    using static Tools;

    [Activity(Label = "@Strings/App_Name", Icon = "@mipmap/appicon", MainLauncher = true)]
    public class MainActivity : Activity
    {
        // Debug-Konstanten
        private static readonly bool debug_date_picker = false;

        public static readonly int EditStorageItemQuantityId = 1001;
        public static readonly int OptionsId = 1002;
        public static readonly int ArticleListId = 1003;
        public static readonly int ContinueScanMode = 1004;
        public static readonly int EditStorageQuantity = 1005;
        public static readonly int EANScanID = 1006;
        public static readonly int ManageDatabases = 1007;
        public static readonly int ShoppingListId = 1008;

        private static DateTime preLaunchTestEndDay;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            // Damit Pre-Launch von Google Play Store nicht immer wieder
            // in die EAN Scan "Falle" tappt und da nicht wieder rauskommt.
            // (meistens nächster Tag)
            MainActivity.preLaunchTestEndDay = new DateTime(2024, 04, 02);

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // ActionBar Hintergrund Farbe setzen
            var backgroundPaint = this.GetDrawable(Resource.Color.Application_ActionBar_Background);
            backgroundPaint?.SetBounds(0, 0, 10, 10);
            this.ActionBar?.SetBackgroundDrawable(backgroundPaint);
            this.ActionBar?.SetDisplayShowHomeEnabled(true);

            // Create databases on startup
            Android_Database.Instance.RestoreDatabasesFromResourcesOnStartup(this);

            // Auswahl nach Kategorien
            Button? buttonKategorie = FindViewById<Button>(Resource.Id.MainButton_Kategorie);
            buttonKategorie.Click += delegate { this.ShowCategoriesSelection();};

            // Lagerbestand
            Button? buttonLagerbestand = FindViewById<Button>(Resource.Id.MainButton_Lagerbestand);
            //buttonLagerbestand.Click += delegate { StartActivityForResult (new Intent (this, typeof(StorageItemListActivity)), EditStorageItemQuantityId);};

            // Artikeldaten
            Button? buttonArticle = FindViewById<Button>(Resource.Id.MainButton_Artikeldaten);
            //buttonArticle.Click += delegate { StartActivityForResult (new Intent (this, typeof(ArticleListActivity)), ArticleListId);};

            // Einkaufsliste
            Button? buttonShoppingList = FindViewById<Button>(Resource.Id.MainButton_ShoppingList);
            //buttonShoppingList.Click += delegate { StartActivityForResult (new Intent (this, typeof(ShoppingListActivity)), ShoppingListId);};

            // Barcode scannen
            Button? buttonBarcode = FindViewById<Button>(Resource.Id.MainButton_Barcode);
            buttonBarcode.Click += ButtonBarcode_Click;

        }

        private void ShowCategoriesSelection()
        {
            string[] categories = Database.GetCategoriesInUse();

            if (categories.Length == 0)
            {
                Toast.MakeText(this, Resource.String.NoArticleCatagories, ToastLength.Long)?.Show();
                return;
            }

            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle(Resource.String.ArticleCatagoriesSelect);
            builder.SetItems(categories, (sender, args) =>
            {
                string kategorie = categories[args.Which];
                /*
                var subCategory = new Intent(this, typeof(SubCategoryActivity));
                subCategory.PutExtra("Category", kategorie);
                StartActivity(subCategory);
                */
            });
            builder.Show();
        }

        private void ButtonBarcode_Click(object sender, System.EventArgs e)
        {
            /*
            //this.SearchEANCode("4058172637117");  // "Bitterstoff Tee", aber ohne Bild
            //this.SearchEANCode("5900352011950");  // "Oranzada"

            StartActivityForResult(typeof(ZXingFragmentActivity), EANScanID);
            */
        }

    }
}