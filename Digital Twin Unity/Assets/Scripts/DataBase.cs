using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

public class DataBase : MonoBehaviour
{
    public float currentLightRoom1 = 1f;
    public float TemperatureRoom1 = 0f;
    public float HumidityRoom1 = 0f;
    public float FatigueRoom1 = 0f;
    public float ComfortRoom1 = 0f;

    public float currentLightRoom2 = 1f;
    public float TemperatureRoom2 = 0f;
    public float HumidityRoom2 = 0f;
    public float FatigueRoom2 = 0f;
    public float ComfortRoom2 = 0f;

    public float currentLightRoom3 = 1f;
    public float TemperatureRoom3 = 0f;
    public float HumidityRoom3 = 0f;
    public float FatigueRoom3 = 0f;
    public float ComfortRoom3 = 0f;
    public string HydrogenRoom3;
    public string LPGRoom3;
    public string MethaneRoom3;
    public string CarbonRoom3;
    public string AlcoholRoom3;
    public string SmokeRoom3;
    public string PropaneRoom3;

    public float currentLightOutside = 1f;
    public float Hour=13f;
    public string Time= "10:10";

    public float outsideHumidity;
    public float TemperatureOutside = 0f;
    public float FatigueOutside = 0f;
    public float ComfortOutside = 0f;
    public float MoistureOutside = 0f;

    MongoClient client = new MongoClient("mongodb://Admin:admin1234@147.156.85.248:27017/?authSource=CloudDB&readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;
    void Start()
    {
        database = client.GetDatabase("CloudDB");
        collection = database.GetCollection<BsonDocument>("CloudDB");
        StartCoroutine(ReadData());
    }

    IEnumerator ReadData()
    {
        while (true)
        {
            var room1 = Builders<BsonDocument>.Filter.Eq("Place", "room1");
            var room1Document = collection.Find(room1).FirstOrDefault();

            var myObjR1 = BsonSerializer.Deserialize<room1_Data>(room1Document);
            currentLightRoom1 = float.Parse(myObjR1.Light.Remove(myObjR1.Light.Length - 1)) / 100;
            Hour=float.Parse(myObjR1.Time.Remove(myObjR1.Time.Length-6));
            Time= myObjR1.Time.Remove(myObjR1.Time.Length-3);
            TemperatureRoom1 = float.Parse(myObjR1.Temperature.Remove(myObjR1.Temperature.Length-2));
            HumidityRoom1 = float.Parse(myObjR1.Humidity.Remove(myObjR1.Humidity.Length-1));
            FatigueRoom1 = float.Parse(myObjR1.Fatigue.Remove(myObjR1.Fatigue.Length-1));
            ComfortRoom1 = float.Parse(myObjR1.Comfort);

            var room2 = Builders<BsonDocument>.Filter.Eq("Place", "room2");
            var room2Document = collection.Find(room2).FirstOrDefault();

            var myObjR2 = BsonSerializer.Deserialize<room2_Data>(room2Document);
            currentLightRoom2 = float.Parse(myObjR2.Light.Remove(myObjR2.Light.Length - 1)) / 100;
            TemperatureRoom2 = float.Parse(myObjR2.Temperature.Remove(myObjR2.Temperature.Length-2));
            HumidityRoom2 = float.Parse(myObjR2.Humidity.Remove(myObjR2.Humidity.Length-1));
            FatigueRoom2 = float.Parse(myObjR2.Fatigue.Remove(myObjR2.Fatigue.Length-1));
            ComfortRoom2 = float.Parse(myObjR2.Comfort);

            var room3 = Builders<BsonDocument>.Filter.Eq("Place", "room3");
            var room3Document = collection.Find(room3).FirstOrDefault();

            var myObjR3 = BsonSerializer.Deserialize<room3_Data>(room3Document);
            currentLightRoom3 = float.Parse(myObjR3.Light.Remove(myObjR3.Light.Length - 1)) / 100;
            TemperatureRoom3 = float.Parse(myObjR3.Temperature.Remove(myObjR3.Temperature.Length-2));
            HumidityRoom3 = float.Parse(myObjR3.Humidity.Remove(myObjR3.Humidity.Length-1));
            FatigueRoom3 = float.Parse(myObjR3.Fatigue.Remove(myObjR3.Fatigue.Length-1));
            ComfortRoom3 = float.Parse(myObjR3.Comfort);
            HydrogenRoom3=myObjR3.Hydrogen;
            LPGRoom3=myObjR3.LPG;
            MethaneRoom3=myObjR3.Methane;
            SmokeRoom3=myObjR3.Smoke;
            CarbonRoom3=myObjR3.Carbon;
            AlcoholRoom3=myObjR3.Alcohol;
            PropaneRoom3=myObjR3.Propane;

            var outside = Builders<BsonDocument>.Filter.Eq("Place", "outside");
            var outsideDocument = collection.Find(outside).FirstOrDefault();

            var myObjO = BsonSerializer.Deserialize<outside_Data>(outsideDocument);
            currentLightOutside = float.Parse(myObjO.Light.Remove(myObjO.Light.Length - 1)) / 100;
            outsideHumidity = float.Parse(myObjO.Humidity.Remove(myObjO.Humidity.Length - 1)) / 100;
            TemperatureOutside = float.Parse(myObjO.Temperature.Remove(myObjO.Temperature.Length-2));
            FatigueOutside = float.Parse(myObjO.Fatigue.Remove(myObjO.Fatigue.Length-1));
            ComfortOutside = float.Parse(myObjO.Comfort);
            MoistureOutside = float.Parse(myObjO.Moisture.Remove(myObjO.Moisture.Length-1));


            yield return new WaitForSeconds(5);
        }
    }
}

public class room1_Data
{
    public ObjectId _id { set; get; }
    public string name { set; get; }

