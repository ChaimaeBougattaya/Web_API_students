using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Model;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("controller")]
    public class Controller : ControllerBase
    {
        private List<Student> Lstudents;
        private readonly DataContext context;
        private readonly ILogger<Controller> logger;
        public Controller(DataContext context, ILogger<Controller> log)
        {
            this.context = context;
            this.logger = log;
        }
        [HttpGet]
        public async Task<List<Student>> GetAsync()
        {
            this.Lstudents = await context.students.ToListAsync();
            logger.LogInformation("data displayed here !");
            return Lstudents;
        }
        [HttpPost]
        public async Task<string> OnPostAsync(Student stud)
        {
            context.students.Add(stud);
            await context.SaveChangesAsync();
            return "inserted successfuly";
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            Student stud = await context.students.FindAsync(id);
            if (stud != null)
            {
                context.students.Remove(stud);
                await context.SaveChangesAsync();
                return "deleting student successfulty";
            }
            else
            {
                return "Error when deleting student";
            }
        }

    }
}
