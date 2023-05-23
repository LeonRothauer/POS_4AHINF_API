﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
using System.Xml.Linq;

namespace WpfAppNotiz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Notiz> notizen = new List<Notiz>();
        string baseApiURL = "http://localhost:3020/notizen";
        string baseApiURLpost = "http://localhost:3020/notiz";
        public MainWindow()
        {
            InitializeComponent();
            btnLoad_Notizen();
            txtTitel.IsReadOnly = true;
            txtNotiz.IsReadOnly = true;
            LoadNotizenAsync();
        }

        public async Task LoadNotizenAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(baseApiURL);

                // Überprüfen ob Anforderung erfolgreich war
                response.EnsureSuccessStatusCode();

                //Antwort wird gelesen und deserialisiert
                string responseBody = await response.Content.ReadAsStringAsync();
                notizen = JsonConvert.DeserializeObject<List<Notiz>>(responseBody);

                // Überprüfen, ob Notizen vorhanden sind
                if (notizen.Count > 0)
                {
                    // Die erste Notiz auswählen
                    Notiz ersteNotiz = notizen.First();
                    lstNotizen.SelectedItem = ersteNotiz.id;

                    // Textfelder mit den Daten der ersten Notiz füllen
                    txtTitel.Text = ersteNotiz.Titel;
                    txtNotiz.Text = ersteNotiz.Notiz_;
                    lblErstelldatum.Content = ersteNotiz.Erstelldatum;
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNewNotiz_Click(object sender, RoutedEventArgs e)
        {
            SecondWindow secondWindow = new SecondWindow(this);
            secondWindow.Show();

            // Ausblenden des aktuellen MainWindow
            this.Hide();
        }

        private void btnChangeNotiz_Click(object sender, RoutedEventArgs e)
        {
            if (lstNotizen.SelectedItem != null)
            {
                Notiz selectedNotiz = notizen.Find(n => n.Titel.StartsWith(lstNotizen.SelectedItem.ToString()));
                if (selectedNotiz != null)
                {
                    UpdateWindow updateWindow = new UpdateWindow(this, selectedNotiz);
                    updateWindow.Show();
                    this.Hide();
                }
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Sind Sie sicher, dass Sie die Notiz löschen wollen?", "Bestätigung", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Notiz selectedNotiz = notizen.Find(n => n.Titel.StartsWith(lstNotizen.SelectedItem.ToString()));


                    if (selectedNotiz == null)
                    {
                        MessageBox.Show("Bitte wählen Sie eine Notiz aus, die gelöscht werden soll.");
                        return;
                    }
                    string notizId = selectedNotiz.Id;


                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.DeleteAsync($"{baseApiURLpost}/{notizId}");

                    response.EnsureSuccessStatusCode();

                    // gelöschte Notiz aus der Liste entfernen 
                    notizen.RemoveAll(n => n.Id == notizId);

                    btnLoad_Notizen();

                    txtTitel.Clear();
                    txtNotiz.Clear();
                    LoadNotizenAsync();
                }            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(baseApiURL);

            
            response.EnsureSuccessStatusCode();
   
            string responseBody = await response.Content.ReadAsStringAsync();
            notizen = JsonConvert.DeserializeObject<List<Notiz>>(responseBody);

            // Fügen Sie die Titel in der ListView hinzu
            lstNotizen.Items.Clear();
            foreach (Notiz item in notizen)
            {
                string shortTitle = item.Titel.Length > 20 ? item.Titel.Substring(0, 20) : item.Titel;
                lstNotizen.Items.Add(shortTitle);
            }
        }
        public async void btnLoad_Notizen()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(baseApiURL);

                // Überprüfen, ob die Anforderung erfolgreich war
                response.EnsureSuccessStatusCode();

                //Antwort deserialisieren 
                string responseBody = await response.Content.ReadAsStringAsync();
                notizen = JsonConvert.DeserializeObject<List<Notiz>>(responseBody);

                // Fügen Sie die Titel in der ListView hinzu
                lstNotizen.Items.Clear();
                foreach (Notiz item in notizen)
                {
                    string shortTitle = item.Titel.Length > 20 ? item.Titel.Substring(0, 20) : item.Titel;
                    lstNotizen.Items.Add(shortTitle);
                }

                // Wiederherstellen der Auswahl des ausgewählten Elements
                if (lstNotizen.Items.Count > 0)
                {
                    lstNotizen.SelectedItem = lstNotizen.Items[0];
                    string selectedTitle = lstNotizen.SelectedItem.ToString();
                    Notiz selectedNotiz = notizen.Find(n => n.Titel.StartsWith(selectedTitle));
                    if (selectedNotiz != null)
                    {
                        txtTitel.Text = selectedNotiz.Titel;
                        txtNotiz.Text = selectedNotiz.Notiz_;
                        lblErstelldatum.Content = selectedNotiz.Erstelldatum;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void lstNotizen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstNotizen.SelectedItem != null)
            {
                btnChange.IsEnabled = true;
                Notiz selectedNotiz = notizen.Find(n => n.Titel.StartsWith(lstNotizen.SelectedItem.ToString()));
                if (selectedNotiz != null)
                {
                    txtTitel.Text = selectedNotiz.Titel;
                    txtNotiz.Text = selectedNotiz.Notiz_;
                    lblErstelldatum.Content = selectedNotiz.Erstelldatum;
                }
            }
        }     
    }

}
