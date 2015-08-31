using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FPtoUnit3DHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public List<TableElements> Lamps;
        public List<TableElements> Switches;

        public MainWindow()
        {
            InitializeComponent();
           
            foreach (var item in Enum.GetValues(typeof(TableElements.Unit3dSwitchType)))
            {
                _comboUnit3dSwitchType.Items.Add(item);
            }
           
        }
        private void MetroWindow_Deactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
        }

        private void _radioLamp_Checked(object sender, RoutedEventArgs e)
        {
            _comboUnit3dSwitchType.IsEnabled = false;
        }

        private void _buttonStart_Click(object sender, RoutedEventArgs e)
        {
            _radioSwitch.IsEnabled = false;
            _radioLamp.IsEnabled = false;
            _buttonStart.IsEnabled = false;
            _setLampCount.IsEnabled = false;
            _buttonSkip.IsEnabled = true;
            _buttonSave.IsEnabled = true;
            _buttonOK.IsEnabled = true;
            _buttonUnused.IsEnabled = true;

            _textFPName.IsEnabled = true;
            _texDescription.IsEnabled = true;
            _buttonArrowLeft.IsEnabled = true;
            _buttonArrowRight.IsEnabled = true;

            _buttonRestart.IsEnabled = true;

            if (_radioSwitch.IsChecked == true)
            {
                _comboUnit3dSwitchType.IsEnabled = true;
                Switches = new List<TableElements>();

                for (int i = 0; i < (int)_setLampCount.Value; i++)
                {
                    Switches.Add(new TableElements(i, ""));
                }

                int switchCount = Switches.Count;
                _totalNumberOfObjects.Text = switchCount.ToString();
                _goToNumber.Maximum = switchCount;
            }
            else
            {
                Lamps = new List<TableElements>();

                for (int i = 0; i < (int)_setLampCount.Value; i++)
			    {
                    Lamps.Add(new TableElements(i,""));
			    }

                int lampCount = Lamps.Count;
                _totalNumberOfObjects.Text = lampCount.ToString();
                _goToNumber.Maximum = lampCount;
               
            }

        }
        private void _buttonRestart_Click(object sender, RoutedEventArgs e)
        {
            _buttonRestart.IsEnabled = false;
            _radioSwitch.IsEnabled = true;
            _radioLamp.IsEnabled = true;
            _buttonStart.IsEnabled = true;
            _setLampCount.IsEnabled = true;
            _buttonSkip.IsEnabled = false;
            _buttonSave.IsEnabled = false;
            _buttonOK.IsEnabled = false;
            _buttonUnused.IsEnabled = false;

            _textFPName.IsEnabled = false;
            _texDescription.IsEnabled = false;
            _buttonArrowLeft.IsEnabled = false;
            _buttonArrowRight.IsEnabled = false;

            _currentNumberOfObjects.Text = "1";
            _totalNumberOfObjects.Text ="1";

            _textTableName.Text = "";
        }
        private void _buttonOK_Click(object sender, RoutedEventArgs e)
        {
            int currentNumber = int.Parse(_currentNumberOfObjects.Text);
            int totalNumber = int.Parse(_totalNumberOfObjects.Text);

            if (_radioLamp.IsChecked == true)
            {
                if (Lamps.Count == 0)
                    Lamps.Add(new TableElements(currentNumber, _textFPName.Text, _texDescription.Text));
                else if (Lamps.ElementAtOrDefault(currentNumber - 1) != null)
                {
                    Lamps.ElementAt(currentNumber - 1).name = _textFPName.Text;
                    Lamps.ElementAt(currentNumber - 1).description = _texDescription.Text;
                }
                else
                {
                    Lamps.Add(new TableElements(currentNumber, _textFPName.Text, _texDescription.Text));
                }
            }
            else if (_radioSwitch.IsChecked==true)
            {
                if (Switches.Count == 0)
                {
                    Switches.Add(new TableElements(currentNumber, _textFPName.Text, _texDescription.Text,_comboUnit3dSwitchType.SelectedItem.ToString()));
                    totalNumber++;
                    _totalNumberOfObjects.Text = totalNumber.ToString();

                }
                else if (Switches.ElementAtOrDefault(currentNumber - 1) != null)
                {
                    Switches.ElementAt(currentNumber - 1).name = _textFPName.Text;
                    Switches.ElementAt(currentNumber - 1).description = _texDescription.Text;
                    Switches.ElementAt(currentNumber - 1).type = _comboUnit3dSwitchType.Text;
                }
                else
                {
                    Switches.Add(new TableElements(currentNumber, _textFPName.Text, _texDescription.Text, _comboUnit3dSwitchType.SelectedItem.ToString()));
                }
            }

            if (_currentNumberOfObjects.Text == _totalNumberOfObjects.Text)
            {
                totalNumber++;
                _totalNumberOfObjects.Text = totalNumber.ToString();
                _goToNumber.Maximum = totalNumber;
            }
            
            _buttonArrowRight_Click(sender, null);
        }
        private void _texDescription_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                _buttonOK_Click(sender, null);
        }
        private void _buttonSkip_Click(object sender, RoutedEventArgs e)
        {
            //Get the current switch or lamp number.
            //If exists in the list edit values or add new TableELement to list
            int currentNumber = int.Parse(_currentNumberOfObjects.Text);
            int totalNumber = int.Parse(_totalNumberOfObjects.Text);

            if (_radioLamp.IsChecked == true)
            {
                if (Lamps.Count == 0)
                    Lamps.Add(new TableElements(currentNumber, "Skip", _texDescription.Text));
                else if (currentNumber - 1 > Lamps.Count)
                {
                    Lamps.ElementAt(currentNumber - 1).name = "skip";
                    Lamps.ElementAt(currentNumber - 1).description = "skip";
                }
                else
                    Lamps.Add(new TableElements(currentNumber, "Skip", _texDescription.Text));
            }
            else if (_radioSwitch.IsChecked==true)
            {
                if (Switches.Count == 0)
                    Switches.Add(new TableElements(currentNumber, "Skip", _texDescription.Text));
                else if (currentNumber - 1 > Switches.Count)
                {
                    Switches.ElementAt(currentNumber - 1).name = "Skip";
                    Switches.ElementAt(currentNumber - 1).description = "Skip";
                }
                else
                    Switches.Add(new TableElements(currentNumber, "Skip", _texDescription.Text));

            }

            if (_currentNumberOfObjects.Text == _totalNumberOfObjects.Text)
            {
                totalNumber++;
                _totalNumberOfObjects.Text = totalNumber.ToString();
            }
            _buttonArrowRight_Click(sender, null);
        }
        private void _buttonUnused_Click(object sender, RoutedEventArgs e)
        {
            //Get the current switch or lamp number.
            //If exists in the list edit values or add new TableELement to list
            int currentNumber = int.Parse(_currentNumberOfObjects.Text);
            int totalNumber = int.Parse(_totalNumberOfObjects.Text);

            if (_radioLamp.IsChecked == true)
            {
                if (Lamps.Count == 0)
                    Lamps.Add(new TableElements(currentNumber, "Unused", _texDescription.Text));
                else if (currentNumber < Lamps.Count)
                {
                    Lamps.ElementAt(currentNumber -1).name = "Unused";
                    Lamps.ElementAt(currentNumber -1).description = "Unused";
                }
                else
                    Lamps.Add(new TableElements(currentNumber, "Unused", _texDescription.Text));
            }
            else if (_radioSwitch.IsChecked == true)
            {
                if (Switches.Count == 0)
                    Switches.Add(new TableElements(currentNumber, "Unused", _texDescription.Text));
                else if (currentNumber - 1 < Switches.Count)
                {
                    Switches.ElementAt(currentNumber - 1).name = "Unused";
                    Switches.ElementAt(currentNumber - 1).description = "Unused";
                }
                else
                    Switches.Add(new TableElements(currentNumber, "Unused", _texDescription.Text));

            }

            if (_currentNumberOfObjects.Text == _totalNumberOfObjects.Text)
            {
                totalNumber++;
                _totalNumberOfObjects.Text = totalNumber.ToString();
            }

            _buttonArrowRight_Click(sender, null);
        }
        private void _buttonArrowLeft_Click(object sender, RoutedEventArgs e)
        {
             int currentNumber = int.Parse(_currentNumberOfObjects.Text);
             if (currentNumber <= 1)
             { }
             else
             {
                 currentNumber--;
                 _currentNumberOfObjects.Text = currentNumber.ToString();

                 if (_radioLamp.IsChecked == true)
                 {
                     _textFPName.Text = Lamps.ElementAt(currentNumber - 1).name;
                     _texDescription.Text = Lamps.ElementAt(currentNumber - 1).description;
                 }
                 else if (_radioSwitch.IsChecked==true)
                 {
                     _textFPName.Text = Switches.ElementAt(currentNumber - 1).name;
                     _texDescription.Text = Switches.ElementAt(currentNumber - 1).description;
                     _comboUnit3dSwitchType.Text = Switches.ElementAt(currentNumber - 1).type;
                 }
             }
        }
        private void _buttonArrowRight_Click(object sender, RoutedEventArgs e)
        {
            int currentNumber = int.Parse(_currentNumberOfObjects.Text);
            int totalNumber = int.Parse(_totalNumberOfObjects.Text);

            if (currentNumber >= totalNumber)
            { }
            else
            {
                currentNumber++;
                _currentNumberOfObjects.Text = currentNumber.ToString();

                if (_radioLamp.IsChecked == true)
                {
                    if (Lamps.ElementAtOrDefault(currentNumber - 1) != null)
                    {
                        _textFPName.Text = Lamps.ElementAt(currentNumber - 1).name;
                        _texDescription.Text = Lamps.ElementAt(currentNumber - 1).description;
                    }
                    else
                    {
                        _textFPName.Text = "";
                        _texDescription.Text = "";
                    }
                }
                else if (_radioSwitch.IsChecked ==true)
                {
                    if (Switches.ElementAtOrDefault(currentNumber - 1) != null)
                    {
                        _textFPName.Text = Switches.ElementAt(currentNumber - 1).name;
                        _texDescription.Text = Switches.ElementAt(currentNumber - 1).description;
                        _comboUnit3dSwitchType.Text = Switches.ElementAt(currentNumber-1).type;
                    }
                    else
                    {
                        _textFPName.Text = "";
                        _texDescription.Text = "";
                        _comboUnit3dSwitchType.SelectedIndex = 0;
                    }
                }
            }
        }
        private void _buttonSave_Click(object sender, RoutedEventArgs e)
        {            
            int t = 1;

            string outputFileName ="";

            if (_radioLamp.IsChecked == true)
            {
                if (_textTableName.Text == "")
                {
                    if (System.IO.File.Exists(@"Lamps.txt"))
                    {
                        System.IO.File.Delete(@"Lamps.txt");
                        
                    }
                    outputFileName = "Lamps.txt";
                }
                else
                {
                    if (System.IO.File.Exists(_textTableName.Text + "_Lamps.txt"))
                    {
                        System.IO.File.Delete(_textTableName.Text + "_Lamps.txt");
                       
                    }

                    outputFileName = _textTableName.Text + "_Lamps.txt";
                }

                foreach (var item in Lamps)
                {
                    string lines = string.Empty;
                    if (string.IsNullOrEmpty(item.name))
                        item.name = "Light" + t;

                    if (item.name != "Skip")
                    {
                        if (item.name !="Unused")
                            lines = "AddLamp(" + "\"" + item.name + "\"" + ", " + (item.number + 1) + ");                  ////" + item.description;
                        else
                            lines = "//// Lamp " + (item.number + 1) + " NOT USED  " + item.description;

                        System.IO.StreamWriter file = new System.IO.StreamWriter(outputFileName, true);
                        file.WriteLine(lines);
                        file.Close();
                    }

                    t++;
                }
            }
            else if (_radioSwitch.IsChecked==true)
            {
                if (_textTableName.Text == "")
                {
                    if (System.IO.File.Exists(@"Switches.txt"))
                    {
                        System.IO.File.Delete(@"Switches.txt");                        
                    }

                    outputFileName = "Switches.txt";
                }
                else
                {
                    if (System.IO.File.Exists(_textTableName.Text + "_Switches.txt"))
                    {
                        System.IO.File.Delete(_textTableName.Text + "_Switches.txt");
                        
                    }

                    outputFileName = _textTableName.Text + "_Switches.txt";
                }

                foreach (var item in Switches)
                {
                    string lines = string.Empty;
                    if (string.IsNullOrEmpty(item.name))
                        item.name = "Unused";

                    if (item.name != "Skip")
                    {
                        if (item.name != "Unused")
                        {
                            if (item.type !="None")
                            {
                                if (item.type == TableElements.Unit3dSwitchType.BxSpinner.ToString())
                                    lines = "AddSwitch(" + "\"" + item.name + "/" + item.name + "\"" + ", " + t + "," + "\"" + item.type + "\"" + ");                  ////" + item.description;
                                else
                                {
                                    lines = "AddSwitch(" + "\"" + item.name + "\"" + ", " + t + ", " + "\"" + item.type + "\"" + ");                  ////" + item.description;
                                }                            
                            }
                            else
                                lines = "AddSwitch(" + "\"" + item.name + "\"" + ", " + t + ");                  ////" + item.description;
                        }
                        else
                            lines = "//// Switch " + t + " NOT USED   :" + item.description;

                        System.IO.StreamWriter file = new System.IO.StreamWriter(outputFileName, true);
                        file.WriteLine(lines);
                        file.Close();
                    }

                    t++;
                }
            }

        }

        private void _textFPName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _buttonOK_Click(sender, null);
            }
        }

        private void _goToNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (_buttonStart.IsEnabled)
                return;
            if (e.Key==Key.Enter)
            {
                _currentNumberOfObjects.Text = _goToNumber.Value.ToString();

                if (_radioLamp.IsChecked == true)
                {
                    if (Lamps.ElementAtOrDefault((int)_goToNumber.Value -1) != null)
                    {
                        _textFPName.Text = Lamps.ElementAt((int)_goToNumber.Value -1).name;
                        _texDescription.Text = Lamps.ElementAt((int)_goToNumber.Value -1).description;
                    }
                    else
                    {
                        _textFPName.Text = "";
                        _texDescription.Text = "";
                    }
                }
                else if (_radioSwitch.IsChecked == true)
                {
                    if (Switches.ElementAtOrDefault((int)_goToNumber.Value -1) != null)
                    {
                        _textFPName.Text = Switches.ElementAt((int)_goToNumber.Value -1).name;
                        _texDescription.Text = Switches.ElementAt((int)_goToNumber.Value-1).description;
                        _comboUnit3dSwitchType.Text = Switches.ElementAt((int)_goToNumber.Value-1).type;
                    }
                    else
                    {
                        _textFPName.Text = "";
                        _texDescription.Text = "";
                        _comboUnit3dSwitchType.SelectedIndex = 0;
                    }
                }
            }
        }




    }

    public class TableElements
    {
        public int number { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool notUsed { get; set; }
        public string type { get; set; }

        public enum Unit3dSwitchType
        {
            None=0,
            BxTriggerSwitch,
            BxDropTargetSwitch,
            BxLeafTargetSwitch,
            BxSlingshotSwitch,
            BxBumperSwitch,
            BxSpinner,

        }

        public TableElements(int number, string name, string desc = "", string type ="")
        {
            this.number = number;
            this.name = name;
            this.description = desc;
            this.type = type;

        }

    }
}
