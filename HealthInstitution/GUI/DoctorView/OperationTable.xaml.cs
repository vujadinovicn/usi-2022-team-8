﻿using HealthInstitution.Core.Operations.Model;
using HealthInstitution.Core.Operations.Repository;
using HealthInstitution.Core.SystemUsers.Doctors.Model;
using HealthInstitution.Core.SystemUsers.Doctors.Repository;
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
    /// Interaction logic for OperationTable.xaml
    /// </summary>
    public partial class OperationTable : Window
    {
        OperationRepository operationRepository = OperationRepository.GetInstance();
        DoctorRepository doctorRepository = DoctorRepository.GetInstance();
        OperationDoctorRepository operationDoctorRepository = OperationDoctorRepository.GetInstance();  
        Doctor loggedDoctor;
        public OperationTable(Doctor doctor)
        {
            this.loggedDoctor = doctor;
            InitializeComponent();
            LoadGridRows();
        }
        public void LoadGridRows()
        {
            dataGrid.Items.Clear();
            List<Operation> doctorOperations = this.loggedDoctor.operations;
            foreach (Operation operation in doctorOperations)
            {
                dataGrid.Items.Add(operation);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new AddOperationDialog(this.loggedDoctor).ShowDialog();
            LoadGridRows();
            dataGrid.Items.Refresh();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Operation selectedOperation = (Operation)dataGrid.SelectedItem;
            new EditOperationDialog(selectedOperation).ShowDialog();
            LoadGridRows();
            dataGrid.Items.Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Are you sure you want to delete selected examination", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Operation selectedOperation = (Operation)dataGrid.SelectedItem;
                dataGrid.Items.Remove(selectedOperation);
                operationRepository.DeleteOperation(selectedOperation.id);
                doctorRepository.DeleteOperation(loggedDoctor, selectedOperation);
                operationDoctorRepository.SaveToFile();
            }
        }
    }
}