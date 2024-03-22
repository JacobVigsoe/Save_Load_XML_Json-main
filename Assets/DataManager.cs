using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.IO;
using System.Xml.Linq;


public class DataManager : MonoBehaviour
{
    // XML stuff
    private string _xmlData;    // Used for creating file
    private string _xmlDataPath;    // Used for creating directory

    // Json stuff
    private string _jsonDataPath; // Used for creating directory
    private string _jsonData;   // Used for creating file

    // Person info stuff
    private string age;
    private string nameOfPerson;
    private string doctor;


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
        //Setting up person data
        age = "25";
        nameOfPerson = "John Doe";
        doctor = "Dr. LægeMand";

        Initialize();
    }

    private void Initialize()
    {
        // Create new directory if it does not exist
        NewDirectory(_xmlDataPath);
        NewDirectory(_jsonDataPath);

        // Write to XML and JSON
        // Can turn these off and on to see that the json file is created from the xml file by first running the xml method first, then the json method after
        WriteToXML(_xmlData);
        WriteToJSON(_xmlData);
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
    public void WriteToXML(string filename)
    {
        // Check if file already exists
        if(File.Exists(filename))
        {
            Debug.Log("File already exists");
            return;
        }

        //Creates filestream and xmlwriter to work with
        FileStream xmlStream = File.Create(filename);
        XmlWriter xmlWriter = XmlWriter.Create(xmlStream);

        // Write to XML file
        xmlWriter.WriteStartDocument();

        // Start of an element in xml file
        xmlWriter.WriteStartElement("PersonData");

        //Information in the element
        xmlWriter.WriteElementString("Person", nameOfPerson);    
        xmlWriter.WriteElementString("Age", age);
        xmlWriter.WriteElementString("NameOfDoctor", doctor);
        
        // End of the element
        xmlWriter.WriteEndElement();

        // End of the document
        xmlWriter.Close();
        xmlStream.Close();
        
        Debug.Log("File created");
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
    
    public void WriteToJSON(string filename)
    {
        // Check if file already exists
        string jsonData = ReadXML(filename);
        if(jsonData == null)
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



}
