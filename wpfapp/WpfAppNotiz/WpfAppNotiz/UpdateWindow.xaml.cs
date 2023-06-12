using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public partial class UpdateWindow : Window
    {
        private MainWindow previousWindow;
        private Notiz selectedNotiz;
        private string baseApiURLpost = "http://localhost:3020/notiz";

        public UpdateWindow(MainWindow previousWindow, Notiz selectedNotiz)
        {
            InitializeComponent();
            this.previousWindow = previousWindow;
            this.selectedNotiz = selectedNotiz;

            txtTitel.Text = selectedNotiz.Titel;
            txtNotiz.Text = selectedNotiz.Notiz_;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (previousWindow != null && string.IsNullOrEmpty(txtTitel.Text) && string.IsNullOrEmpty(txtNotiz.Text))
            {
                this.Hide();
                previousWindow.Show();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Sie haben die Notiz noch nicht geändert! Möchten Sie trotzdem zurückkehren?", "Bestätigung", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    this.Hide();
                    previousWindow.Show();
                }
            }
        }

        public async void btnChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTitel.Text) || string.IsNullOrEmpty(txtNotiz.Text))
                {
                    MessageBox.Show("Bitte füllen Sie beide Textfelder aus.");
                    return;
                }
                selectedNotiz.Titel = txtTitel.Text;
                selectedNotiz.Notiz_ = txtNotiz.Text;

                string json = JsonConvert.SerializeObject(selectedNotiz);
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PutAsync($"{baseApiURLpost}/{selectedNotiz.id}", new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                txtTitel.Clear();
                txtNotiz.Clear();

                MessageBox.Show("Notiz erfolgreich geändert.");

                // Aktualisieren der Notizen im Hauptfenster
                previousWindow.Load_Notizen();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
