﻿using HealthInstitution.Commands.DoctorCommands.Scheduling;
using HealthInstitution.Core;
using HealthInstitution.Core.Operations;
using HealthInstitution.Core.Operations.Model;
using HealthInstitution.Core.SystemUsers.Doctors.Model;
using HealthInstitution.ViewModels.ModelViewModels.Scheduling;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.ViewModels.GUIViewModels.DoctorViewViewModels.AppointmentsTable
{
    internal class OperationTableViewModel : ViewModelBase
    {
        public Doctor LoggedDoctor;

        public List<Operation> Operations;

        private int _selectedOperationIndex;

        public int SelectedOperationIndex
        {
            get
            {
                return _selectedOperationIndex;
            }
            set
            {
                _selectedOperationIndex = value;
                OnPropertyChanged(nameof(SelectedOperationIndex));
            }
        }

        private ObservableCollection<OperationViewModel> _operationsVM;

        public ObservableCollection<OperationViewModel> OperationsVM
        {
            get
            {
                return _operationsVM;
            }
            set
            {
                _operationsVM = value;
                OnPropertyChanged(nameof(OperationsVM));
            }
        }

        public void RefreshGrid()
        {
            _operationsVM.Clear();
            Operations.Clear();
            foreach (Operation operation in OperationService.GetByDoctor(LoggedDoctor.Username))
            {
                    Operations.Add(operation);
                    _operationsVM.Add(new OperationViewModel(operation));
            }
        }

        public Operation GetSelectedOperation()
        {
            return Operations[_selectedOperationIndex];
        }

        public ICommand CreateOperationCommand { get; }
        public ICommand EditOperationCommand { get; }
        public ICommand DeleteOperationCommand { get; }

        public OperationTableViewModel(Doctor loggedDoctor)
        {
            LoggedDoctor = loggedDoctor;
            CreateOperationCommand = new CreateOperationCommand(this, loggedDoctor);
            EditOperationCommand = new EditOperationCommand(this);
            DeleteOperationCommand = new DeleteOperationCommand(this);
            Operations = new();
            _operationsVM = new();
            RefreshGrid();
        }
    }
}