    public string Place { set; get; }
    public string Temperature { set; get; }
    public string Humidity { set; get; }
    [MongoDB.Bson.Serialization.Attributes.BsonElement("Light Intensity")]
    public string Light { set; get; }
    [MongoDB.Bson.Serialization.Attributes.BsonElement("Fatigue Index")]
    public string Fatigue { set; get; }
    [MongoDB.Bson.Serialization.Attributes.BsonElement("Comfort Index")]
    public string Comfort { set; get; }
    public string Time { set; get; }
}

public class room2_Data
{
    public ObjectId _id { set; get; }
    public string name { set; get; }

    public string Place { set; get; }
    public string Temperature { set; get; }
    public string Humidity { set; get; }
    [MongoDB.Bson.Serialization.Attributes.BsonElement("Light Intensity")]
    public string Light { set; get; }
    [MongoDB.Bson.Serialization.Attributes.BsonElement("Fatigue Index")]
    public string Fatigue { set; get; }
    [MongoDB.Bson.Serialization.Attributes.BsonElement("Comfort Index")]
    public string Comfort { set; get; }
    public string Time { set; get; }
}

public class room3_Data
{
    public ObjectId _id { set; get; }
    public string name { set; get; }

    public string Place { set; get; }
    public string Temperature { set; get; }
    public string Humidity { set; get; }
    [MongoDB.Bson.Serialization.Attributes.BsonElement("Light Intensity")]
    public string Light { set; get; }
    public string Hydrogen { set; get; }
    public string LPG { set; get; }
    public string Methane { set; get; }
    [MongoDB.Bson.Serialization.Attributes.BsonElement("Carbon Monoxide")]
    public string Carbon { set; get; }
    public string Alcohol { set; get; }
    public string Smoke { set; get; }
    public string Propane { set; get; }
    [MongoDB.Bson.Serialization.Attributes.BsonElement("Fatigue Index")]
    public string Fatigue { set; get; }
    [MongoDB.Bson.Serialization.Attributes.BsonElement("Comfort Index")]
    public string Comfort { set; get; }
    public string Time { set; get; }
}

public class outside_Data
{
    public ObjectId _id { set; get; }
    public string name { set; get; }

    public string Place { set; get; }
    public string Temperature { set; get; }
    public string Humidity { set; get; }
    public string Moisture { set; get; }
    [MongoDB.Bson.Serialization.Attributes.BsonElement("Light Intensity")]
    public string Light { set; get; }
    [MongoDB.Bson.Serialization.Attributes.BsonElement("Fatigue Index")]
    public string Fatigue { set; get; }
    [MongoDB.Bson.Serialization.Attributes.BsonElement("Comfort Index")]
    public string Comfort { set; get; }
    public string Time { set; get; }
}

