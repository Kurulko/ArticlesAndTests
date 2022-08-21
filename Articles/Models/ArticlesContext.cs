using Articles.Models.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Articles.Models
{
    public class ArticlesContext : IdentityDbContext<User>
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<ResultOfTest> ResultsOfTests { get; set; }
        public ArticlesContext(DbContextOptions<ArticlesContext> options) : base(options)
            => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Test test = new();
            test.Id = 1;
            test.Name = "first";
            test.Description = "It's the first test";
            test.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRigNzYMk99dNNwXhpIaLeN-ZTBhMaoi2LC9Q&usqp=CAU";
            modelBuilder.Entity<Test>().HasData(test);


            List<Exercise> exercises = new();
            for (int i = 0, j = 1; i < 30; i += 3, j++)
            {
                Exercise exercise = new();
                exercise.Id = j;
                exercise.Question = $"x % 3 = 0";
                exercise.TestId = test.Id;
                exercises.Add(exercise);
            }
            modelBuilder.Entity<Exercise>().HasData(exercises);

            List<Answer> answers = new();
            for (int i = 0; i < 30; i++)
            {
                Answer answer = new();
                answer.Id = i + 1;
                answer.ExerciseId = i / 3 + 1;
                answer.Value =  i.ToString();
                answer.IsCorrect = i % 3 == 0;
                answers.Add(answer);
            }
            modelBuilder.Entity<Answer>().HasData(answers);

            base.OnModelCreating(modelBuilder);
        }
    }
}
