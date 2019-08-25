using MachineLearningWithDotNetML.ConsoleApp;
using MachineLearningWithDotNetML.Model.DataModels;
using Microsoft.ML;
using System;

namespace MachineLearningWithDotNet
{
    class Program
    {
        //Machine Learning model to load and use for predictions
        private const string MODEL_FILEPATH = @"MLModel.zip";

        //Dataset to use for predictions 
        private const string DATA_FILEPATH = @"D:\projects\python\mc_test\input_data_pred.csv";

        static void Main(string[] args)
        {
            MachineLearningWithDotNetML.ConsoleApp.ModelBuilder.CreateModel();

        }
    }
}
