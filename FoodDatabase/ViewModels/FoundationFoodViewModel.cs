using FoodDatabase.Data;
using FoodDatabase.Mvvm;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using Z.EntityFramework.Plus;

namespace FoodDatabase.ViewModels
{
    public class FoundationFoodViewModel : BaseDataViewModel<FoodContext>
    {      
        public int FoodId { get; set; }

        public ICommand BackCommand { get; private set; }

        private FoundationFood food = null;

        public FoundationFood Food
        {
            get => food;
            set
            {
                food = value;                
                RaisePropertyChanged(nameof(Food));
            }
        }

        public FoundationFoodViewModel(IDbContextFactory<FoodContext> factory) : base(factory)
        {
        }

        public FoodNutrient NutrientByName(string name, bool contains = false) =>
            contains
            ? Food?.FoodNutrients.FirstOrDefault()
            : Food?.FoodNutrients.FirstOrDefault();

        public FoodNutrient Calories => NutrientByName("Energy", true);
        public FoodNutrient Fat => NutrientByName("Total fat", true);
        public FoodNutrient SaturatedFat => NutrientByName("Fatty acids, total saturated");
        public FoodNutrient TransFat => NutrientByName("Fatty acids, total trans");
        public FoodNutrient Cholesterol => NutrientByName("Cholesterol",  true);
        public FoodNutrient Sodium => NutrientByName("Sodium", true);
        public FoodNutrient Carbohydrate => NutrientByName("Carbohydrate", true);
        public FoodNutrient Fiber => NutrientByName("Fiber, total", true);
        public FoodNutrient Sugars => NutrientByName("Sugars, Total", true);
        public FoodNutrient Protein => NutrientByName("Protein", true);
        public FoodNutrient VitaminD => NutrientByName("Vitamin D", true);
        public FoodNutrient Iron => NutrientByName("Iron", true);
        public FoodNutrient VitaminA => NutrientByName("Vitamin A", true);
        public FoodNutrient Riboflavin => NutrientByName("Riboflavin", true);
        public FoodNutrient VitaminB6 => NutrientByName("Vitamin B6", true);
        public FoodNutrient Calcium => NutrientByName("Calcium", true);
        public FoodNutrient Potassium => NutrientByName("Potassium", true);
        public FoodNutrient Thiamin => NutrientByName("Thiamin", true);
        public FoodNutrient Niacin => NutrientByName("Niacin", true);
        public FoodNutrient Zinc => NutrientByName("Zinc", true);

        public override void Init()
        {
            if (Food == null || Food.Id != FoodId)
            {
                using var ctx = factory.CreateDbContext();
                Food = ctx.FoundationFoods
                    .IncludeOptimized(ff => ff.FoodNutrients)
                    .IncludeOptimized(ff => ff.FoodNutrients.Select(fn => fn.Nutrient))
                    .IncludeOptimized(ff => ff.FoodPortions)
                    .IncludeOptimized(ff => ff.FoodPortions.Select(fp => fp.MeasureUnit))
                    .FirstOrDefault(ff => ff.Id == FoodId);
            }
            RaisePropertyChanged(string.Empty);
        }

        public async Task ShowFoodAsync(int foodId, string returnRoute)
        {
            FoodId = foodId;
            BackCommand = new NavigationCommand(returnRoute);
            await Shell.Current.GoToAsync("//Food");
            Init();
        }
    }
}
