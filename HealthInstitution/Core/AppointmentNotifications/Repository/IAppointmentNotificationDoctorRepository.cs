﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Notifications.Repository
{
    public interface IAppointmentNotificationDoctorRepository
    {
        public void LoadFromFile();
        public void Save();
    }
}
