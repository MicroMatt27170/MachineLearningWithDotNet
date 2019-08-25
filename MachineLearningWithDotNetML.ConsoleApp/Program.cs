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
using System.Collections.Generic;
using System.Text;

namespace MachineLearningWithDotNetML.ConsoleApp
{
    public class Program
    {
        //Machine Learning model to load and use for predictions
        private const string MODEL_FILEPATH = @"MLModel.zip";

        //Dataset to use for predictions 
        private const string DATA_FILEPATH = @"D:\projects\python\mc_test\input_data_pred.csv";

        static void Main(string[] args)
        {
            MLContext mlContext = new MLContext();

            // Training code used by ML.NET CLI and AutoML to generate the model
            //ModelBuilder.CreateModel();

            ITransformer mlModel = mlContext.Model.Load(GetAbsolutePath(MODEL_FILEPATH), out DataViewSchema inputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: DATA_FILEPATH,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Here (ModelInput object) you could provide new test data, hardcoded or from the end-user application, instead of the row from the file.
            IEnumerable<ModelInput> sampleForPrediction = mlContext.Data.CreateEnumerable<ModelInput>(dataView, false);

            StringBuilder csv = new StringBuilder();
            csv.Append(ModelOutput.getHeader());

            foreach (var m in sampleForPrediction)
            {
                // Create sample data to do a single prediction with it 

                // Try a single prediction
                ModelOutput predictionResult = predEngine.Predict(m);

                Console.WriteLine($"Single Prediction --> Actual value: {m.Sa_quantity} | Predicted value: {predictionResult.Score} \n");
                
                csv.Append()
                //Console.WriteLine("=============== End of process, hit any key to finish ===============");
            }

            
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
