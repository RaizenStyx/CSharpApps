using SQLite;

namespace SunriseSunset
{
    public class LocalNames
    {
        public string en { get; set; }
        public string ru { get; set; }
    }

    // below are objects that are created from JSON using JSONutillites
    
    // Locations that get inserted into database 
    public class Location
    {
        public string name { get; set; }
        public LocalNames local_names { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
        public string state { get; set; }
    }

    // Taking locations, this creates a database with ID, name, lat, and lon
    public class City
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(100)]
        public string name { get; set; }

        [MaxLength(20)]
        public string lat { get; set; }

        [MaxLength(20)]
        public string lon { get; set; }

    }

    // used for getting time of sunrise ect.
    public class Results
    {
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string solar_noon { get; set; }
        public string day_length { get; set; }
        public string civil_twilight_begin { get; set; }
        public string civil_twilight_end { get; set; }
        public string nautical_twilight_begin { get; set; }
        public string nautical_twilight_end { get; set; }
        public string astronomical_twilight_begin { get; set; }
        public string astronomical_twilight_end { get; set; }
    }

    public class SunriseSunset
    {
        public Results results { get; set; }
        public string status { get; set; }
    }

}