using System.Collections.Generic;
using RiderAspNetMvc.ViewModels;

namespace RiderAspNetMvc.Services {
    internal interface IStudentService {
        List<StudentListItem> ListAll(params string[] order);
        Result Create(StudentCreate student);
    }
}