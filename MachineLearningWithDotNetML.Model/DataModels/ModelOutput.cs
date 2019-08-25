//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using System;
using Microsoft.ML.Data;

namespace MachineLearningWithDotNetML.Model.DataModels
{
    public class ModelOutput
    {
        [ColumnName("sa_quantity"), LoadColumn(3)]
        public float Score { get; set; }

        public float Sa_quantity { get; set; }

        [ColumnName("location"), LoadColumn(0)]
        public float Location { get; set; }


        [ColumnName("product"), LoadColumn(1)]
        public float Product { get; set; }


        [ColumnName("date"), LoadColumn(2)]
        public string Date { get; set; }

        [ColumnName("temp_mean"), LoadColumn(4)]
        public float Temp_mean { get; set; }


        [ColumnName("temp_max"), LoadColumn(5)]
        public float Temp_max { get; set; }


        [ColumnName("temp_min"), LoadColumn(6)]
        public float Temp_min { get; set; }


        [ColumnName("sunshine_quant"), LoadColumn(7)]
        public float Sunshine_quant { get; set; }


        [ColumnName("event"), LoadColumn(8)]
        public string Event { get; set; }


        [ColumnName("price"), LoadColumn(9)]
        public float Price { get; set; }

        public static string getHeader()
        {
            return string.Format("location,product,date,sa_quantity," +
                "temp_mean,temp_max,temp_min,sunshine_quant," +
                "event,price");
        }

        public string getRow()
        {
            return string.Format("location,product,date,sa_quantity," +
                "temp_mean,temp_max,temp_min,sunshine_quant," +
                "event,price");
        }
    }
}
