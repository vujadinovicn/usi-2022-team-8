﻿using HealthInstitution.Core.Examinations.Model;
using HealthInstitution.Core.SystemUsers.Patients.Model;
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

namespace HealthInstitution.GUI.DoctorView
{
    /// <summary>
    /// Interaction logic for EditExaminationDialog.xaml
    /// </summary>
    public partial class EditExaminationDialog : Window
    {

        public Examination selectedExamination { get; set; }    
        public EditExaminationDialog(Examination selectedExamination)
        {
            this.selectedExamination = selectedExamination;
            InitializeComponent();
        }

        private void PatientComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var patientComboBox = sender as System.Windows.Controls.ComboBox;
            List<Patient> patients = new List<Patient>();
            //TODO  
            patientComboBox.ItemsSource = patients;
            patientComboBox.SelectedItem = selectedExamination.medicalRecord.patient;
        }

        private void HourComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var hourComboBox = sender as System.Windows.Controls.ComboBox;
            List<String> hours = new List<String>();
            for (int i = 9; i < 22; i++)
            {
                hours.Add(i.ToString());
            }
            hourComboBox.ItemsSource = hours;
            hourComboBox.SelectedIndex = 0;
            // TODO stavi sate
        }

        private void MinuteComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var minuteComboBox = sender as System.Windows.Controls.ComboBox;
            List<String> minutes = new List<String>();
            minutes.Add("00");
            minutes.Add("15");
            minutes.Add("30");
            minutes.Add("45");
            minuteComboBox.ItemsSource = minutes;
            minuteComboBox.SelectedIndex = 0;
            // TODO stavi minute
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string date = datePicker.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            String minutes = minuteComboBox.Text;
            String hours = hourComboBox.Text;
            var patient = (Patient)patientComboBox.SelectedItem;
        }
    }
}