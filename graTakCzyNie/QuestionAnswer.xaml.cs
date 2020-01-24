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
using System.Windows.Shapes;
using graTakCzyNieLibrary;

namespace graTakCzyNie
{
    /// <summary>
    /// Interaction logic for QuestionAnswer.xaml
    /// </summary>
    public partial class QuestionAnswer : Window
    {
        public QuestionAnswer(Question question)
        {
            InitializeComponent();
            TextBlockQuestionText.Text = question.QuestionText;
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
