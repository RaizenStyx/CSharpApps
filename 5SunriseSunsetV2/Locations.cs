using SQLite;

/// <summary>
/// Name:
/// File:
/// Project:
/// Revision Date:
/// </summary>

namespace SunriseSunset
{


    /// <summary>
    /// 
    /// </summary>
    public class LocalNames
    {
        public string en { get; set; }
        public string ru { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CityLocation
    {
        public string name { get; set; }
        public LocalNames local_names { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
        public string state { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CityLocations
    {
        public CityLocation results { get; set; }
        public string status { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Location
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(20)]
        public string latitude { get; set; }

        [MaxLength(20)]
        public string longtitude { get; set; }

        [MaxLength(20)]
        public string stationID { get; set; }

        [MaxLength(100)]
        public string name { get; set; }

        [MaxLength(20)]
        public string country { get; set; }

        [MaxLength(20)]
        public string state { get; set; }

    }
}