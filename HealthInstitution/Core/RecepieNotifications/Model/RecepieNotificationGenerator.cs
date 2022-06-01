﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core.RecepieNotifications.Repository;
using HealthInstitution.Core.Prescriptions.Repository;

namespace HealthInstitution.Core.RecepieNotifications.Model;

public class RecepieNotificationGenerator
{
    private string _loggedUser;

    public RecepieNotificationGenerator(string loggedUser)
    {
        _loggedUser = loggedUser;
    }

    public void GenerateAllSkippedNotifications()
    {
        foreach (var setting in RecepieNotificationSettingsRepository.GetInstance().Settings)
        {
            GenerateForOne(setting);
        }
    }

    private DateTime GetLastDateTime(RecepieNotificationSettings setting)
    {
        var createdNotifications = RecepieNotificationRepository.GetInstance().GetPatientPresctiptionNotification(setting.PatientUsername, setting.Prescription.Id);
        createdNotifications.OrderBy(o => o.TriggerDateTime).ToList();
        if (createdNotifications.Count == 0) return DateTime.Today.AddDays(-1);

        return createdNotifications.Last().TriggerDateTime;
    }

    private double CalculateIncrement(RecepieNotificationSettings setting)
    {
        return 24 / setting.Prescription.DailyDose;
    }

    private DateTime CalculateFirstDatetime(RecepieNotificationSettings setting)
    {
        DateTime lastDateTime = GetLastDateTime(setting);
        var firstDate = setting.Prescription.dateTime.AddHours(-setting.BeforeAmmount.Hour).AddMinutes(-setting.BeforeAmmount.Minute);
        lastDateTime = lastDateTime.AddMinutes(firstDate.Minute);
        lastDateTime = lastDateTime.AddHours(firstDate.Hour);
        return lastDateTime;
    }

    public void GenerateCronJobs(List<DateTime> dateTimes, RecepieNotificationSettings setting)
    {
        recepieNotificationCronJob recepieNotificationCronJob = new recepieNotificationCronJob();
        foreach (DateTime dateTime in dateTimes)
            recepieNotificationCronJob.GenerateJob(_loggedUser, setting, dateTime);
    }

    private void GenerateForOne(RecepieNotificationSettings setting)
    {
        List<DateTime> dateTimes = GenerateDateTimes(setting);
        GenerateCronJobs(dateTimes, setting);

        while (true)
        {
            foreach (var dateTime in dateTimes)
            {
                if (dateTime > DateTime.Now) return;
                int id = RecepieNotificationRepository.GetInstance().Notifications.Count;
                RecepieNotification recepieNotification = new RecepieNotification(id, setting.PatientUsername, setting.Prescription, true);
                recepieNotification.TriggerDateTime = dateTime;
                RecepieNotificationRepository.GetInstance().Add(recepieNotification);
            }
            dateTimes = NextDay(dateTimes);
        }
    }

    private List<DateTime> NextDay(List<DateTime> dateTimes)
    {
        for (int i = 0; i < dateTimes.Count; i++)
        {
            dateTimes[i] = dateTimes[i].AddDays(1);
        }
        return dateTimes;
    }

    public List<DateTime> GenerateDateTimes(RecepieNotificationSettings setting)
    {
        double increment = CalculateIncrement(setting);
        List<DateTime> notificationTimes = new List<DateTime>();
        for (int i = 0; i < setting.Prescription.DailyDose; i++)
        {
            if (notificationTimes.Count == 0) notificationTimes.Add(CalculateFirstDatetime(setting));
            else notificationTimes.Add(notificationTimes.Last().AddHours(increment));
        }
        return notificationTimes;
    }
}