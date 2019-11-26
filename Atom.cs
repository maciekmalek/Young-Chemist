using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Author: Aleksander Lagierski
//Date: 26/11/19
//Description: Class which represents chuj 
namespace Young_Chemist
{
    public class Atom
    {
        public string ChemSymbol { get; set; }
        public int Count { get; set; }
    }

    public class Formula
    {
        public List<Atom> Atoms { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var atom in Atoms)
                if (atom.Count > 1)
                    sb.Append($"{atom.ChemSymbol}{atom.Count.ToUnicodeSubscript()}");
                else
                    sb.Append(atom.ChemSymbol);
            return sb.ToString();

        }
        internal int Count = 1;
    }

    public static class IntExtensions
    {
        public static string ToUnicodeSubscript(this int value)
        {
            string valuestr = value.ToString();
            string nstr = "";
            foreach (char c in valuestr)
            {
                switch (c)
                {
                    case '0':
                        nstr += "\u2080";
                        break;
                    case '1':
                        nstr += "\u2081";
                        break;
                    case '2':
                        nstr += "\u2082";
                        break;
                    case '3':
                        nstr += "\u2083";
                        break;
                    case '4':
                        nstr += "\u2084";
                        break;
                    case '5':
                        nstr += "\u2085";
                        break;
                    case '6':
                        nstr += "\u2086";
                        break;
                    case '7':
                        nstr += "\u2087";
                        break;
                    case '8':
                        nstr += "\u2088";
                        break;
                    case '9':
                        nstr += "\u2089";
                        break;
                }
            }
            return nstr;
        }
    }
}
//