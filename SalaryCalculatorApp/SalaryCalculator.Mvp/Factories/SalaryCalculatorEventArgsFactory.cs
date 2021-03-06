﻿using System;
using SalaryCalculator.Mvp.EventsArguments;

namespace SalaryCalculator.Mvp.Factories
{
    public class SalaryCalculatorEventArgsFactory : ISalaryCalculatorEventArgsFactory
    {
        public IModelIdEventArgs GetModelIdEventArgs(int id)
        {
            return new ModelIdEventArgs(id);
        }

        public IModelIdEventArgs GetModelIdEventArgs(string id)
        {
            return new ModelIdEventArgs(id);
        }

        public IPaycheckEventArgs GetPaycheckEventArgs(decimal grossSalary, decimal fixedBonus, decimal nonFixedBonus, DateTime createdDate)
        {
           return new PaycheckEventArgs(grossSalary, fixedBonus, nonFixedBonus, createdDate);
        }

        public IRemunerationBillEventArgs GetRemunerationBillEventArgs(decimal grossSalary, DateTime createdDate)
        {
            return new RemunerationBillEventArgs(grossSalary ,createdDate);
        }

        public ISelfEmploymentEventArgs GetSelfEmploymentEventArgs(decimal socialSecurityIncome, DateTime createdDate,decimal additionalSocialSecurityIncome = 0, bool isInsuredForGDM = false)
        {
            return new SelfEmploymentEventArgs(socialSecurityIncome, createdDate, additionalSocialSecurityIncome, isInsuredForGDM);
        }

        public IEmployeeEventArgs GetEmployeeEventArgs(string firstName, string middleName, string lastName, string personalId)
        {
            return new EmployeeEventArgs(firstName, middleName, lastName, personalId);
        }
    }
}
