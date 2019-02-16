using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp1
{
    public class getCommands
    {




        List<string> yList = new List<string> { };
       List<string> xList = new List<string> { };
        public getCommands() { }
        public string[,] findCommands(string filePath)
        {
            
            var lines = File.ReadAllLines(filePath);



            int j = 0;


            foreach (var line in lines)
            {



                Console.WriteLine(line);
                string[] field = line.Split(' ');
                if (field.GetLength(0) > 2)
                {
                    

                    if (field[2].Contains("flash"))
                    {
                        j++;

                        string[] temp = field[4].Split('\\');
                         xList.Add(temp[1]);
                        yList.Add(field[3]);

                    }

                }
                


                //commandList[j][0] = field[3];
                //MessageBox.Show("herro");
                //commandList[j][1] = temp[1];

            }

            string[,] commands = new string[xList.Count(), 2];
            int z = 0;
            foreach (string s in xList)
            {
                commands[z,0] = s;
                z++;
            }
            z = 0;
            foreach (string s in yList)
            {
                commands[z, 1] = s;
                Console.WriteLine(z);
                z++;
            }
            return commands;
        }

    }
}
