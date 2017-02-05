﻿using Moq;
using NUnit.Framework;
using SalaryCalculator.Data.Contracts;
using SalaryCalculator.Data.Models;
using SalaryCalculator.Data.Services.Contracts;
using SalaryCalculator.Mvp.Presenters;
using SalaryCalculator.Mvp.Views;
using SalaryCalculator.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator.Tests.Mvp.Presenters
{
    [TestFixture]
    public class ReportNonLaborPresenterTests
    {
        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenParameterServiceIsNull()
        {
            var view = new Mock<IReportNonLaborView>();
            var service = new Mock<IRemunerationBillService>();

            Assert.That(() => new ReportNonLaborPresenter(view.Object, null), Throws.InstanceOf<ArgumentNullException>().With.Message.Contains("The argument is null"));
        }

        [Test]
        [Ignore("Not finished test.")]
        public void Constructor_ShouldCreateInstanceCorrectly_WhenAllParametersArePassed()
        {
            var view = new Mock<IReportNonLaborView>();
            var service = new Mock<IRemunerationBillService>();
            var repo = new Mock<IRepository<RemunerationBill>>();
            var bill = new FakeRemunerationBill();

            IReportNonLaborPresenter presenter = new ReportNonLaborPresenter(view.Object, service.Object);

            Assert.IsInstanceOf<IReportNonLaborPresenter>(presenter);
        }
    }
}
