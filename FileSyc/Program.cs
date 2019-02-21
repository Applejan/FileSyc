using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;


namespace FileSyc
{
    class Program
    {
        static void Main(string[] args)
        {
            //Program Start
            //string choice;
            //Console.WriteLine("Choose Your Option：\n1：Company To Removeable Disk\n2：Home To Removeable Disk");
            //Console.WriteLine("3：Removeable Disk To Company\n4：Removeable Disk To Home\n");
            //choice = Console.ReadKey().KeyChar.ToString();
            //switch (choice)
            //{
            //    case "1":
            //        Console.WriteLine("Now Start Company To Removeable Disk\n");
            //        break;
            //        //case "2":
            //        //    Console.WriteLine("Press 2");
            //        //    break;
            //        //case "3":
            //        //    Console.WriteLine("Press 3");
            //        //    break;
            //        //case "4":
            //        //    Console.WriteLine("Press 4");
            //        //    break;
            //        //default:
            //        //    Console.WriteLine("You Should Make Right Choice!");
            //        //    return;
            //}

            //Start Logic
            FileGroup home = new FileGroup(EStations.Home.ToString());
            home.ReadFromXML(EStations.Home);
            home.Show();
            Console.ReadKey();
        }

        class FileGroup
        {
            public string Station { get; set; }
            public string WorkDir { get; set; }
            public string SoftWareDir { get; set; }
            public string ForsDir { get; set; }

            public FileGroup(string Station)
            {
                this.Station = Station;
            }

            public void Show()
            {
                Console.WriteLine(Station);
                Console.WriteLine(WorkDir);
                Console.WriteLine(SoftWareDir);
                Console.WriteLine(ForsDir);
                Console.ReadKey();
            }

            public void Setting(EStations station)
            {
                this.Station = station.ToString();
                Console.WriteLine("Choose WORK DIR\n");
                this.WorkDir = Console.ReadLine();
                Console.WriteLine("Choose SOFTWARE DIR\n");
                SoftWareDir = Console.ReadLine();
                Console.WriteLine("Choose FORS DIR\n");
                ForsDir = Console.ReadLine();
                //XElement el = new XElement();
            }

            public void ReadFromXML(EStations sta)
            {
                try
                {
                    XElement Info = XElement.Load("Info.xml", LoadOptions.None);
                    IEnumerable<XElement> _station =
                         from el in Info.Elements("Computer")
                         where (string)el.Attribute("Station") == sta.ToString()
                         select el;
                    if (Info == null)
                    {
                        Console.WriteLine("{0} Not exist in Info.xml", sta.ToString());
                        Console.WriteLine("Using Setting First for {0}", sta.ToString());
                        return;
                    }
                    foreach (var Compu in _station)
                    {
                        this.Station = sta.ToString();
                        this.WorkDir = (string)Compu.Element("WorkDir");
                        this.SoftWareDir = (string)Compu.Element("SoftwartDir");
                        this.ForsDir = (string)Compu.Element("ForsDir");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                
            }
        }

        enum EStations
        {
            Home,
            Cmopany,
            RemoveableDisk
        }

        void RunCmd(EStations Source,EStations Destination)
        {
            var CMDPath = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\cmd.exe";
            Process CMD= new Process();
            CMD.StartInfo.FileName = CMDPath;

        }
    }
}