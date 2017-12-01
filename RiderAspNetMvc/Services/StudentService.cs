using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mapster;
using RiderAspNetMvc.DataAccess;
using RiderAspNetMvc.Models;
using RiderAspNetMvc.ViewModels;
using Common;

namespace RiderAspNetMvc.Services {
    public class StudentService : IStudentService {
        private readonly IRepository<Student> repository;

        public StudentService(IRepository<Student> repo = null) {
            repository = repo ?? new Repository<Student>();
        }

        private void Seed()
        {
            var id = 0;
            foreach (var name in new[] { "Eva Luator", "Ben Bitdiddle" }.Select(n => n.Split(' ')))
            {
                repository.Insert(new Student {
                    Id = ++id,
                    FirstName = name[0],
                    LastName = name[1]
                });
                repository.Save();
            }
        }

        public List<StudentListItem> ListAll(params string[] order) {

            var items = repository
                .GetAll()
                .ProjectToType<StudentListItem>()
                .OrderByMany(order);

            

            return items.ToList();
        }

        public StudentDetails GetDetailsById(int id) {
            return repository
                .GetById(id)
                .Adapt<StudentDetails>();
        }

        public StudentEdit GetEditById(int id) {
            return repository
                .GetById(id)
                .Adapt<StudentEdit>();
        }

        public Result Create(StudentCreate studentCreate) {
            repository.Insert(studentCreate.Adapt<StudentCreate, Student>());
            return repository.Save();
        }

        public Result Update(StudentEdit studentEdit) {
            repository.Update(studentEdit.Adapt<StudentEdit, Student>());
            return repository.Save();
        }
    }
}
