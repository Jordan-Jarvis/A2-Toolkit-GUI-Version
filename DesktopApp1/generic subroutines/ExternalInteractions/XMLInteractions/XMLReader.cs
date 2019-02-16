using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DesktopApp1
{
    public class XMLReader 
    {
        private List<VersionInfo> VList = new List<VersionInfo> { };
        public XMLReader(string XML)
        { 
            XmlTextReader reader = new XmlTextReader(XML);
            int i = 0;
            int type = 0; //type 0 is default, type 1 - link, 2- releaseDate, 3 - Version
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        if (reader.Name == "row")
                        {
                            VersionInfo test = new VersionInfo { };
                            VList.Add(test);
                            
                        }
                        if (reader.Name == "Link")
                        {
                            type = 1;
                        }
                        if (reader.Name == "ReleaseDate")
                        {
                            type = 2;
                        }
                        if (reader.Name == "Version")
                        {
                            type = 3;
                        }
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        Console.WriteLine(reader.Value);
                        Console.WriteLine(type);
                        switch (type)
                        {
                            case 0:
                                break;
                            case 1:
                                VList.Last().setLink(reader.Value);
                                break;
                            case 2:
                                VList.Last().setReleaseDate(reader.Value);
                                break;
                            case 3:
                                VList.Last().setVersion(reader.Value);
                                break;
                            default:
                                break;
                        }
                        


                        Console.WriteLine(reader.Value);
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        Console.Write("</" + reader.Name);
                        Console.WriteLine(">");
                        break;
                }
                i++;
            }

        }
        public List<VersionInfo> getList()
        {
            return VList;
        }
    }
}
