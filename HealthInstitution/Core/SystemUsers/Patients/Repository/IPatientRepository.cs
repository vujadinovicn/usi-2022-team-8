﻿using HealthInstitution.Core.Notifications.Model;
using HealthInstitution.Core.SystemUsers.Patients.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.SystemUsers.Patients.Repository
{
    public interface IPatientRepository : IRepository<Patient>
    {
        public void LoadFromFile();
        public void Save();
        public List<Patient> GetAll();
        public Patient GetByUsername(string username);
        public void Add(Patient patient);
        public void Update(Patient byPatient);
        public void Delete(string username);
        public void ChangeBlockedStatus(Patient patient);
        public void DeleteNotification(Patient patient, AppointmentNotification notification);
    }
}
