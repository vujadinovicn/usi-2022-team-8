﻿using HealthInstitution.Core.SystemUsers.Patients.Model;
using HealthInstitution.Core.SystemUsers.Patients.Repository;
using HealthInstitution.Core.SystemUsers.Users.Repository;
using HealthInstitution.GUI.SecretaryView;
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

namespace HealthInstitution.GUI.UserWindow
{
    /// <summary>
    /// Interaction logic for CrudbPatients.xaml
    /// </summary>
    public partial class CrudbPatients : Window
    {
        public CrudbPatients()
        {
            InitializeComponent();
            /*updateButton.IsEnabled = false;
            deleteButton.IsEnabled = false;
            blockButton.IsEnabled = false;*/
        }
        public void LoadGridRows()
        {
            List<Patient> patients = PatientRepository.GetInstance().patients;
            foreach (Patient patient in patients)
            {
                dataGrid.Items.Add(patient);
            }
        }
        private void createPatient_click(object sender, RoutedEventArgs e)
        {
            CreatePatientWindow createPatientWindow = new CreatePatientWindow();
            createPatientWindow.ShowDialog();
            this.Close();
        }

        private void updatePatient_click(object sender, RoutedEventArgs e)
        {
            Patient selectedPatient = (Patient)dataGrid.SelectedItem;
            if (selectedPatient != null) 
            {
                UpdatePatientWindow updatePatientWindow = new UpdatePatientWindow(selectedPatient);
                updatePatientWindow.ShowDialog();
                this.Close();
            }
        }

        private void deletePatient_click(object sender, RoutedEventArgs e)
        {
            Patient selectedPatient = (Patient)dataGrid.SelectedItem;
            if (selectedPatient != null)
            {
                UserRepository userRepository = UserRepository.GetInstance();
                PatientRepository patientRepository = PatientRepository.GetInstance();
                patientRepository.DeletePatient(selectedPatient.username);
                userRepository.DeleteUser(selectedPatient.username);
            }
        }

        private void blockPatient_click(object sender, RoutedEventArgs e)
        {
            Patient selectedPatient = (Patient)dataGrid.SelectedItem;
            if (selectedPatient != null)
            {
                PatientRepository patientRepository = PatientRepository.GetInstance();
                patientRepository.ChangeBlockedStatus(selectedPatient.username);
            }
        }
    }
}