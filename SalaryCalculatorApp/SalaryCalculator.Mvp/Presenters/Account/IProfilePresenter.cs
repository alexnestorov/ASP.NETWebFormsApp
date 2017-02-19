﻿using System;

using WebFormsMvp;

using SalaryCalculator.Mvp.Views.Account;

namespace SalaryCalculator.Mvp.Presenters.Account
{
    public interface IProfilePresenter : IPresenter<IProfileView>
    {
        void GetUser(object sender, EventArgs e);

        void UpdateUser(object sender, EventArgs e);
    }
}