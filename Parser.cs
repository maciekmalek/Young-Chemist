using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young_Chemist
{
    public class Parser
    {
        public static int AtomCountParse(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return 1;
            int result = 1;
            int.TryParse(str, out result);
            return result;
        }
        public static bool ParseFormula(string inputstr, out Core parsed, out string result)
        {
            Core core = new Core();
            core.Molecules = new List<Formula>();
            foreach (var input in inputstr.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries))
            {
                Formula formula = new Formula();
                formula.Atoms = new List<Atom>();
                string elementId = "";
                bool in_polyatomic = false;
                string count = "";
                for (int i = 0; i < input.Length; i++)
                {
                    char c = input[i];
                    if (char.IsWhiteSpace(c))
                        continue;
                    if (c == '(')
                    {
                        if (in_polyatomic == true)
                        {
                            result = "Error: Nested polyatomic.";
                            parsed = null;
                            return false;
                        }
                        if (!string.IsNullOrWhiteSpace(elementId))
                        {
                            //There's already an element to add.
                            Atom atom = new Atom
                            {
                                ChemSymbol = elementId,
                                Count = AtomCountParse(count)
                            };
                            formula.Atoms.Add(atom);
                            elementId = "";
                            count = "";
                        }

                        in_polyatomic = true;
                        continue;
                    }
                    if (c == ')')
                    {
                        if (in_polyatomic == false)
                        {
                            result = "Error: End of polyatomic, no beginning.";
                            parsed = null;
                            return false;
                        }
                        in_polyatomic = false;
                        continue;
                    }

                    if (char.IsUpper(c))
                    {
                        if (in_polyatomic)
                        {
                            elementId += c;
                            continue;
                        }
                        //Start of a new element.
                        if (!string.IsNullOrWhiteSpace(elementId))
                        {
                            //There's already an element to add.
                            Atom atom = new Atom
                            {
                                ChemSymbol = elementId,
                                Count = AtomCountParse(count)
                            };
                            formula.Atoms.Add(atom);
                            elementId = "";
                            count = "";
                        }
                        elementId += c;
                        continue;
                    }
                    else if (char.IsLower(c))
                    {
                        if (in_polyatomic)
                        {
                            elementId += c;
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(elementId))
                        {
                            //Malformed element ID.
                            result = "Error: Malformed chemical symbol. Chemical symbols must start with an uppercase letter.";
                            parsed = null;
                            return false;
                        }
                        elementId += c;
                        continue;
                    }
                    else if (char.IsNumber(c))
                    {
                        if (in_polyatomic)
                        {
                            elementId += c;
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(elementId))
                        {
                            result = "Error: Malformed atom! Atom count cannot precede the atom itself.";
                            parsed = null;
                            return false;
                        }
                        count += c;
                    }
                    else
                    {
                        result = "Malformed formula: Unrecognized character.";
                        parsed = null;
                        return false;
                    }
                }
                //Perform this check once again.
                if (!string.IsNullOrWhiteSpace(elementId))
                {
                    //There's already an element to add.
                    Atom atom = new Atom
                    {
                        ChemSymbol = elementId,
                        Count = AtomCountParse(count)
                    };
                    formula.Atoms.Add(atom);
                    elementId = "";
                    count = "";
                }
                if (in_polyatomic)
                {
                    result = "End of polyatomic expected.";
                    parsed = null;
                    return false;
                }
                core.Molecules.Add(formula);
            }

            result = "success";
            parsed = core;
            return true;
        }
    }
}
