using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Studentportal.Data;
using Studentportal.Models;
using Studentportal.Models.Entities;

namespace Studentportal.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext DbContext;
        public StudentsController(AppDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed,
            };
            await DbContext.AddAsync(student);
            await DbContext.SaveChangesAsync();

          return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var Students = await DbContext.students.ToListAsync();
            return View(Students);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student=await DbContext.students.FindAsync(id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student=await DbContext.students.FindAsync(viewModel.Id);
            if (student != null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;
                await DbContext.SaveChangesAsync();
            }
            return RedirectToAction("List","Students");
        }
        [HttpPost]
        public async Task<IActionResult>Delete(Student viewModel)
        {
            var student = await DbContext.students
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Id==viewModel.Id);
            if(student is not null)
            {
                DbContext.students.Remove(viewModel);
                await DbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }
            
            
   
    }
}
 