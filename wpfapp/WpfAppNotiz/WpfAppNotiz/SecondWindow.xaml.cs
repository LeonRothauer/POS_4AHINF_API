using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
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

namespace WpfAppNotiz
{
    /// <summary>
    /// Interaktionslogik für SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        private MainWindow previousWindow;
       
        private string baseApiURLpost = "http://localhost:3020/notiz";
        public SecondWindow(MainWindow previousWindow)
        {
            InitializeComponent();
            this.previousWindow = previousWindow;
            txtTitel.IsReadOnly = false;
            txtNotiz.IsReadOnly = false;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (previousWindow != null && string.IsNullOrEmpty(txtTitel.Text) == true && string.IsNullOrEmpty(txtNotiz.Text) == true)
            {
                this.Hide();
                previousWindow.btnLoad_Notizen();
                previousWindow.Show();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Sie haben die Notiz noch nicht hinzugefügt! Möchten Sie trotzdem zurückkehren?", "Bestätigung", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {   
                    this.Hide();
                    previousWindow.btnLoad_Notizen();
                    previousWindow.Show();
                }             
            }
        }
        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                Notiz newData = new Notiz
                {
                    Titel = txtTitel.Text,
                    Notiz_ = txtNotiz.Text
                };

                var jsonInput = JsonConvert.SerializeObject(newData);
                var requestContent = new StringContent(jsonInput, Encoding.UTF8, "application/json");
                var httpResponse = await client.PostAsync(baseApiURLpost, requestContent);

                txtTitel.Clear();
                txtNotiz.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
      
    }
}
