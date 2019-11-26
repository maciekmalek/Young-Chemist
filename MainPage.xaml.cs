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
        private int comboleft = 0;
        private int comboright = 0;
        private List<Equation> levels = new List<Equation>();
        private int levelPasses = 0;
        private int currentLevel = 0;

        public MainPage()
        {
            this.InitializeComponent();
            LoadJson();
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
       
        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_SelectionChanged_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            comboleft = int.Parse(comboLeft.SelectedItem.ToString());
            comboright = int.Parse(comboRight.SelectedItem.ToString());
            if (comboleft == comboright)
            {
                answer = true;
            }
            else
            {
                answer = false;
            }
            if (answer == true)
            {
                score++;
                scoreTextBlock.Text = "Player score: " + score.ToString();
            }
            else
            {
                score = 0;
                scoreTextBlock.Text = "Player score: " + score.ToString();

            }


        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                   

        }

        private void TextBlock_SelectionChanged_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
