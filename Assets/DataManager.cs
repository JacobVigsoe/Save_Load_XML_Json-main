using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

[System.Serializable]
public class Person
{
    public string name;
    public string dateOfBirth;
    public string favColor;
}


public class DataManager : MonoBehaviour
{
    // XML stuff
    private string _xmlData;    // Used for creating file
    private string _xmlDataPath;    // Used for creating directory

    // Json stuff
    private string _jsonDataPath; // Used for creating directory
    private string _jsonData;   // Used for creating file




    void Awake()
    {
        _xmlDataPath = Application.persistentDataPath + "/XMLDataToSave/";  // Path to save XML data folder
        _jsonDataPath = Application.persistentDataPath + "/JSONDataToSave/";// Path to save JSON data folder

        _jsonData = _jsonDataPath + "Person_Data.json"; //Path to save JSON file
        _xmlData = _xmlDataPath + "Person_Data.xml";    //Path to save XML file

        //Debug.Log("XML Path: " + _xmlDataPath);
    }

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        // Create new directory if it does not exist
        NewDirectory(_xmlDataPath);
        NewDirectory(_jsonDataPath);

        // Create list of persons
        List<Person> persons = new List<Person>();

        // Add data for each person
        persons.Add(new Person { name = "Jacob vigso", dateOfBirth = "20/03/2002", favColor = "Blue" });
        persons.Add(new Person { name = "Karl", dateOfBirth = "01/05/2003", favColor = "Red" });
        persons.Add(new Person { name = "Torben", dateOfBirth = "19/12/2000", favColor = "Yellow" });
        persons.Add(new Person { name = "Felix", dateOfBirth = "7/08/2001", favColor = "Green" });
        persons.Add(new Person { name = "Jens", dateOfBirth = "24/09/1999", favColor = "LightBlue" });

        // Write to XML and JSON
        WriteToXML(_xmlData, persons);
        WriteToJSON(_xmlData, persons);
    }

    /// <summary>
    /// Creates a new directory if it does not exist
    /// </summary>
    /// <param name="dataPath"></param>
    public void NewDirectory(string dataPath)
    {
        if(Directory.Exists(dataPath))
        {
            Debug.Log("Directory already exists");
            return;
        }

        Directory.CreateDirectory(dataPath);
        Debug.Log("Directory created");
    }

    /// <summary>
    /// Writes to XML file
    /// </summary>
    /// <param name="filename"></param>
    public void WriteToXML(string filename, List<Person> persons)
    {
        // Create XML document
        XDocument doc = new XDocument(new XElement("Persons"));

        // Add data for each person
        foreach (Person person in persons)
        {
            XElement personElement = new XElement("Person",
                new XElement("Name", person.name),
                new XElement("DateOfBirth", person.dateOfBirth),
                new XElement("FavoriteColor", person.favColor)
            );
            doc.Root.Add(personElement);
        }

        // Save XML document to file
        doc.Save(filename);

        Debug.Log("XML file created at: " + filename);
    }

    public void WriteToJSON(string filename, List<Person> persons)
    {
        // Check if file already exists
        string jsonData = ReadXML(filename);
        if (jsonData == null)
        {
            Debug.Log("No data to convert");
            return;
        }

        // Convert XML to JSON by using the ReadXML method which returns the XDocument as a string
        using (StreamWriter stream = File.CreateText(_jsonData))
        {
            stream.WriteLine(jsonData); //Writes the string as a json file, but is still in XML format
            // I dont know how to convert it to JSON format
        }

        Debug.Log("JSON file created");
    }


    public string ReadXML(string filename)
    {
        // Check if file exists
        if(!File.Exists(filename))
        {
            Debug.Log("File does not exist");
            return null;
        }

        // Load the file into an XDocument
        XDocument doc = XDocument.Load(filename);
        //Debug.Log(doc.ToString());

        // Return the XDocument as a string
        return doc.ToString();
    }
    




}
