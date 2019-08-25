//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using System;
using System.IO;
using System.Linq;
using Microsoft.ML;
using MachineLearningWithDotNetML.Model.DataModels;
using System.Text;

namespace MachineLearningWithDotNetML.ConsoleApp
{
    class Program
    {
        //Machine Learning model to load and use for predictions
        private const string MODEL_FILEPATH = @"MLModel.zip";

        //Dataset to use for predictions 
        private const string DATA_FILEPATH = @"D:\projects\python\mc_test\input_data_pred.csv";

        static void Main(string[] args)
        {
            MLContext mlContext = new MLContext();
            var output_file = @"C:\Users\mmmat\Downloads\Output.csv";
            File.CreateText(output_file).Close();
            // Training code used by ML.NET CLI and AutoML to generate the model
            //ModelBuilder.CreateModel();

            ITransformer mlModel = mlContext.Model.Load(GetAbsolutePath(MODEL_FILEPATH), out DataViewSchema inputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            // Create sample data to do a single prediction with it 
            //ModelInput sampleData = CreateSingleDataSample(mlContext, DATA_FILEPATH);
            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: DATA_FILEPATH,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Here (ModelInput object) you could provide new test data, hardcoded or from the end-user application, instead of the row from the file.
            var sampleForPrediction = mlContext.Data.CreateEnumerable<ModelInput>(dataView, false)
                                                                        .ToList();

            File.AppendAllText(output_file, "location,product,date,sa_quantity,temp_mean,temp_max,temp_min,sunshine_quant,event,price");

            foreach (var m in sampleForPrediction)
            {
                ModelOutput predictionResult = predEngine.Predict(m);
                var score = Math.Round(predictionResult.Score);
                score = score < 0 ? 0 : score;
                Console.WriteLine($"Single Prediction --> Actual value: {m.Sa_quantity} | Predicted value: {score}");
                File.AppendAllText(output_file, string.Format("\n{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", 
                    m.Location, m.Product, m.Date, score, m.Temp_mean, m.Temp_max, m.Temp_min, m.Sunshine_quant,
                    m.Event, m.Price == 0 ? "" : m.Price.ToString()));
            }
            // Try a single prediction

            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }

        // Method to load single row of data to try a single prediction
        // You can change this code and create your own sample data here (Hardcoded or from any source)
        private static ModelInput CreateSingleDataSample(MLContext mlContext, string dataFilePath)
        {
            // Read dataset to get a single row for trying a prediction          
            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: dataFilePath,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Here (ModelInput object) you could provide new test data, hardcoded or from the end-user application, instead of the row from the file.
            ModelInput sampleForPrediction = mlContext.Data.CreateEnumerable<ModelInput>(dataView, false)
                                                                        .First();
            return sampleForPrediction;
        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }
    }
}
