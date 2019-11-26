using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young_Chemist
{
    public class Core
    {
        public  List<Formula> Molecules { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var m in Molecules)
            {
                if (m.Count > 1)
                    sb.Append(m.Count + " " + m.ToString() + " ");
                else
                    sb.Append(m.ToString() + " ");
            }
            return sb.ToString().Trim();
        }

        public int GetTotalAtomCount(string symbol)
        {
            int count = 0;
            foreach (var m in Molecules)
            {
                int acount = 0;
                foreach (var a in m.Atoms)
                {
                    if (a.ChemSymbol == symbol)
                        acount += a.Count;
                }
                count += acount * m.Count;
            }

            return count;
        }

        public string[] GetAllSymbols()
        {
            List<string> symbols = new List<string>();

            foreach (var m in Molecules)
            {
                foreach (var a in m.Atoms)
                {
                    if (!symbols.Contains(a.ChemSymbol))
                        symbols.Add(a.ChemSymbol);
                }
            }

            return symbols.ToArray();
        }
    }
} 
