using MachineLearningWithDotNetML.ConsoleApp;
using MachineLearningWithDotNetML.Model.DataModels;
using Microsoft.ML;
using System;

namespace MachineLearningWithDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            MLContext mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load("MLModel.zip", out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            // Use the code below to add input data
            var input = new ModelInput();
            input.Location = 1193;
            input.Product = 2;
            input.Temp_mean = 5.51f;
            input.Temp_max = 7.24f;
            input.Temp_min = 3.78f;
            input.Sunshine_quant = 285;
            input.Event = "New Year's Day observed";
            input.Price = 1.5f;

            // input.

            // Try model on sample data
            ModelOutput result = predEngine.Predict(input);
           
            Console.WriteLine(result.Sa_quantity);
        }
    }
}
