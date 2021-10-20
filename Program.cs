using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        public static string[,] DrawMap(string[] line, string[,] rest)
        {
            
            for(int i = 0; i< int.Parse(line[1]); i++)
            {
                for (int j = 0; j < int.Parse(line[2]); j++)
                {
                    rest[i,j] = "-";
                    
                }
            }
            return rest;
        }
        public static string[,] DrawMountain(string[] line, string[,] rest)
        {
            rest[int.Parse(line[1]),int.Parse(line[2])] = "M";
            return rest;
        }
        public static string[,] DrawTresor(string[] line, string[,] rest)
        {
            rest[int.Parse(line[1]),int.Parse(line[2])] = "T("+line[3]+")";
            return rest;
        }
        public static string[,] avancer(string[,]rest,int[] position,char orientation){
            switch(orientation){
                    case 'N' : if(rest[position[0]-1,position[1]]!=null){
                        rest[position[0] - 1,position[1]] = "N";
                                }
                                break;
                    case 'S': if(rest[position[0]+1,position[1]]!=null){
                                rest[position[0]+1,position[1]]="S";
                                }
                                break;
                    case 'O': if(rest[position[0],position[1]-1]!=null){
                                rest[position[0],position[1]-1]="O";//verifier ou est palcer le pion a chque fois
                                }
                                break;
                    case 'E': if(rest[position[0],position[1]+1]!=null){
                                rest[position[0],position[1]+1]="E";//verifier ou est palcer le pion a chque fois
                                }
                                break;
                    
                }
                return rest;
        }
        public static char tourner(char orientation){
            switch(orientation){
                    case 'N': orientation = 'N';
                                break;
                    case 'S': orientation = 'S';
                                break;
                    case 'O': orientation = '0';
                                break;
                    case 'E': orientation = 'E';
                                break;
                    
                }
                return orientation;
        }
        public static string[,] MoveAventurier(string[] line, string[,] rest)
        {
            string orientation = line[4];
            string name = line[1];
            char firstOrientation = orientation.ElementAt(0);
            char firstLetter = name.ElementAt(0);
            int[] position = new int[2];
            position[0] = int.Parse(line[2]);
            position[1] = int.Parse(line[3]);
            rest[int.Parse(line[2]),int.Parse(line[3])] = name.ElementAt(0).ToString();
            for( int i = 0;i< line[5].Length;i++){
                switch(line[5].ElementAt(i)){
                    case 'A' : rest=avancer(rest,position, firstOrientation);break;
                    case 'D': firstOrientation = tourner('D');break;
                    case 'G': firstOrientation = tourner('G');break;
                }
            }
            return rest;

        }
        public static string[,] LectureFichier(string[] line,string[,]rest)
        {
            string[] result = new string[3];
            switch (line[0])
            {
                case "C": rest = DrawMap(line,rest); break;
                case "M": rest = DrawMountain(line,rest); break ;
                case "T": rest = DrawTresor(line,rest); break;
                case "A":rest = MoveAventurier(line, rest); break;
                case "#":break;
                default: throw new Exception();
            }
            
            return rest;
        }
        static void Main(string[] args)
        {
            string[] text = System.IO.File.ReadAllLines(@"C:\Users\rdelannet\Documents\test.txt");
            string[,] result = new string[int.Parse(text[0].ElementAt(2).ToString()), int.Parse(text[0].ElementAt(4).ToString())];
            //string[] lineE = new string[text.Length]; 
            
            foreach(string line in text)
            {
                string[] tline = line.Split('-');
                result = LectureFichier(tline,result);
                
                
                
            }
            try
            {
                // Create the file, or overwrite if the file exists.
                using (System.IO.FileStream fs = System.IO.File.Create(@"C:\Users\rdelannet\Documents\test.txt"))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                // Open the stream and read it back.
                using (System.IO.StreamReader sr = System.IO.File.OpenText(@"C:\Users\rdelannet\Documents\test.txt"))
                {
                    string s = "";
                    for(int k =0;k < int.Parse(text[0].ElementAt(2).ToString()); k++)
                    {
                        for(int l = 0; l< int.Parse(text[0].ElementAt(4).ToString()); l++)
                        {
                            Console.WriteLine(result[k,l]);
                        }
                        
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
