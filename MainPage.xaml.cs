using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Young_Chemist
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page

    {
        private int score = 0;
        private bool answer = false;
        private int comboleft1 = 0;
        private int comboleft2 = 0;
        private int comboright = 0;

        private Core first = null;
        private Core second = null;
        private Core final = null;

        private List<Equation> levels = new List<Equation>();
        
        private int currentLevel = 0;

        public MainPage()
        {
            this.InitializeComponent();
            LoadJson();
            SetUpLevel();
            //levels = JsonConvert.DeserializeObject<List<Equation>>(Data.Data);

        }
        public void LoadJson()
        {
            using (StreamReader r = new StreamReader(@"Assets/Data/Data.txt"))
            {
                
                string json = r.ReadToEnd();
                levels = JsonConvert.DeserializeObject<List<Equation>>(json);
            }
        }
        public void SetUpLevel()
        {
            var level = levels[currentLevel];
            Core firstCurrent = null;
            string firstCurrentError = null;
            Core secondCurrent = null;
            string secondCurrentError = null;
            Core finalCurrent = null;
            string finalCurrentError = null;

            if (!Parser.ParseFormula(level.First, out firstCurrent, out firstCurrentError))
            {
                throw new Exception(firstCurrentError);

            }
            if (!Parser.ParseFormula(level.Second, out secondCurrent, out secondCurrentError))
            {
                throw new Exception(secondCurrentError);

            }
            if (!Parser.ParseFormula(level.Final, out finalCurrent, out finalCurrentError))
            {
                throw new Exception(finalCurrentError);

            }

            first = firstCurrent;
            second = secondCurrent;
            final = finalCurrent;
            



            comboLeft1.SelectedItem = "1";
            comboLeft2.SelectedItem = "1";
            comboRight.SelectedItem = "1";



            Lewy1.Text = level.First;
            Lewy2.Text = level.Second;
            Prawy.Text = level.Final;
            //TODO
        }

        public void nextLevel()
        {
            if(currentLevel < levels.Count - 1)
            {
                currentLevel++;
                SetUpLevel();
            }
        }

        public bool isMatching()
        {
            bool match = true;
            foreach(var atom1 in first.GetAllSymbols())
            foreach(var atom2 in second.GetAllSymbols())
                {
                    int firstCount = first.GetTotalAtomCount(atom1);
                    int secondCount = second.GetTotalAtomCount(atom2);
                    int finalCount1 = final.GetTotalAtomCount(atom1);
                    int finalCount2 = final.GetTotalAtomCount(atom2);
                    if(firstCount != finalCount1 || secondCount != finalCount2)
                    {
                        match = false;
                        break;
                    }
                }
            return match;
        }
        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

           
        }

        private void TextBlock_SelectionChanged_2(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            comboleft1 = int.Parse(comboLeft1.SelectedItem.ToString());
            comboleft2 = int.Parse(comboLeft2.SelectedItem.ToString());
            comboright = int.Parse(comboRight.SelectedItem.ToString());
            

            
            if (isMatching())
            {
                score++;
                scoreTextBlock.Text = "Player score: " + score.ToString();
            }

            nextLevel();
            

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                   

        }

        private void TextBlock_SelectionChanged_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
