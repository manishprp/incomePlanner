using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace incomePlanner
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button calculateButton;
        private EditText incomePerHour;
        private EditText savingRate;
        private EditText taxRate;
        private EditText hourPerDay;

        private TextView workSummary;
        private TextView grossIncome;
        private TextView taxPayable;
        private TextView annualSavings;
        private TextView spendableIncome;
        private RelativeLayout resultLayout;

        bool inputCalculated = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            _uiReference();
            calculateButton.Click += CalculateButton_Click;
            
        }


        private void CalculateButton_Click(object sender, System.EventArgs e)
        {
            if(inputCalculated)
            {
                clearInputs();
                calculateButton.Text = "Calculate";
                inputCalculated = false;
                return;
            }
            double incomePerHourVar = double.Parse(incomePerHour.Text);
            double savingRateVar = double.Parse(savingRate.Text);
            double taxRateVar = double.Parse(taxRate.Text);
            double hourPerDayVar = double.Parse(hourPerDay.Text);

            double workSummaryResult = hourPerDayVar * 5 * 50; // Let's say user takes 2 weeks off
            double grossIncomeResult = incomePerHourVar * hourPerDayVar * 5 * 50;
            double taxpayableResult = (taxRateVar / 100) * grossIncomeResult;
            double savingRateResult = (savingRateVar/100) * grossIncomeResult;
            double spendableIncomeResult = grossIncomeResult - taxpayableResult - savingRateResult;

            workSummary.Text = workSummaryResult.ToString("#,###,##") + " Hour";
            grossIncome.Text = "Rs. " + grossIncomeResult.ToString("#,###,##");
            taxPayable.Text = "Rs. " + taxpayableResult.ToString("#,###,##");
            annualSavings.Text = "Rs. " + savingRateResult.ToString("#,###,##");
            spendableIncome.Text = "Rs. " + spendableIncomeResult.ToString("#,###,##");

            
            calculateButton.Text = "Clear";
            resultLayout.Visibility = Android.Views.ViewStates.Visible;
            inputCalculated = true;
        }

        public void _uiReference()
        {
            calculateButton = FindViewById<Button>(Resource.Id.calculateButton);
            incomePerHour = FindViewById<EditText>(Resource.Id.incomePerHourEditText);
            hourPerDay = FindViewById<EditText>(Resource.Id.hoursPerDayEditText);
            savingRate = FindViewById<EditText>(Resource.Id.savingRateEditText);
            taxRate = FindViewById<EditText>(Resource.Id.taxRateEditText);

            workSummary = (TextView)FindViewById(Resource.Id.workSummaryResult);
            grossIncome = (TextView)FindViewById(Resource.Id.grossIncomeResult);
            taxPayable = (TextView)FindViewById(Resource.Id.taxPayableResult);
            annualSavings = (TextView)FindViewById(Resource.Id.annualSavingsResult);
            spendableIncome = (TextView)FindViewById(Resource.Id.spendableIncomeResult);

            resultLayout = (RelativeLayout)FindViewById(Resource.Id.resultLayout);

        }
        
        public void clearInputs()
        {
            incomePerHour.Text = "";
            savingRate.Text = ""; ;
            taxRate.Text = ""; ;
            hourPerDay.Text = ""; ;

            workSummary.Text = ""; 
            grossIncome.Text = "";
            taxPayable.Text = "";
            annualSavings.Text = ""; 
            spendableIncome.Text = "";

            resultLayout.Visibility = Android.Views.ViewStates.Invisible;
        }

    }
}