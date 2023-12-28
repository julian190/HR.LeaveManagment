//using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagment.Application.Contracts.Presistance;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<ILeaveTypeRepository> GetUnitOfWork()
        {
            var mockUow = new Mock<ILeaveTypeRepository>();
          //  var mockLeaveTypeRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

           // mockUow.Setup(r => r.LeaveTypeRepository).Returns(mockLeaveTypeRepo.Object);

            return mockUow;
        }
    }
}