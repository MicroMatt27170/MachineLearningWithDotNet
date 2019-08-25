using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MachineLearningWithDotNetML.Model.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;

namespace WEBAPP.Controllers
{
    public class HomeController : Controller
    {
        private const string ModelFilepath = @"MLModel.zip";
        private const string DataFilepath = @"D:\projects\python\mc_test\input_data_pred.csv";

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Models()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Location,Product,Date,Temp_mean,Temp_max,Temp_min,Sunshine_quant,Event,Price")] ModelInput model)
        {
            var mlContext = new MLContext();
            var mlModel = mlContext.Model.Load(GetAbsolutePath(ModelFilepath), out DataViewSchema inputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
            model.Sa_quantity = 0;
            var predictionResult = predEngine.Predict(model);
            var score = (float)Math.Round(predictionResult.Score);

            ViewData["prediction"] = score < 0 ? 0 : score;

            return View("~/Views/Home/Models.cshtml");
        }

        [HttpPost]
        public string GetAbsolutePath(string relativePath)
        {
            var _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            var assemblyFolderPath = _dataRoot.Directory.FullName;

            var fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

        [HttpPost]
        public List<ModelInput> GetModelsJson(int limit = 20)
        {
            var mlContext = new MLContext();

            var models = new List<ModelInput>();

            var mlModel = mlContext.Model.Load(GetAbsolutePath(ModelFilepath), out DataViewSchema inputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            // Create sample data to do a single prediction with it 
            //ModelInput sampleData = CreateSingleDataSample(mlContext, DATA_FILEPATH);
            var dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: DataFilepath,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true);

            // Here (ModelInput object) you could provide new test data, hardcoded or from the end-user application, instead of the row from the file.
            var sampleForPrediction = mlContext.Data.CreateEnumerable<ModelInput>(dataView, false)
                                                                        .Take(limit);

            foreach (var m in sampleForPrediction)
            {
                var predictionResult = predEngine.Predict(m);
                var score = (float)Math.Round(predictionResult.Score);
                score = score < 0 ? 0 : score;
                m.Sa_quantity = score;
                models.Add(m);
            }

            return models;            
        }
    }
}
