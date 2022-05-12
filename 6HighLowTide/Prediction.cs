using System.Collections.Generic;

/// <summary>
/// Name: Connor Reed
/// File: Predictions.cs
/// Project: High_Low_Tides
/// Revision Date: 04/03/2022
/// </summary>

namespace High_Low_Tides.Models
{	// Class recieved from API
    public class Prediction
    {
        public string t { get; set; }
        public string v { get; set; }
        public string type { get; set; }
    }
	// List of first class
    public class Predictions
    {
        public IList<Prediction> tidepredictions { get; set; }
    }
	// Used this as a test case when it wasnt working.
    public class DisplayPredictions
    {
        public string DisplayTime  { get; set; }
        public string DisplayType { get; set; }
        public string DisplayValue { get; set; }
    }
}
