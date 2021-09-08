using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using KindergartenLogger.Models;
using KindergartenLogger.Views;

namespace KindergartenLogger.Services
{
    public interface ICheckFilePermission
    {
        bool CheckPermission();
    }

    public class DataHandler
    {
        public SHA256 Encrypt = SHA256.Create();
        public byte[] MainPassword = new byte[32]; //steal if gay

        private static bool debug=false;

        public string ChildNameText = "";
        public string ChildOIBText = "";

        public string GuardianNameText = "";
        public string GuardianOIBText = "";

        public Guardian GuardianPastebin = new Guardian();
        public Child ChildPastebin = new Child();

        public string PasswordForwardPage;
        public string PasswordBackwardPage;

        public static StreamWriter ChildWriter;
        public static StreamReader ChildReader;
        public static StreamWriter GuardianWriter;
        public static StreamReader GuardianReader;
        public static StreamWriter ConnectionWriter;
        public static StreamReader ConnectionReader;
        public static StreamWriter OptionsWriter;
        public static StreamReader OptionsReader;

        public ObservableCollection<Child> ChildList = new ObservableCollection<Child>();
        public string ChildListJson;
        public ObservableCollection<Guardian> GuardianList = new ObservableCollection<Guardian>();
        public string GuardianListJson;
        public ObservableCollection<Connection> ConnectionList = new ObservableCollection<Connection>();
        public string ConnectionListJson;
        public Options options = new Options();
        public string OptionsListJson;

        private readonly static string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private readonly string FilePathChild = path + @"/child.json";
        private readonly string FilePathGuardian = path + @"/guardian.json";
        private readonly string FilePathConnection = path + @"/connection.json";
        private readonly string FilePathOptions = path+@"/options.json";

        public DataHandler()
        {
            Console.WriteLine(path);
            MainPassword = Encrypt.ComputeHash(Encoding.UTF8.GetBytes("asd"));
            if (File.Exists(FilePathChild) && !debug)
            {
                ReadFromChild();
            }
            else {
                File.CreateText(FilePathChild).Dispose();
            }

            if (File.Exists(FilePathGuardian) && !debug)
            {
                ReadFromGuardian();
            }
            else
            {
                File.CreateText(FilePathGuardian).Dispose();
            }

            if (File.Exists(FilePathConnection) && !debug)
            {
                ReadFromConnection();
            }
            else
            {
                File.CreateText(FilePathConnection).Dispose();
            }

            if (File.Exists(FilePathOptions) && !debug)
            {
                ReadFromOptions();
            }
            else
            {
                File.CreateText(FilePathOptions).Dispose();
            }
        }
        public void WriteToChild()
        {
            ChildWriter = File.CreateText(FilePathChild);
            ChildListJson = JsonSerializer.Serialize(ChildList);
            ChildWriter.Write(ChildListJson);
            ChildWriter.Dispose();
        }
        public void WriteToGuardian() 
        {
            GuardianWriter = File.CreateText(FilePathGuardian);
            GuardianListJson = JsonSerializer.Serialize(GuardianList);
            GuardianWriter.Write(GuardianListJson);
            GuardianWriter.Dispose();
        }
        public void WriteToConnection()
        {
            ConnectionWriter = File.CreateText(FilePathConnection);
            ConnectionListJson = JsonSerializer.Serialize(ConnectionList);
            ConnectionWriter.Write(ConnectionListJson);
            ConnectionWriter.Dispose();
        }

        public void WriteToOptions() 
        {
            OptionsWriter = File.CreateText(FilePathOptions);
            OptionsListJson = JsonSerializer.Serialize(options);
            Console.WriteLine("Json Serializer options: "+OptionsListJson);
            OptionsWriter.Write(OptionsListJson);
            OptionsWriter.Dispose();
        }
        public void ReadFromChild()
        {
            Console.WriteLine("Reading from child.json:");
            ChildReader = File.OpenText(FilePathChild);
            ChildListJson = ChildReader.ReadToEnd();
            Console.WriteLine(ChildListJson);
            ChildReader.Dispose();
            if (ChildListJson!="")
            {
                ChildList = JsonSerializer.Deserialize<ObservableCollection<Child>>(ChildListJson);
            }
            else 
            {

                WriteToChild();
            }
        }
        public void ReadFromGuardian()
        {
            Console.WriteLine("Reading from guardian.json:");
            GuardianReader = File.OpenText(FilePathGuardian);
            GuardianListJson = GuardianReader.ReadToEnd();
            Console.WriteLine(GuardianListJson);
            GuardianReader.Dispose();
            if (GuardianListJson != "")
            {
                GuardianList = JsonSerializer.Deserialize<ObservableCollection<Guardian>>(GuardianListJson);
            }
            else
            {
                WriteToGuardian();
            }
        }
        public void ReadFromConnection()
        {
            Console.WriteLine("Reading from connection.json:");
            ConnectionReader = File.OpenText(FilePathConnection);
            ConnectionListJson = ConnectionReader.ReadToEnd();
            Console.WriteLine(ConnectionListJson);
            ConnectionReader.Dispose();
            if (ConnectionListJson != "")
            {
                ConnectionList = JsonSerializer.Deserialize<ObservableCollection<Connection>>(ConnectionListJson);
            }
            else
            {
                WriteToConnection();
            }
        }
        public void ReadFromOptions()
        {
            Console.WriteLine("Reading from options.json:");
            OptionsReader = File.OpenText(FilePathOptions);
            OptionsListJson = OptionsReader.ReadToEnd();
            Console.WriteLine(OptionsListJson);
            OptionsReader.Dispose();
            if (OptionsListJson != "" && OptionsListJson != "[]")
            {
                options = JsonSerializer.Deserialize<Options>(OptionsListJson);
            }
            else
            {
                options.AutomaticEntryMessage = true;
                options.EntryTime = new TimeSpan(10, 0, 0);
                WriteToOptions();
            }
        }

        public void RefreshChild()
        {
            ChildEditList.RefreshChildList();
            ChildEntryList.RefreshChildList();
            ChildExitList.RefreshChildList();
            WriteToChild();
        }

        public void RefreshGuardian()
        {
            GuardianEditList.RefreshGuardianList();
            GuardianOptionsList.RefreshGuardianList();
            ConnectionAddPage.RefreshGuardian();
            ConnectionExitPage.RefreshGuardian();
            WriteToGuardian();
        }
        public void RefreshConnection()
        {
            ChildEditPage.RefreshConnectionList();
            ConnectionAddPage.RefreshGuardian();
            ConnectionExitPage.RefreshGuardian();
            WriteToConnection();
        }
        public void RefreshOptions()
        {
            MainOptionsPage.RefreshOptions();
            WriteToOptions();
        
        }


    }
}
